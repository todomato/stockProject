using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{
    public class EpsPriceCaculator
    {
        double high_PE = 0; //平均高pe
        double low_PE = 0;  //平均低pe
        double system_PE = 0;   //系統pe
        double stockCount = 1;  //發行
        double session_revenue = 0; //季營收
        double netProfitMargin = 0; //預估竟利率
        double netProfit = 0; //預估竟利
        double eps = 0; //預估eps
        double new4eps = 0; //預估近四季eps
        string session; //預估近四季eps

        public List<goodinfo_Revenues> Revenues { get; set; }
        public List<goodinfo_FinancialRatio> FinancialRatio { get; set; }
        public List<goodinfo_KSession> KSession { get; set; }

        public EpsPriceCaculator(string s, List<goodinfo_Revenues> r, List<goodinfo_FinancialRatio> f, List<goodinfo_KSession> k)
        {
            this.session = s;
            this.Revenues = r;
            this.FinancialRatio = f;
            this.KSession = k;
        }

        //預估結果
        public string GetPredictResult()
        {
            GetAverageHighPE(session);
            GetAverageLowPE(session);
            GetSystemPE(session);
            GetPredictEPS(session);

            double 預估高點 = (high_PE * new4eps);
            double 預估低點 = (low_PE * new4eps);
            double 大跌低點 = (system_PE * new4eps);
            double 預估買進 = (預估低點 + 大跌低點) / 2;

            string result = string.Format(@"
                預估高點 : {0}
                預估低點 : {0}
                大跌低點 : {0}
                預估買進 : {0}
            ", 預估高點, 預估低點, 大跌低點, 預估買進);

            return result;
        }

        //TODO 取得預估四季EPS
        private void GetPredictEPS(string session)
        {
            //TODO 取得預估季營收

            //TODO 取得稅後淨利率

            //預估淨利
            netProfit = session_revenue * netProfitMargin;

            //TODO 取得發行股數
            eps = netProfit / stockCount;

            //取得近三季eps和
            var epslast3session = GetSumEPSOfRecently3Session(session);
            new4eps = (eps + epslast3session);
        }

        //取得平均高PE
        private void GetAverageHighPE(string session)
        {
            //取得近四季文字
            List<string> sessionList = GetSessionStr(session);
            double totalPE = 0;

            foreach (var s in sessionList)
            {
                double eps = GetEPSOfRecently4Session(s);
                var dhigh = KSession.Where(c => c.Session == session.Substring(2, 4)).Select(c => c.kHigh).Single();
                double high = double.Parse(dhigh.ToString());
                double pe = high / eps;
                totalPE += pe;
            }

            high_PE = totalPE / sessionList.Count;
        }

        //取得平均低PE
        private void GetAverageLowPE(string session)
        {
            //取得近四季文字
            List<string> sessionList = GetSessionStr(session);
            double totalPE = 0;

            foreach (var s in sessionList)
            {
                double eps = GetEPSOfRecently4Session(s);
                var dlow = KSession.Where(c => c.Session == session.Substring(2, 4)).Select(c => c.kLow).Single();
                double low = double.Parse(dlow.ToString());
                double pe = low / eps;
                totalPE += pe;
            }

            low_PE = totalPE / sessionList.Count;
        }

        //取得系統PE
        private void GetSystemPE(string session)
        {
            //取得近四季文字
            List<string> sessionList = GetSessionStr(session);
            List<double> peList = new List<double>();

            foreach (var s in sessionList)
            {
                double eps = GetEPSOfRecently4Session(s);
                var dlow = KSession.Where(c => c.Session == session.Substring(2, 4)).Select(c => c.kLow).Single();
                double low = double.Parse(dlow.ToString());
                double pe = low / eps;
                peList.Add(pe);
            }

            system_PE = peList.Min(c => c);
        }

        //取得近四季EPS
        //session : 20xxQx
        private double GetEPSOfRecently4Session(string session)
        {
            //取得近四季文字
            List<string> sessionList = GetSessionStr(session);

            //近四季eps和
            var sumEps = SumEPS(sessionList);

            return sumEps;
        }

        //取得近三季EPS
        private double GetSumEPSOfRecently3Session(string session)
        {
            //取得近三季文字
            List<string> sessionList = GetSessionStr(session, 3);

            //近四季eps和
            var sumEps = SumEPS(sessionList);

            return sumEps;
        }

        //計算EPS和
        private double SumEPS(List<string> sessionList)
        {
            double total = 0;

            //計算EPS
            foreach (var item in sessionList)
            {
                var eps = this.FinancialRatio.Where(c => c.Session == item).Select(c => c.EPS).SingleOrDefault();
                total += double.Parse(eps.ToString());
            }

            return total ;
        }

        //TODO 取得近四季文字
        private List<string> GetSessionStr(string session, int howManySession = 4)
        {
            List<string> result = new List<string>();
            result.Add(session);

            //拆解
            int year = int.Parse(session.Substring(0, 4));
            int currentSession = int.Parse(session.Substring(5, 1));

            for (int i = 0; i < howManySession - 1; i++)
            {
                if (currentSession - 1 == 0)
                {
                    currentSession = 4;
                    year = year - 1;
                    string data = string.Format("{0}Q{1}", year, currentSession);
                    result.Add(data);
                }
                else
                {
                    currentSession = currentSession - 1;
                    string data = string.Format("{0}Q{1}", year, currentSession);
                    result.Add(data);
                }
            }

            return result;
        }

    }
}
