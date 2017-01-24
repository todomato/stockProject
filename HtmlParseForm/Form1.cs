using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HtmlParseForm
{
    public partial class Form1 : Form
    {
        private bool isParsed = true;
        private string url_revenue = "http://goodinfo.tw/StockInfo/ShowSaleMonChart.asp?STOCK_ID={0}"; //每月營收
        private string url_session = "http://goodinfo.tw/StockInfo/ShowK_Chart.asp?STOCK_ID={0}&CHT_CAT2=QUAR";    //季K
        private string url_finance_ratio = "http://goodinfo.tw/StockInfo/StockFinDetail.asp?RPT_CAT=XX_M_QUAR&STOCK_ID={0}";    //單季季表財務比率
        private string default_stock = "2912";

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = "...";
            var url = string.Format(url_finance_ratio, default_stock);
            string xpath = "/html/body/table[2]/tr/td[3]/table/tr/td/div/div/div/table/tr[1]/td";

            textBox2.Text = "";
            url = (string.IsNullOrEmpty(txt_Url.Text) ? url : txt_Url.Text);
            xpath = (string.IsNullOrEmpty(txt_xPath.Text)) ? xpath : txt_xPath.Text;

            //下載html
            var doc = ParseHtmlToDoc(url, ck_unicode.Checked);

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

        private static HtmlAgilityPack.HtmlDocument ParseHtmlToDoc(string url, bool ischeckCode)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            using (var myWebClient = new WebClient())
            {

                if (ischeckCode)
	            {
                    myWebClient.Encoding = Encoding.UTF8;
	            }
                string page = myWebClient.DownloadString(url);
                doc.LoadHtml(page);
            }
            return doc;
        }
    }
}
