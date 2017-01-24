using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class RevenueModel
    {
        public RevenueModel() { }

        public RevenueModel(string id, string str)
        {
            //解析字串
            List<string> list = str.Split(new string[]{"  "}, StringSplitOptions.None).ToList();
            this.ID = id;
            this.Date = list[0];
            this.rOpen = double.Parse(ParseDashToZero(list[1]));
            this.rHigh = double.Parse(ParseDashToZero(list[2]));
            this.rClose = double.Parse(ParseDashToZero(list[3]));
            this.rLow = double.Parse(ParseDashToZero(list[4]));
            this.UpDownPrice = double.Parse(ParseDashToZero(list[5]));
            this.UpDownPercentage = double.Parse(ParseDashToZero(list[6]));
            this.Revenue = double.Parse(list[7]);
            this.MonthGrowthRate = double.Parse(ParseDashToZero(list[8]));
            this.YearGrowthRate = double.Parse(ParseDashToZero(list[9]));
            this.TotalRevenue = double.Parse(list[10]);
            this.TotalYearGrowthRate = double.Parse(list[11]);
            this.CoRevenue = double.Parse(ParseDashToZero(list[12]));
            this.CoMonthGrowthRate = double.Parse(ParseDashToZero(list[13]));
            this.CoYearGrowthRate = double.Parse(ParseDashToZero(list[14]));
            this.CoTotalRevenue = double.Parse(ParseDashToZero(list[15]));
            this.CoTotalYearGrowthRate = double.Parse(ParseDashToZero(list[16]));
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

        public string Date { get; set; }
        public string ID { get; set; }
        public double rOpen { get; set; }
        public double rHigh { get; set; }
        public double rClose { get; set; }
        public double rLow { get; set; }
        public double UpDownPrice { get; set; }
        public double UpDownPercentage { get; set; }
        public double Revenue { get; set; }
        public double MonthGrowthRate { get; set; }
        public double YearGrowthRate { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalYearGrowthRate { get; set; }
        public double CoRevenue { get; set; }
        public double CoMonthGrowthRate { get; set; }
        public double CoYearGrowthRate { get; set; }
        public double CoTotalRevenue { get; set; }
        public double CoTotalYearGrowthRate { get; set; }


      
    }
}
