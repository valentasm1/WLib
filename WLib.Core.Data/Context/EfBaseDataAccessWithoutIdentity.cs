using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WLib.Core.Bll.DataAccess;
using WLib.Core.Bll.DataAccess.Model;

namespace WLib.Core.Data.Context
{
    public class EfBaseDataAccessWithoutIdentity : DbContext, IBaseDataAccess, IDisposable
    {
        public EfBaseDataAccessWithoutIdentity()
        {
        }

        public EfBaseDataAccessWithoutIdentity(DbContextOptions nameOrConnectionString)
          : base(nameOrConnectionString)
        {
        }


        public IEnumerable<TResult> SetOf<T, TResult>(DbContext dbContext, Func<T, TResult> func) where T : class where TResult : class
        {
            return dbContext.GetType().Assembly.GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface)
                .SelectMany(t => Enumerable.Cast<T>(dbContext.Set<T>())).Select(func);
        }

        public virtual IQueryable<T> GetAllReadOnly<T>(params Expression<Func<T, object>>[] includes) where T : class
        {

            return this.GetAll<T>(includes).AsNoTracking<T>();
        }

        public virtual IQueryable<TKey> GetAll<TKey>(params Expression<Func<TKey, object>>[] includes) where TKey : class
        {
            IQueryable<TKey> source = (IQueryable<TKey>)this.Set<TKey>();
            if (includes != null)
            {
                foreach (Expression<Func<TKey, object>> include in includes)
                    source = source.Include<TKey, object>(include);
            }
            return source;
        }

        protected virtual async Task<TKey> InsertAsync<TKey>(TKey entity) where TKey : class
        {
            var item = await this.Set<TKey>().AddAsync(entity);
            return item.Entity;
        }

        public virtual async Task InsertRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await this.Set<T>().AddRangeAsync(entities);
        }

        protected virtual T Update<T, TItem>(T entity, Expression<Func<T, TItem>> onlyFields) where T : class where TItem : class
        {
            if (onlyFields == null)
            {
                var saved = this.Update<T>(entity);
                return saved.Entity;
            }
            else
            {
                var entry1 = this.Entry<T>(entity);
                if (entry1.State == EntityState.Detached)
                {
                    T entity1 = typeof(T).IsSubclassOf(typeof(BaseEntity)) ? this.Set<T>().Local.SingleOrDefault<T>((Func<T, bool>)(e => ((object)e as BaseEntity).Id == ((object)(T)entity as BaseEntity).Id)) : default(T);
                    if ((object)entity1 != null)
                    {
                        var entry2 = this.Entry<T>(entity1);
                        this.MarkPropertiesAsMoidified<T, TItem>(entry2, onlyFields);
                        foreach (MemberInfo member in ((NewExpression)onlyFields.Body).Members)
                        {
                            entry2.Property(member.Name).CurrentValue = entry1.Property(member.Name).CurrentValue;
                            entry2.Property(member.Name).IsModified = true;
                        }
                    }
                    else
                    {
                        this.Set<T>().Attach(entity);
                        this.MarkPropertiesAsMoidified<T, TItem>(entry1, onlyFields);
                    }
                }
                else
                {
                    this.MarkPropertiesAsMoidified<T, TItem>(entry1, onlyFields);
                }

                return entry1.Entity;
            }
        }

        public virtual async Task<TKey> InsertOrUpdateAsync<TKey, O>(TKey entity, Expression<Func<TKey, O>> onlyFields = null) where TKey : BaseEntity, new() where O : class
        {
            if (entity.Id == 0)
            {
                var saved = await this.InsertAsync<TKey>(entity);
                return saved;
            }
            else
            {
                var saved = this.Update<TKey, O>(entity, onlyFields);
                return saved;
            }
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            var aaa = this.Entry<T>(entity);
            if (this.Entry<T>(entity).State == EntityState.Detached)
                this.Set<T>().Attach(entity);
            this.Set<T>().Remove(entity);
        }

        public virtual void Delete<T>(int id) where T : BaseEntity, new()
        {
            T instance = Activator.CreateInstance<T>();
            instance.Id = id;
            this.Delete<T>(instance);
        }

        public virtual async Task<int> SaveAsync()
        {

            var validationErrors = ChangeTracker
                .Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success);

            if (validationErrors.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var entityValidationError in validationErrors)
                {
                    stringBuilder.AppendFormat("{0} failed validation\n", (object)entityValidationError.ErrorMessage);
                    foreach (var validationError in entityValidationError.MemberNames)
                    {
                        stringBuilder.AppendFormat("- {0} : {1}", (object)validationError);//, (object)validationError.ErrorMessage);
                        stringBuilder.AppendLine();
                    }
                }
                // Possibly throw an exception here
                throw new ValidationException(stringBuilder.ToString());
            }

            return await base.SaveChangesAsync();
        }

        public virtual void Rollback()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        continue;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        continue;
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        continue;
                    default:
                        continue;
                }
            }
        }

        public int SqlExecute(string sql, params object[] parameters)
        {
            return this.Database.ExecuteSqlRaw(sql, parameters);
        }


        public virtual async Task<int> SqlExecuteAsync(string sql, params object[] parameters)
        {
            return await this.Database.ExecuteSqlRawAsync(sql, parameters);
        }


        protected virtual void MarkPropertiesAsMoidified<T, O>(EntityEntry<T> entry, Expression<Func<T, O>> onlyFields) where T : class where O : class
        {
            if (onlyFields.NodeType != ExpressionType.Lambda || onlyFields.Body.NodeType != ExpressionType.New)
                throw new Exception("Bad onlyFields expression, must be like: q=> new { q.Id, q.Name} ");
            foreach (MemberInfo member in ((NewExpression)onlyFields.Body).Members)
                entry.Property(member.Name).IsModified = true;
        }


    }
}
