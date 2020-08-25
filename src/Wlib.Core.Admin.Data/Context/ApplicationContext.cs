using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Wlib.Core.Admin.Data.Domain;
using Wlib.Core.Admin.Data.Domain.Authentication;
using Wlib.Core.Admin.Data.Domain.Entities;
using WLib.Core.Bll.Model.Meta;

namespace Wlib.Core.Admin.Data.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                if (type.FullName.Contains("BaseEntityTypeConfiguration")) continue;

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        /// <summary>
        /// Finds by primary key
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Find<TEntity>(int id) where TEntity : ApplicationEntityBase
        {
            var all = await GetAll<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            return all;
        }

        public virtual async Task<TEntity> FindAsTracking<TEntity>(int id) where TEntity : ApplicationEntityBase
        {
            var all = await GetAllAsTracking<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            return all;
        }
        public virtual IQueryable<TEntity> GetAll<TEntity>() where TEntity : ApplicationEntityBase
        {
            var items = this.Set<TEntity>();

            return items.Where(x => !x.Deleted).Where(x => x.Active).AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllAsTracking<TEntity>() where TEntity : ApplicationEntityBase
        {
            var items = this.Set<TEntity>();

            return items.Where(x => !x.Deleted).Where(x => x.Active);
        }
        public async Task InsertOrUpdateAsync<TEntity>(TEntity enity) where TEntity : ApplicationEntityBase
        {
            if (enity.Id == 0)
            {
                enity.Active = true;
                enity.Deleted = false;
                enity.ChangeDate = DateTimeOffset.Now;
                enity.CreateDate = DateTimeOffset.Now;
                await AddAsync(enity);
            }
            else
            {
                enity.ChangeDate = DateTimeOffset.Now;
                Update(enity);
            }
        }

        //private void SetOtherFields()
        //{
        //    var modifiedEntries = ChangeTracker.Entries<Kosmos.Core.Contracts.Models.Contracts.IDeletable>()
        //        .Where(e => e.State == EntityState.Added);

        //    foreach (EntityEntry<Kosmos.Core.Contracts.Models.Contracts.IDeletable> auditableEntity in modifiedEntries)
        //    {
        //        if (auditableEntity.Entity is Kosmos.Core.Contracts.Models.Contracts.IDeletable entity)
        //        {
        //            entity.Active = true;
        //        }
        //    }
        //}

        //TODO fix with date fields with current entities
        public void AddAuditInfo()
        {
            var modifiedEntries = ChangeTracker.Entries<IAuditEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<IAuditEntity> auditableEntity in modifiedEntries)
            {
                if (auditableEntity.Entity is IEntity dbModel)
                {
                    auditableEntity.Entity.ChangeDate = DateTimeOffset.Now;
                    if (dbModel.Id == 0)
                    {
                        auditableEntity.Entity.CreateDate = DateTimeOffset.Now;
                    }
                    else
                    {
                        var createProp = auditableEntity.Property(x => x.CreateDate);
                        if (createProp != null)
                        {
                            createProp.IsModified = false;
                        }
                    }

                }

            }
        }

        /// <summary>
        /// Main point for saving data with audit info
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            AddAuditInfo();
            await SaveChangesAsync(true);
        }

    }
}