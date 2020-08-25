using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wlib.Core.Admin.Data.Context;

namespace Kosmos.Core.Data.Domain
{
    public static class DbExtensions
    {
        public static async Task EnsureSeeded(this ApplicationContext context)
        {

            //if (!context.GetAll<TenantEntity>().Any(x => x.Name == "Lithuania"))
            //{
            //    await context.InsertOrUpdateAsync(new TenantEntity()
            //    {
            //        Name = "Lithuania"
            //    });

            //    await context.SaveAsync();
            //}
        }
    }

}
