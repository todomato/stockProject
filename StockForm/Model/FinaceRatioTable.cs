using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class FinaceRatioTable
    {
        public int Id { get; set; }
        public string Title { get; set; }   //網站欄位名稱
        public string Field { get; set; }   //資料庫欄位名稱

    }

    //財務比率對照表
    public class FinaceRatioList
    {
        private static List<FinaceRatioTable> Table = new List<FinaceRatioTable>(){
            new FinaceRatioTable() { Id = 1, Title = "獲利能力", Field = "" },
            new FinaceRatioTable() { Id = 2, Title = "毛利率", Field = "GrossMargin" },
            new FinaceRatioTable() { Id = 3, Title = "營業利益率", Field = "OperatingProfitMargin" },
            new FinaceRatioTable() { Id = 4, Title = "稅前淨利率", Field = "EarningBeforeTaxMargin" },
            new FinaceRatioTable() { Id = 5, Title = "稅後淨利率", Field = "NetProfitMargin" },
            new FinaceRatioTable() { Id = 6, Title = "稅後淨利率", Field = "NetProfitMarginMother" },
            new FinaceRatioTable() { Id = 7, Title = "每股稅前盈餘", Field = "EPSBeforeTax" },
            new FinaceRatioTable() { Id = 8, Title = "每股稅後盈餘", Field = "EPS" },
            new FinaceRatioTable() { Id = 9, Title = "股東權益報酬率", Field = "ROE_Session" },
            new FinaceRatioTable() { Id = 10, Title = "股東權益報酬率", Field = "ROE_Year" },
            new FinaceRatioTable() { Id = 11, Title = "資產報酬率", Field = "ROA_Session" },
            new FinaceRatioTable() { Id = 12, Title = "資產報酬率", Field = "ROA_Year" },
        };

        public static string GetTitleById(int id)
        {
            return Table.Where(c => c.Id == id).Select(c => c.Title).Single();
        }

        public static string GetFieldById(int id)
        {
            return Table.Where(c => c.Id == id).Select(c => c.Field).Single();
        }

    }
}
