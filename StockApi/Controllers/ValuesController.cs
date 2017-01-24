using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace StockApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string id)
        {
            //下載html
            var doc = new HtmlDocument();
            using (var myWebClient = new WebClient())
            {
                myWebClient.Encoding = Encoding.UTF8;
                string page = myWebClient.DownloadString("http://goodinfo.tw/StockInfo/ShowSaleMonChart.asp?STOCK_ID=2912");
                doc.LoadHtml(page);
            }

            //解析html
            //string xpath = WebConfigurationManager.AppSettings["xpath"].ToString();
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(id);
            // /html/body/table[2]/tbody/tr/td[3]/table/tbody/tr/td/div/div/div/table/tbody[1]/tr[7]
            ///html[1]/body[1]/table[2]/tbody[1]/tr[1]/td[3]/table[1]/tbody[1]/tr[1]/td[1]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]

            foreach (HtmlNode node in nodes)
            {
                var a = node.InnerText.Trim();
            } 


            //更新db


            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
