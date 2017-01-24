using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipForm.model
{
    public class ChipModel
    {
        private string id;
        private string str;

        public ChipModel()
        {

        }

        public ChipModel(string id, string str)
        {
            //解析字串
            List<string> list = str.Split(new string[] { "\r\n"  }, StringSplitOptions.None).Select(c => c.Trim()).ToList();
            Code = id;
            Num = int.Parse(list[0]);
            StockLevel = list[1];
            People = int.Parse(list[2].Replace(",",""));
            ChipCount = long.Parse(list[3].Replace(",", ""));
            Ratio = double.Parse(list[4]);
        }

        /// <summary>
        /// 設定總和資訊
        /// </summary>
        /// <param name="str"></param>
        public void SetTotal(string str, string date)
        {
            //解析字串
            List<string> list = str.Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(c => c.Trim()).ToList();
            TotalPeople = int.Parse(list[2].Replace(",", ""));
            TotalChipCount = long.Parse(list[3].Replace(",", ""));
            TotalRatio = double.Parse(list[4]);
            InfoDate = date;
        }

        public string Code { get; set; }
        public string InfoDate { get; set; }
        public int Num { get; set; }
        public string StockLevel { get; set; }
        public int People { get; set; }
        public long ChipCount { get; set; }
        public double Ratio { get; set; }

        public int TotalPeople { get; set; }
        public long TotalChipCount { get; set; }
        public double TotalRatio { get; set; }
    }
}
