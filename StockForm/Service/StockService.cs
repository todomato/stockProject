using AutoMapper;
using StockForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Service
{
    public class StockService
    {
        private StockEntities db;

        //新增更新營收
        public void UpdateRevenue(string id, List<RevenueModel> data)
        {
            using (db = new StockEntities())
            {
                var model = db.goodinfo_Revenues.Where(c => c.ID == id).ToList();

                var list = new List<goodinfo_Revenues>();
                Mapper.Initialize(cfg => cfg.CreateMap<RevenueModel, goodinfo_Revenues>());
                foreach (RevenueModel temp in data)
                {
                    //比對不重複data
                    if (!model.Exists(c => c.Date == temp.Date))
                    {
                        var item = Mapper.Map<goodinfo_Revenues>(temp);
                        list.Add(item);
                    }
                }

                db.goodinfo_Revenues.AddRange(list);
                db.SaveChanges();
            }
        }

        //更新季K
        public void UpdateKSession(string id, List<KSessionModel> data)
        {
            using (db = new StockEntities())
            {
                var model = db.goodinfo_KSession.Where(c => c.ID == id).ToList();

                var list = new List<goodinfo_KSession>();
                Mapper.Initialize(cfg => cfg.CreateMap<KSessionModel, goodinfo_KSession>());
                foreach (KSessionModel temp in data)
                {
                    //比對不重複data
                    if (!model.Exists(c => c.Session == temp.Session))
                    {
                        var item = Mapper.Map<goodinfo_KSession>(temp);
                        list.Add(item);
                    }
                }

                db.goodinfo_KSession.AddRange(list);
                db.SaveChanges();
            }
        }

        //更新財務比率
        public void UpdateFinaceRatio(string id, List<FinaceRatioModel> data)
        {
            using (db = new StockEntities())
            {
                var model = db.goodinfo_FinancialRatio.Where(c => c.ID == id).ToList();

                var list = new List<goodinfo_FinancialRatio>();
                Mapper.Initialize(cfg => cfg.CreateMap<FinaceRatioModel, goodinfo_FinancialRatio>());
                foreach (FinaceRatioModel temp in data)
                {
                    //比對不重複data
                    if (!model.Exists(c => c.Session == temp.Session))
                    {
                        var item = Mapper.Map<goodinfo_FinancialRatio>(temp);
                        list.Add(item);
                    }
                }

                db.goodinfo_FinancialRatio.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
