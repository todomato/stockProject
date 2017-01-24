using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockForm.Model
{

    public partial class StockEntities : DbContext
    {
        /// <summary>
        /// 進資料庫前攔截
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                var fullName = entry.Entity.GetType().FullName;

                // 更新
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(new
                    {
                        ModifiedTime = DateTime.Now,
                    });
                }

                // 新增
                if (entry.State == EntityState.Added)
                {

                    entry.CurrentValues.SetValues(new
                    {
                        CreateTime = DateTime.Now,
                        ModifiedTime = DateTime.Now,
                    });
                }
            }

            return base.SaveChanges();
        }
    }
}
