using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class mops_revenue
    {
        //公司	公司名稱	當月營收	上月營收	去年當月營收	上月比較	去年同月	當月累計營收	去年累計營收	前期比較	
        public string ID { get; set; }
        public string Company { get; set; }
        public string Revenue { get; set; }
        public string YOY { get; set; }
        public string TotalRevenue { get; set; }
        public string TotalYOY { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Note { get; set; }

    }
}
