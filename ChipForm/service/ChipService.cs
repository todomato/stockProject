using AutoMapper;
using ChipForm.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipForm.service
{
    public class ChipService
    {
        private StockEntities db;

        //新增股權分布
        public void AddChip(List<ChipModel> data)
        {
            using (db = new StockEntities())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;

                //取得資料庫已有日期資料
                int code = int.Parse(data[0].Code);
                string infoDate = data[0].InfoDate;
                var model = db.Chip.AsNoTracking()
                    .Where(c => c.InfoDate == infoDate)
                    .ToList();

                var list = new List<Chip>();
                Mapper.Initialize(cfg => cfg.CreateMap<ChipModel, Chip>());

                //比對不重複data
                if (model.Count == 0)
                {
                    foreach (ChipModel temp in data)
                    {
                        var item = Mapper.Map<Chip>(temp);
                        list.Add(item);
                    }

                    //新增
                    db.Chip.AddRange(list);
                    db.SaveChanges();
                }
            }
        }

        //新增三大法人
        public void AddInstitution(List<InstitutionalModel> data)
        {
            using (db = new StockEntities())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;


                Mapper.Initialize(cfg => cfg.CreateMap<InstitutionalModel, Institution>());

                var list = Mapper.Map<List<Institution>>(data);
                //新增
                db.Institution.AddRange(list);
                db.SaveChanges();

            }
        }

        public List<string> GetChipDateList()
        {
            List<string> r = new List<string>();
            using (db = new StockEntities())
            {
                r = db.Database.SqlQuery<string>(@"select distinct InfoDate from Chip").ToList();
            }

            return r;
        }

        public List<string> GetinstitutionDateListMarket1()
        {
            List<string> r = new List<string>();
            using (db = new StockEntities())
            {
                r = db.Database.SqlQuery<string>(@"select distinct InfoDate from Institution where Category = '上市'").ToList();
            }

            return r;
        }

        public List<string> GetinstitutionDateListMarket2()
        {
            List<string> r = new List<string>();
            using (db = new StockEntities())
            {
                r = db.Database.SqlQuery<string>(@"select distinct InfoDate from Institution where Category = '上櫃'").ToList();
            }

            return r;
        }
    }
}
