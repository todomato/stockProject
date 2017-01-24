using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class FinaceRatioModel
    {
        public FinaceRatioModel() { }

        public string Session { get; set; }
        public string ID { get; set; }
        public double GrossMargin { get; set; }
        public double OperatingProfitMargin { get; set; }
        public double EarningBeforeTaxMargin { get; set; }
        public double NetProfitMargin { get; set; }
        public double NetProfitMarginMother { get; set; }
        public double EPSBeforeTax { get; set; }
        public double EPS { get; set; }
        public double ROE_Session { get; set; }
        public double ROE_Year { get; set; }
        public double ROA_Session { get; set; }
        public double ROA_Year { get; set; }

      
    }
}
