using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipForm.model
{
    public class InstitutionalModel
    {
        public string Code { get; set; }    //1
        public string Category { get; set; }    //上市櫃
        public string Type { get; set; }    //week
        public string InfoDate { get; set; }    //
        public decimal ForeignBuy { get; set; }     //3
        public decimal ForeignSell { get; set; }    //4
        public decimal ForeignNet { get; set; }     //5
        public decimal DomesticBuy { get; set; }   //6 
        public decimal DomesticSell { get; set; }   //7
        public decimal DomesticNet { get; set; }    //8

        public decimal DealerNet { get; set; }  //9
        public decimal TotalNet { get; set; }   //16
        
    }
}
