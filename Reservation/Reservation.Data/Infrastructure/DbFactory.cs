using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        public ReservationEntities dbContext;

        public ReservationEntities Init()
        {
            return dbContext ?? (dbContext = new ReservationEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
