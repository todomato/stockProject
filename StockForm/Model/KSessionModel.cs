using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class KSessionModel
    {
        public KSessionModel() { }

        public KSessionModel(string id, string str)
        {
            //解析字串
            List<string> list = str.Split(new string[]{"  "}, StringSplitOptions.None).ToList();
            this.ID = id;
            this.Session = ParseSession(list[0].Substring(0, 4));  //處理session字串邏輯
            this.TradeCount = int.Parse(list[0].Substring(4, 2));
            this.kOpen = double.Parse(list[1]);
            this.kHigh = double.Parse(list[2]);
            this.kLow = double.Parse(list[3]);
            this.kClose = double.Parse(list[4]);
            this.UpDownPrice = double.Parse(ParseDashToZero(list[5]));
            this.UpDownPercentage = double.Parse(ParseDashToZero(list[6].Replace("%","")));
       
        }

        public string ID { get; set; }
        public string Session { get; set; }
        public int TradeCount { get; set; }
        public double kOpen { get; set; }
        public double kHigh { get; set; }
        public double kClose { get; set; }
        public double kLow { get; set; }
        public double UpDownPrice { get; set; }
        public double UpDownPercentage { get; set; }


        private string ParseDashToZero(string str)
        {
            //防呆2012年以前無合併報表資料
            if (str == "-")
            {
                return "0";
            }
            return str;
        }

        private string ParseSession(string str)
        {
            string y = str.Substring(0, 1);  //9x表示19xx年分
            if (y == "9")
            {
                return string.Format("19{0}", str);
            }

            return string.Format("20{0}", str);
        }

    }
}
