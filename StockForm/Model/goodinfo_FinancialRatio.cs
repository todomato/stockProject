//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockForm.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class goodinfo_FinancialRatio
    {
        public string Session { get; set; }
        public string ID { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal OperatingProfitMargin { get; set; }
        public decimal EarningBeforeTaxMargin { get; set; }
        public decimal NetProfitMargin { get; set; }
        public decimal NetProfitMarginMother { get; set; }
        public decimal EPSBeforeTax { get; set; }
        public decimal EPS { get; set; }
        public decimal ROE_Session { get; set; }
        public decimal ROE_Year { get; set; }
        public decimal ROA_Session { get; set; }
        public decimal ROA_Year { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
    }
}
