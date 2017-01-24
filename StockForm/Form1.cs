using AutoMapper;
using HtmlAgilityPack;
using StockForm.Model;
using StockForm.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockForm
{
    public partial class Form1 : Form
    {
        private bool isParsed = true;
        private string url_revenue = "http://goodinfo.tw/StockInfo/ShowSaleMonChart.asp?STOCK_ID={0}"; //每月營收
        private string url_session = "http://goodinfo.tw/StockInfo/ShowK_Chart.asp?STOCK_ID={0}&CHT_CAT2=QUAR";    //季K
        private string url_finance_ratio = "http://goodinfo.tw/StockInfo/StockFinDetail.asp?RPT_CAT=XX_M_QUAR&STOCK_ID={0}";    //單季季表財務比率
        private string url_reven = "http://mops.twse.com.tw/nas/t21/sii/t21sc03_100_2_0.html";
        private string default_stock = "2912";

        //revenue /html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr[7]
        //sesson K /html/body/table[2]/tr/td[3]/table/tr/td/div[3]/div/div/table/tr[5]
        //ratio /html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr[2]/td

        public Form1()
        {
            InitializeComponent();
        }

        //查詢
        private void btn_query_Click(object sender, EventArgs e)
        {
            textBox2.Text = "...";
            var url = string.Format(url_reven, default_stock);
            string xpath = "/html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr[1]/td";
            xpath = "/html/body/center/center/table[3]/tr[2]/td/table/tr[11]";
            
            textBox2.Text = "";
            url = (string.IsNullOrEmpty(txt_Url.Text) ? url : txt_Url.Text);
            xpath = (string.IsNullOrEmpty(txt_xPath.Text)) ? xpath : txt_xPath.Text;

            //下載html
            var doc = ParseHtmlToDoc(url);

            //解析html
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    textBox2.Text += node.InnerText.Trim() + Environment.NewLine;
                } 
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }

        //更新營收
        private void btn_revenue_Click(object sender, EventArgs e)
        {
            textBox2.Text = "...";
            //每月營收
            var id = default_stock;
            var url = string.Format(url_revenue, id);
            
            //下載html
            var doc = ParseHtmlToDoc(url);
            //解析html
            var data = ParseRevenueHtml(id, doc);
            //save db
            if (isParsed)
            {
                try
                {
                    StockService svc = new StockService();
                    svc.UpdateRevenue(id, data);
                    textBox2.Text = "更新完成";
                }
                catch (Exception ex)
                {
                    textBox2.Text = ex.Message;
                } 
            }
        }

        //更新季K
        private void btn_sessionK_Click(object sender, EventArgs e)
        {
            textBox2.Text = "...";
            //季K
            var id = default_stock;
            var url = string.Format(url_session, id);

            //下載html
            var doc = ParseHtmlToDoc(url);
            //解析html
            var data = ParseSessionKHtml(id, doc);
            //save db
            if (isParsed)
            {
                try
                {
                    StockService svc = new StockService();
                    svc.UpdateKSession(id, data);
                    textBox2.Text = "更新完成";
                }
                catch (Exception ex)
                {
                    textBox2.Text = ex.Message;
                }
            }
        }

        //財務比率
        private void btn_ratio_Click(object sender, EventArgs e)
        {
            textBox2.Text = "...";
            var id = default_stock;
            var url = string.Format(url_finance_ratio, id);

            //下載html
            var doc = ParseHtmlToDoc(url);
            //解析html
            var data = ParseFinaceRatioHtml(id, doc);
            //save db
            if (isParsed)
            {
                try
                {
                    StockService svc = new StockService();
                    svc.UpdateFinaceRatio(id, data);
                    textBox2.Text = "更新完成";
                }
                catch (Exception ex)
                {
                    textBox2.Text = ex.Message;
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            txt_stock.Text = string.IsNullOrEmpty(txt_stock.Text) ? default_stock : txt_stock.Text;

            AnalysisService svc = new AnalysisService();
            svc.Search(txt_stock.Text, "2016Q3");

        }

        #region 私人方法
        //下載html -> htmlDoc
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

        //解析營收 -> list
        private List<RevenueModel> ParseRevenueHtml(string id, HtmlAgilityPack.HtmlDocument doc)
        {
            string xpath = "/html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr";
            var data = new List<RevenueModel>();
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);

                foreach (HtmlNode node in nodes)
                {
                    data.Add(new RevenueModel(id, node.InnerText.Trim()));
                }
                this.isParsed = true;
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
                this.isParsed = false;
            }
            return data;
        }

        //解析季K -> list
        private List<KSessionModel> ParseSessionKHtml(string id, HtmlAgilityPack.HtmlDocument doc)
        {
            string xpath = "/html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr";
            var data = new List<KSessionModel>();
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath);

                foreach (HtmlNode node in nodes)
                {
                    data.Add(new KSessionModel(id, node.InnerText.Trim()));
                }
                this.isParsed = true;
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
                this.isParsed = false;
            }
            return data;
        }

        //解析財務比率 -> list
        private List<FinaceRatioModel> ParseFinaceRatioHtml(string id, HtmlAgilityPack.HtmlDocument doc)
        {
            string xpath = "/html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr[{0}]/td";
            var data = new List<FinaceRatioModel>();
            try
            {
                for (int i = 1; i < 13; i++)
                {
                    string path = string.Format(xpath, i.ToString());
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(path);

                    //創建物件
                    if (i == 1) 
                    {
                        foreach (HtmlNode node in nodes)
	                    {
                            if (!node.InnerText.Trim().Contains(FinaceRatioList.GetTitleById(i)))
	                        {
                                //加入日期
                                var entity = new FinaceRatioModel() { ID = id, Session = node.InnerText.Trim() };
                                data.Add(entity);
	                        }
	                    }
                    }
                    else
                    {
                        for (int j = 1; j <= data.Count; j++)   //按編號set財務info
                        {
                            Type ntype = data[j-1].GetType();
                            PropertyInfo propertyInfo = ntype.GetProperty(FinaceRatioList.GetFieldById(i));
                            propertyInfo.SetValue(data[j - 1], Double.Parse(ParseDashToZero(nodes[j].InnerText.Trim())));
                        }
                    }
                }

                this.isParsed = true;
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
                this.isParsed = false;
            }
            return data;
        }

        private string ParseDashToZero(string str)
        {
            //防呆2012年以前無合併報表資料
            if (str == "-")
            {
                return "0";
            }
            return str;
        }
        #endregion

        //解析http://mops.twse.com.tw/nas/t21/sii/t21sc03_103_1_0.html 從excel吐出來的資料
        private void button2_Click(object sender, EventArgs e)
        {
            var a = textBox1.Text;
            var list = a.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

            var d = new List<mops_revenue>();
            foreach (var row in list)
            {
                var rData = new List<string>();
                var cols = row.Split(new string[] { "\t" }, StringSplitOptions.None).ToList();
                string id = cols[0];
                int num = 0;

                if (int.TryParse(id, out num) && id.Length == 4 && cols.Count == 11)
                {
                    var temp = new mops_revenue()
                    {
                        ID = cols[0],
                        Company = cols[1],
                        Revenue = cols[2],
                        YOY = cols[5],
                        TotalRevenue = cols[7],
                        TotalYOY = cols[9],
                        Note = cols[10]
                    };
                    d.Add(temp);
                }
                else
                {
                    var test = cols;
                }

            }
        }

       
       
    }
    
}
