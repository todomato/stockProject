using ChipForm.model;
using ChipForm.service;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ChipForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新增單一周集保資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_query_Click(object sender, EventArgs e)
        {
            //日期
            string date = comboBox1.SelectedValue.ToString();

            //防呆
            ChipService svc = new ChipService();
            var dbDateList = svc.GetChipDateList();
            if (string.IsNullOrEmpty(date)) return; //防空值
            //if (dbDateList.Any(c => c == date))
            //{
            //    MessageBox.Show("資料庫已有此日期資料");
            //    return; //防重複
            //}

            // 取得近一年stock code : 將股號放在txt檔即可
            string fullPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "StockCode.txt");
            var codeList = File.ReadAllLines(fullPath, Encoding.UTF8).ToList();

            //取得集保資訊
            string urlTemplate = "http://www.tdcc.com.tw/smWeb/QryStock.jsp?SCA_DATE={0}&SqlMethod=StockNo&StockNo={1}&StockName=&sub=%ACd%B8%DF";
            string url = "";
            string xpathTemplate = "/html/body/table/tr/td/table[6]/tbody/tr[{0}]";
            string xpath = "";
            // /html/body/table/tr/td/table[6]/tbody/tr[17]

            var data = new List<ChipModel>();
            var errprList = new List<string>();

            //代碼列
            int count = 1;
            foreach (var code in codeList)
            {
                textBox1.Text = string.Format("{2} : {0}/{1}", count++, codeList.Count, date);
                Application.DoEvents();

                url = string.Format(urlTemplate, date, code);

                #region 解析web籌碼

                //下載html
                var doc = ParseHtmlToDoc(url);

                try
                {
                    //解析資料
                    // 2~16 : 15個級距
                    for (int i = 2; i <= 16; i++)
                    {
                        xpath = string.Format(xpathTemplate, i);
                        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                        if (nodes != null)
                        {
                            foreach (HtmlNode node in nodes)
                            {
                                data.Add(new ChipModel(code, node.InnerText.Trim()));
                            }
                        }
                    }

                    // 17 : 總和
                    xpath = string.Format(xpathTemplate, 17);
                    HtmlNodeCollection nodes2 = doc.DocumentNode.SelectNodes(xpath);
                    if (nodes2 != null)
                    {
                        foreach (HtmlNode node in nodes2)
                        {
                            string str = node.InnerText.Trim();
                            if (str.Contains("合　計"))
                            {
                                data.Where(c => c.Code == code).ToList()
                                .ForEach(c => c.SetTotal(node.InnerText.Trim(), date));
                            }
                            else
                            {
                                xpath = string.Format(xpathTemplate, 18);
                                HtmlNodeCollection nodes3 = doc.DocumentNode.SelectNodes(xpath);
                                if (nodes3 != null)
                                {
                                    foreach (HtmlNode node3 in nodes3)
                                    {
                                        string str3 = node3.InnerText.Trim();
                                        if (str3.Contains("合　計"))
                                        {
                                            data.Where(c => c.Code == code).ToList()
                                                .ForEach(c => c.SetTotal(node3.InnerText.Trim(), date));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errprList.Add(code);
                    textBox1.Text = Environment.NewLine + ex.InnerException.ToString();
                    Application.DoEvents();
                }

                #endregion 解析web籌碼
            }

            //更新資料庫
            svc.AddChip(data);

            //錯誤紀錄
            if (errprList.Count != 0)
            {
                RecordError(date, errprList);
                MessageBox.Show("有錯誤紀錄！");
            }

            MessageBox.Show("更新完成");
        }

        /// <summary>
        /// 取得集保頁面每周日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_queryDate_Click(object sender, EventArgs e)
        {
            //取得日期列表
            var dateList = getChipDateList();

            BindingSource bs = new BindingSource();
            bs.DataSource = dateList;
            comboBox1.DataSource = bs;
        }

        /// <summary>
        /// 更新至最近集保
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_all_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要更新全部，等待時間會非常長??", "Close Application", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            //日期
            var dateList = getChipDateList();
            dateList.Reverse();

            ChipService svc = new ChipService();
            var dbDateList = svc.GetChipDateList();

            // 取得近一年stock code : 將股號放在txt檔即可
            string fullPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "StockCode.txt");
            var codeList = File.ReadAllLines(fullPath, Encoding.UTF8).ToList();

            //取得集保資訊
            string urlTemplate = "http://www.tdcc.com.tw/smWeb/QryStock.jsp?SCA_DATE={0}&SqlMethod=StockNo&StockNo={1}&StockName=&sub=%ACd%B8%DF";
            string url = "";
            string xpathTemplate = "/html/body/table/tr/td/table[6]/tbody/tr[{0}]";
            string xpath = "";
            // /html/body/table/tr/td/table[6]/tbody/tr[17]

            foreach (var date in dateList)
            {
                //防呆 資料庫有就不新增
                if (dbDateList.Any(c => c == date)) continue;

                var data = new List<ChipModel>();
                var errprList = new List<string>();

                //代碼列
                int count = 1;
                foreach (var code in codeList)
                {
                    textBox1.Text = string.Format("{2} : {0}/{1}", count++, codeList.Count, date);
                    Application.DoEvents();

                    //設定網址
                    url = string.Format(urlTemplate, date, code);

                    #region 解析web籌碼

                    //下載html
                    var doc = ParseHtmlToDoc(url);

                    try
                    {
                        //解析資料
                        // 2~16 : 15個級距
                        for (int i = 2; i <= 16; i++)
                        {
                            xpath = string.Format(xpathTemplate, i);
                            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                            if (nodes != null)
                            {
                                foreach (HtmlNode node in nodes)
                                {
                                    data.Add(new ChipModel(code, node.InnerText.Trim()));
                                }
                            }
                        }

                        // 17 : 總和
                        xpath = string.Format(xpathTemplate, 17);
                        HtmlNodeCollection nodes2 = doc.DocumentNode.SelectNodes(xpath);
                        if (nodes2 != null)
                        {
                            foreach (HtmlNode node in nodes2)
                            {
                                string str = node.InnerText.Trim();
                                if (str.Contains("合　計"))
                                {
                                    data.Where(c => c.Code == code).ToList()
                                        .ForEach(c => c.SetTotal(node.InnerText.Trim(), date));
                                }
                                else
                                {
                                    xpath = string.Format(xpathTemplate, 18);
                                    HtmlNodeCollection nodes3 = doc.DocumentNode.SelectNodes(xpath);
                                    if (nodes3 != null)
                                    {
                                        foreach (HtmlNode node3 in nodes3)
                                        {
                                            string str3 = node3.InnerText.Trim();
                                            if (str3.Contains("合　計"))
                                            {
                                                data.Where(c => c.Code == code).ToList()
                                                .ForEach(c => c.SetTotal(node3.InnerText.Trim(), date));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errprList.Add(code);
                        textBox1.Text = Environment.NewLine + ex.InnerException.ToString();
                        Application.DoEvents();
                    }

                    #endregion 解析web籌碼
                }

                //更新資料庫
                svc.AddChip(data);

                //錯誤紀錄
                if (errprList.Count != 0)
                {
                    RecordError(date, errprList);
                    MessageBox.Show("有錯誤紀錄！");
                }
            }

            MessageBox.Show("更新完成");
        }

        /// <summary>
        /// 上市 : 三大法人周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_weekTrade_Click(object sender, EventArgs e)
        {
            //上市三大法人週買賣超 "http://www.tse.com.tw/ch/trading/fund/TWT54U/TWT54U.php";
            // 參數 download=&query_year=2017&query_week=20170103&select2=ALLBUT0999&sorting=by_issue
            // xpath : //*[@id="tbl-sortable"]/tbody/tr[1]

            string Url = "http://www.tse.com.tw/ch/trading/fund/TWT54U/TWT54U.php";
            ChipService svc = new ChipService();
            var dbDateList = svc.GetinstitutionDateListMarket1();

            //2016 年日期列表, 新的列表要用新的邏輯去抓每週一,如果回傳空值要抓隔天之類的,或是提供自行輸入抓取
            var dates = GetDates();

            foreach (var date in dates)
            {
                //防呆 資料庫有就不新增
                if (dbDateList.Any(c => c == date)) continue;

                var year = date.Substring(0, 4);
                string param = string.Format("download=&query_year={1}&query_week={0}&select2=ALLBUT0999&sorting=by_issue", date, year);
                byte[] bs = Encoding.ASCII.GetBytes(param);

                HttpWebRequest request = HttpWebRequest.Create(Url) as HttpWebRequest;
                string page = null;
                request.Method = "POST";    // 方法
                request.KeepAlive = false; //是否保持連線
                request.ContentType = "application/x-www-form-urlencoded";

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    page = sr.ReadToEnd();
                    sr.Close();
                }

                //解析html
                HtmlNode.ElementsFlags.Remove("option");
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                string xpath = "//*[@id=\"tbl-sortable\"]/tbody";

                //解析資料
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        var str = node.InnerText.Trim();
                        //解析字串
                        List<string> list = str.Split(new string[] { "\t\t\t" }, StringSplitOptions.None).Select(c => c.Trim()).ToList();

                        // 組裝
                        var institutions = new List<InstitutionalModel>();
                        for (int i = 0; i <= list.Count; i = i + 17)
                        {
                            //非stock
                            if (list[i].Length != 4) continue;
                            textBox1.Text = string.Format("{1} : {0}", institutions.Count, date);
                            Application.DoEvents();

                            var item = new InstitutionalModel()
                            {
                                Code = list[i],
                                Category = "上市",
                                Type = "week",
                                InfoDate = date,
                                ForeignBuy = decimal.Parse(list[i + 2]),
                                ForeignSell = decimal.Parse(list[i + 3]),
                                ForeignNet = decimal.Parse(list[i + 4]),
                                DomesticBuy = decimal.Parse(list[i + 5]),
                                DomesticSell = decimal.Parse(list[i + 6]),
                                DomesticNet = decimal.Parse(list[i + 7]),
                                DealerNet = decimal.Parse(list[i + 8]),
                                TotalNet = decimal.Parse(list[i + 15])
                            };

                            institutions.Add(item);
                        }

                        //更新資料庫
                        svc.AddInstitution(institutions);
                    }
                }
            }

            MessageBox.Show("更新完成");
        }

        /// <summary>
        /// 上櫃 : 三大法人周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_weekTrade2_Click(object sender, EventArgs e)
        {
            //上櫃三大法人週買賣超 "http://www.tpex.org.tw/web/stock/3insti/daily_trade/3itrade_hedge_print.php?l=zh-tw&se=EW&t=W&d=105/06/28&s=0,asc,0";
            // xpath : /html/body/table/tbody

            string UrlTemplate = "http://www.tpex.org.tw/web/stock/3insti/daily_trade/3itrade_hedge_print.php?l=zh-tw&se=EW&t=W&d={0}/{1}/{2}&s=0,asc,0";

            ChipService svc = new ChipService();
            var dbDateList = svc.GetinstitutionDateListMarket2();

            var dates = GetDates();

            foreach (var date in dates)
            {
                //防呆 資料庫有就不新增
                if (dbDateList.Any(c => c == date)) continue;

                //解析日期格式 : ex. 105/12/23
                var year = (int.Parse(date.Substring(0, 4)) - 1911).ToString();
                var Url = string.Format(UrlTemplate, year, date.Substring(4, 2), date.Substring(6, 2));
                HttpWebRequest request = HttpWebRequest.Create(Url) as HttpWebRequest;
                string page = null;
                request.Method = "GET";    // 方法
                request.KeepAlive = false; //是否保持連線
                request.ContentType = "application/x-www-form-urlencoded";

                using (WebResponse response = request.GetResponse())
                {
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    page = sr.ReadToEnd();
                    sr.Close();
                }

                //解析html
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                string xpath = "/html/body/table/tbody";

                //解析資料
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                if (nodes != null)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        var str = node.InnerText.Trim();
                        //解析字串
                        List<string> list = str.Split(new string[] { "\r\n\t\t\t\t" }, StringSplitOptions.None).Select(c => c.Trim()).ToList();

                        // 組裝
                        var institutions = new List<InstitutionalModel>();
                        for (int i = 0; i < list.Count; i = i + 16)
                        {
                            //非stock
                            if (list[i].Length != 4) continue;
                            textBox1.Text = string.Format("{1} : {0}", institutions.Count, date);
                            Application.DoEvents();

                            var item = new InstitutionalModel()
                            {
                                Code = list[i],
                                Category = "上櫃",
                                Type = "week",
                                InfoDate = date,
                                ForeignBuy = decimal.Parse(list[i + 2]),
                                ForeignSell = decimal.Parse(list[i + 3]),
                                ForeignNet = decimal.Parse(list[i + 4]),
                                DomesticBuy = decimal.Parse(list[i + 5]),
                                DomesticSell = decimal.Parse(list[i + 6]),
                                DomesticNet = decimal.Parse(list[i + 7]),
                                DealerNet = decimal.Parse(list[i + 8]),
                                TotalNet = decimal.Parse(list[i + 15])
                            };

                            institutions.Add(item);
                        }

                        //更新資料庫
                        svc.AddInstitution(institutions);
                    }
                }
            }

            MessageBox.Show("更新完成");
        }

        #region 私人方法

        /// <summary>
        /// 取得日期列表
        /// </summary>
        /// <returns></returns>
        private List<string> getChipDateList()
        {
            var list = new List<string>();

            //查詢日期列表
            string url = "http://www.tdcc.com.tw/smWeb/QryStock.jsp";
            string xpath = "/html/body/table/tr/td/table[2]/tbody/tr/td[2]/table/tr[1]/td/select";
            xpath = "//option";

            //預設option會被當作empty : https://goo.gl/1u82pO
            HtmlNode.ElementsFlags.Remove("option");

            //下載html
            var doc = ParseHtmlToDoc(url);

            //解析html
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string strValue = (node.InnerText);
                    list.Add(strValue);
                }
            }
            catch (Exception ex)
            {
            }

            return list;
        }

        /// <summary>
        /// 下載網頁
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HtmlAgilityPack.HtmlDocument ParseHtmlToDoc(string url)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            using (var myWebClient = new WebClient())
            {
                //myWebClient.Encoding = Encoding.UTF8;
                string page = myWebClient.DownloadString(url);
                doc.LoadHtml(page);
            }
            return doc;
        }

        /// <summary>
        /// 記錄錯誤股票代碼
        /// </summary>
        /// <param name="errprList"></param>
        private static void RecordError(string dateName, List<string> errprList)
        {
            string txt_name = dateName + ".txt";
            FileStream fileStream = new FileStream(txt_name, FileMode.Create);
            fileStream.Close();   //切記開了要關,不然會被佔用而無法修改喔!!!

            using (StreamWriter sw = new StreamWriter(txt_name))
            {
                // 欲寫入的文字資料 ~
                foreach (var item in errprList)
                {
                    sw.WriteLine(item);
                }
            }
        }

        private List<string> GetDates()
        {
            var data = txt_weeks.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(c => c.Trim()).ToList();

            return data;
        }

        #endregion 私人方法

        /// <summary>
        /// 查詢DB集保的最新日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDBChipDate_Click(object sender, EventArgs e)
        {
            //判斷資料庫最新日期
            ChipService svc = new ChipService();
            var dbDateList = svc.GetChipDateList();

            //顯示到畫面
            dbDateList.Sort();
            textBox1.Text = dbDateList.Last();

            MessageBox.Show("查詢完成");
        }
    }
}