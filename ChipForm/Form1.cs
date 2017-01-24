using ChipForm.model;
using ChipForm.service;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            var dbDateList = svc.GetDateList();
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
            int count = 1 ;
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
                #endregion
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
            var dbDateList = svc.GetDateList();


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
                if(dbDateList.Any(c => c == date)) continue;

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
                    #endregion
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
            HttpWebRequest request = HttpWebRequest.Create(Url) as HttpWebRequest;
            string page = null;
            request.Method = "POST";    // 方法
            request.KeepAlive = true; //是否保持連線
            request.ContentType = "application/x-www-form-urlencoded";

            //2016 年日期列表, 新的列表要用新的邏輯去抓每週一,如果回傳空值要抓隔天之類的,或是提供自行輸入抓取
            var dates = GetOldDate(); 

            //TODO foreach 回圈抓資料

            string param = "download=&query_year=2016&query_week=20161226&select2=ALLBUT0999&sorting=by_issue";
            byte[] bs = Encoding.ASCII.GetBytes(param);

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
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            string xpath = textBox2.Text;
           

            //解析資料
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                        
                    var str = node.InnerText.Trim();
                    //解析字串
                    List<string> list = str.Split(new string[] { "\t\t\t" }, StringSplitOptions.None).Select(c => c.Trim()).ToList();

                    foreach (var item in list)
	                {
                        textBox1.Text += item + Environment.NewLine;
	                }
                        
                }
            }


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

        private List<string> GetOldDate()
        {
            var data = new List<string>();
            data.Add("20161226");
            data.Add("20161219");
            data.Add("20161212");
            data.Add("20161205");
            data.Add("20161128");
            data.Add("20161121");
            data.Add("20161114");
            data.Add("20161107");
            data.Add("20161031");
            data.Add("20161024");
            data.Add("20161017");
            data.Add("20161011");
            data.Add("20161003");
            data.Add("20160926");
            data.Add("20160919");
            data.Add("20160912");
            data.Add("20160905");
            data.Add("20160829");
            data.Add("20160822");
            data.Add("20160815");
            data.Add("20160808");
            data.Add("20160801");
            data.Add("20160725");
            data.Add("20160718");
            data.Add("20160711");
            data.Add("20160704");
            data.Add("20160627");
            data.Add("20160620");
            data.Add("20160613");
            data.Add("20160606");
            data.Add("20160530");
            data.Add("20160523");
            data.Add("20160516");
            data.Add("20160509");
            data.Add("20160503");
            data.Add("20160425");
            data.Add("20160418");
            data.Add("20160411");
            data.Add("20160406");
            data.Add("20160328");
            data.Add("20160321");
            data.Add("20160314");
            data.Add("20160307");
            data.Add("20160301");
            data.Add("20160222");
            data.Add("20160215");
            data.Add("20160201");
            data.Add("20160125");
            data.Add("20160118");
            data.Add("20160111");
            data.Add("20160104");

            return data;
        }
        #endregion

        /// <summary>
        /// 查詢DB集保的最新日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDBChipDate_Click(object sender, EventArgs e)
        {
            //判斷資料庫最新日期
            ChipService svc = new ChipService();
            var dbDateList = svc.GetDateList();

            //顯示到畫面
            dbDateList.Sort();
            textBox1.Text = dbDateList.Last();

            MessageBox.Show("查詢完成");
        }

       
    }
}


