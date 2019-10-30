using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.Core.Data.Data.Extensions
{
    public static class DbContextExtensions
    {
        //public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        //{
        //    foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        //    {
        //        entity.SetTableName(entity.DisplayName());
        //    }
        //}

        //public static void RemoveFromTableName(this ModelBuilder modelBuilder, string replace)
        //{
        //    foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        //    {
        //        var tableName = entity.GetTableName();
        //        entity.SetTableName(tableName.Replace(replace, ""));
        //    }
        //}



        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
