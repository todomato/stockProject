using StockForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Service
{
    public class AnalysisService
    {
        private StockEntities db;

        public void Search(string stockId, string session)
        {
            using (db = new StockEntities())
            {
                var revenue = db.goodinfo_Revenues.Where(c => c.ID == stockId).ToList();
                var financialRatio = db.goodinfo_FinancialRatio.Where(c => c.ID == stockId).ToList();
                var kSession = db.goodinfo_KSession.Where(c => c.ID == stockId).ToList();

                EpsPriceCaculator caculator = new EpsPriceCaculator(session, revenue, financialRatio, kSession);
                caculator.GetPredictResult();

            }
        }

    }
}
