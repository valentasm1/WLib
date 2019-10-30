using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WLib.Core.Bll.DataAccess.Model;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Data.Context
{

    public partial class BaseDataAccess<TUser> : IdentityDbContext<TUser> where TUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public BaseDataAccess(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }


        }

        public virtual IQueryable<TEntity> GetAll<TEntity>() where TEntity : DeletableAuditBaseEntity
        {
            var items = this.Set<TEntity>();

            return items.Where(x => !x.Deleted).AsNoTracking();
        }

        public async Task InsertOrUpdateAsync<TEntity>(TEntity enity) where TEntity : BaseEntity
        {
            if (enity.Id == 0)
            {
                await AddAsync(enity);
            }
            else
            {
                Update(enity);
            }
        }

        private void AddAuditInfo()
        {
            var modifiedEntries = ChangeTracker.Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<IAuditable> auditableEntity in modifiedEntries)
            {
                if (auditableEntity.Entity is IEntity dbModel)
                {
                    auditableEntity.Entity.ChangeDate = DateTime.Now;
                    if (dbModel.Id == 0)
                    {
                        auditableEntity.Entity.CreateDate = DateTime.Now;
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

        public async Task SaveAsync()
        {
            AddAuditInfo();
            await SaveChangesAsync(true);
        }

    }
    //public partial class EfBaseDataAccess<TUser> : IdentityDbContext<TUser>, IDataAccess
    //    where TUser : Microsoft.AspNetCore.Identity.IdentityUser
    //{
    //    //https://stackoverflow.com/questions/19902756/asp-net-identity-dbcontext-confusion

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //    }

    //    public EfBaseDataAccess(DbContextOptions existingConnection)
    //        : base(existingConnection)
    //    { }

    //    public virtual IQueryable<T> GetAllReadOnly<T>(params Expression<Func<T, object>>[] includes) where T : class
    //    {
    //        return this.GetAll<T>(includes).AsNoTracking<T>();
    //    }

    //    public virtual IQueryable<TKey> GetAll<TKey>(params Expression<Func<TKey, object>>[] includes) where TKey : class
    //    {
    //        IQueryable<TKey> source = GetAllInternal(includes);
    //        if (includes != null)
    //        {
    //            foreach (Expression<Func<TKey, object>> include in includes)
    //                source = source.Include<TKey, object>(include);
    //        }

    //        //if (typeof(TKey).GetInterfaces().Contains(typeof(IDeletable)))
    //        //{
    //        //    Expression<Func<TKey, bool>> predicate = x => !((IDeletable)x).Deleted;
    //        //    predicate = EntityCastRemoverVisitor.Convert(predicate);
    //        //    source = source.Where(x => !((IDeletable)x).Deleted);
    //        //}

    //        //if (typeof(TKey).GetInterfaces().Contains(typeof(IOrderable)))
    //        //{
    //        //    Expression<Func<TKey, int?>> orderByClause = x => ((IOrderable)x).OrderBy;
    //        //    orderByClause = EntityCastRemoverVisitor.Convert(orderByClause);
    //        //    source = source.OrderBy(orderByClause);
    //        //}
    //        return source;
    //    }

    //    private IQueryable<TKey> GetAllInternal<TKey>(params Expression<Func<TKey, object>>[] includes) where TKey : class
    //    {
    //        IQueryable<TKey> source = (IQueryable<TKey>)this.Set<TKey>();
    //        if (includes != null)
    //        {
    //            foreach (Expression<Func<TKey, object>> include in includes)
    //                source = source.Include<TKey, object>(include);
    //        }

    //        return source;
    //    }


    //    protected virtual async Task<TKey> InsertAsync<TKey>(TKey entity) where TKey : class
    //    {
    //        var item = this.Update(entity);

    //        //var item = await this.Set<TKey>().AddAsync(entity);
    //        return item.Entity;
    //    }


    //    protected virtual T Update<T, TItem>(T entity, Expression<Func<T, TItem>> onlyFields) where T : class where TItem : class
    //    {
    //        if (onlyFields == null)
    //        {
    //            var saved = this.Update<T>(entity);
    //            return saved.Entity;
    //        }
    //        else
    //        {
    //            var entry1 = this.Entry<T>(entity);
    //            if (entry1.State == EntityState.Detached)
    //            {
    //                T entity1 = typeof(T).IsSubclassOf(typeof(BaseEntity)) ? this.Set<T>().Local.SingleOrDefault<T>((Func<T, bool>)(e => ((object)e as BaseEntity).Id == ((object)(T)entity as BaseEntity).Id)) : default(T);
    //                if ((object)entity1 != null)
    //                {
    //                    var entry2 = this.Entry<T>(entity1);
    //                    this.MarkPropertiesAsMoidified<T, TItem>(entry2, onlyFields);
    //                    foreach (MemberInfo member in ((NewExpression)onlyFields.Body).Members)
    //                    {
    //                        entry2.Property(member.Name).CurrentValue = entry1.Property(member.Name).CurrentValue;
    //                        entry2.Property(member.Name).IsModified = true;
    //                    }
    //                }
    //                else
    //                {
    //                    this.Set<T>().Attach(entity);
    //                    this.MarkPropertiesAsMoidified<T, TItem>(entry1, onlyFields);
    //                }
    //            }
    //            else
    //            {
    //                this.MarkPropertiesAsMoidified<T, TItem>(entry1, onlyFields);
    //            }

    //            return entry1.Entity;
    //        }
    //    }



    //    public virtual async Task<TKey> InsertOrUpdateAsync<TKey, O>(TKey entity, Expression<Func<TKey, O>> onlyFields = null) where TKey : BaseEntity, new() where O : class
    //    {
    //        if (entity.Id == 0)
    //        {
    //            var saved = await this.InsertAsync<TKey>(entity);
    //            return saved;
    //        }
    //        else
    //        {
    //            var saved = this.Update<TKey, O>(entity, onlyFields);
    //            return saved;
    //        }
    //    }

    //    public virtual void Delete<T>(T entity) where T : class
    //    {
    //        if (this.Entry<T>(entity).State == EntityState.Detached)
    //            this.Set<T>().Attach(entity);
    //        this.Set<T>().Remove(entity);
    //    }



    //    public virtual void Delete<T>(int id) where T : BaseEntity, new()
    //    {
    //        if (GetAll<T>().SingleOrDefault(x => x.Id == id) is IDeletable entity)
    //        {
    //            entity.Deleted = true;
    //        }
    //    }

    //    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //    {

    //        AddAuditInfo();
    //        return base.SaveChangesAsync(cancellationToken);
    //    }

    //    public virtual async Task<int> SaveAsync()
    //    {

    //        var validationErrors = ChangeTracker
    //            .Entries<IValidatableObject>()
    //            .SelectMany(e => e.Entity.Validate(null))
    //            .Where(r => r != ValidationResult.Success).ToImmutableList();

    //        if (validationErrors.Any())
    //        {
    //            var stringBuilder = new StringBuilder();
    //            foreach (var entityValidationError in validationErrors)
    //            {
    //                stringBuilder.AppendFormat("{0} failed validation\n", (object)entityValidationError.ErrorMessage);
    //                foreach (var validationError in entityValidationError.MemberNames)
    //                {
    //                    stringBuilder.AppendFormat("- {0} : {1}", (object)validationError);//, (object)validationError.ErrorMessage);
    //                    stringBuilder.AppendLine();
    //                }
    //            }
    //            // Possibly throw an exception here
    //            throw new ValidationException(stringBuilder.ToString());
    //        }

    //        foreach (var entry in this.ChangeTracker.Entries().Where(o => o.State == EntityState.Added || o.State == EntityState.Modified))
    //        {
    //            if (entry.Entity is IAuditable changedEntity)
    //            {

    //                var now = DateTime.Now;
    //                changedEntity.ChangeDate = now.AddMilliseconds(-now.Millisecond);
    //                //changedEntity.ChangeUserId = workContext?.CurrentUser?.Id ?? 1;

    //                if (entry.State == EntityState.Modified)
    //                {
    //                    this.Update(changedEntity, f => new { f.ChangeDate, f.ChangeUserId });
    //                }
    //            }
    //        }

    //        return await SaveChangesAsync(true);
    //    }

    //    public override int SaveChanges()
    //    {
    //        AddAuditInfo();
    //        return base.SaveChanges();
    //    }

    //    public virtual void Rollback()
    //    {
    //        foreach (var entry in this.ChangeTracker.Entries())
    //        {
    //            switch (entry.State)
    //            {
    //                case EntityState.Added:
    //                    entry.State = EntityState.Detached;
    //                    continue;
    //                case EntityState.Deleted:
    //                    entry.State = EntityState.Unchanged;
    //                    continue;
    //                case EntityState.Modified:
    //                    entry.CurrentValues.SetValues(entry.OriginalValues);
    //                    entry.State = EntityState.Unchanged;
    //                    continue;
    //                default:
    //                    continue;
    //            }
    //        }
    //    }

    //    public int SqlExecute(string sql, params object[] parameters)
    //    {
    //        return this.Database.ExecuteSqlRaw(sql, parameters);
    //    }


    //    public virtual async Task<int> SqlExecuteAsync(string sql, params object[] parameters)
    //    {
    //        return await this.Database.ExecuteSqlRawAsync(sql, parameters);
    //    }



    //    protected virtual void MarkPropertiesAsMoidified<T, TOnlyFieldsType>(EntityEntry<T> entry, Expression<Func<T, TOnlyFieldsType>> onlyFields) where T : class where TOnlyFieldsType : class
    //    {
    //        if (onlyFields.NodeType != ExpressionType.Lambda || onlyFields.Body.NodeType != ExpressionType.New)
    //            throw new Exception("Bad onlyFields expression, must be like: q=> new { q.Id, q.Name} ");
    //        foreach (MemberInfo member in ((NewExpression)onlyFields.Body).Members)
    //            entry.Property(member.Name).IsModified = true;
    //    }

    //    private void AddAuditInfo()
    //    {
    //        var modifiedEntries = ChangeTracker.Entries<IAuditable>()
    //            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    //        foreach (EntityEntry<IAuditable> auditableEntity in modifiedEntries)
    //        {
    //            auditableEntity.Entity.ChangeDate = DateTime.UtcNow;

    //            if (auditableEntity.State == EntityState.Added)
    //            {
    //                auditableEntity.Entity.CreateDate = DateTime.Now;
    //            }
    //        }
    //    }

    //    public async Task<int> SaveWithoutTrackingAsync()
    //    {
    //        AddAuditInfo();
    //        return await SaveAsync();
    //    }

    //    public IQueryable<T> GetAllIncludingDeleted<T>(params Expression<Func<T, object>>[] includes)
    //        where T : class
    //    {
    //        return GetAllInternal(includes);
    //    }

    //    public void Detach<T>(T obj)
    //        where T : class
    //    {
    //        var entry = this.Entry(obj);

    //        entry.State = EntityState.Detached;
    //    }

    //    public async Task<int> ValidateAndSaveAsync(bool noTracking = false)
    //    {
    //        var result = new DbServiceResult();
    //        result.Success = true;

    //        foreach (var entry in this.ChangeTracker.Entries())
    //        {
    //            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
    //            {
    //                result.AddErrors(this.ValidateChangedProperties(entry));
    //            }
    //        }

    //        if (!result.Success)
    //        {
    //            var exception = new WLib.Core.Services.Exceptions.ValidationException();
    //            foreach (var errors in result)
    //            {
    //                foreach (var err in errors.Value)
    //                {
    //                    exception.AddError(errors.Key, err);
    //                }
    //            }

    //            throw exception;
    //        }

    //        if (noTracking)
    //        {
    //            return await this.SaveWithoutTrackingAsync();
    //        }
    //        else
    //        {
    //            return await this.SaveAsync();
    //        }
    //    }

    //    protected virtual bool ShouldSkipProperty(PropertyDescriptor property)
    //    {
    //        if (property.PropertyType.IsGenericType &&
    //            (property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
    //             property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>) ||
    //             property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
    //        {
    //            return true;
    //        }

    //        if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
    //        {
    //            return true;
    //        }

    //        MethodInfo setMethod = property.ComponentType.GetProperty(property.Name).GetSetMethod();

    //        if (setMethod == null)
    //        {
    //            return true;
    //        }

    //        if (property.PropertyType.IsInstanceOfType(GetType()))
    //        {
    //            return true;
    //        }

    //        //Id is not updated
    //        //if (property.Name.Equals("Id"))
    //        //{
    //        //    return true;
    //        //}

    //        return false;
    //    }

    //    protected virtual void MarkPropertiesAsModified<T>(EntityEntry<T> entry, T entity)
    //        where T : class
    //    {
    //        ////var results = new List<ValidationResult>();
    //        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(entity))
    //        {
    //            if (this.ShouldSkipProperty(property))
    //            {
    //                continue;
    //            }

    //            if (property.Attributes[typeof(SuppressUpdateAttribute)] == null)
    //            {
    //                var prop = entry.Property(property.Name);
    //                if (property.Name.Equals("Id"))
    //                {
    //                    continue;
    //                }

    //                if (prop != null)
    //                {
    //                    prop.CurrentValue = property.GetValue(entity);
    //                    prop.IsModified = true;
    //                }
    //            }
    //        }
    //    }

    //    private DbServiceResult ValidateChangedProperties(EntityEntry entry)
    //    {
    //        var results = new List<ValidationResult>();
    //        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(entry.Entity))
    //        {
    //            if (this.ShouldSkipProperty(property))
    //            {
    //                continue;
    //            }

    //            var prop = entry.Property(property.Name);
    //            if (prop.IsModified)
    //            {
    //                Validator.TryValidateProperty(
    //                    prop.CurrentValue,
    //                    new ValidationContext(entry.Entity, null, null) { MemberName = property.Name, DisplayName = property.DisplayName },
    //                    results);
    //            }
    //        }

    //        var serviceResult = new DbServiceResult { Success = true };
    //        foreach (var result in results)
    //        {
    //            serviceResult.AddError(result.MemberNames.FirstOrDefault() ?? string.Empty, result.ErrorMessage);
    //        }

    //        return serviceResult;
    //    }

    //    public virtual async Task InsertOrUpdateAsync<T>(T entity) where T : BaseEntity, new()
    //    {
    //        if (entity.Id == default(int))
    //        {
    //            await this.InsertAsync(entity);
    //        }
    //        else
    //        {
    //            this.Update(entity);
    //        }
    //    }

    //    /// <summary>
    //    /// Return entities from current db context based on current type. Type could be interface
    //    /// </summary>
    //    /// <typeparam name="TKey"></typeparam>
    //    /// <returns>If none type is found then empty</returns>
    //    public IQueryable<TKey> SetOf<TKey>() where TKey : class
    //    {
    //        var firstType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
    //            .FirstOrDefault(type => typeof(TKey).IsAssignableFrom(type) && !type.IsInterface);
    //        if (firstType == null) return Enumerable.Empty<TKey>().AsQueryable();

    //        var dbSetMethodInfo = typeof(DbContext).GetMethod("Set");
    //        var dbSet = dbSetMethodInfo.MakeGenericMethod(firstType);

    //        IQueryable<TKey> queryable = ((IQueryable)dbSet.Invoke(this, null)).Cast<TKey>();

    //        return queryable;//.ToList().Cast<TKey>();
    //    }

    //    internal sealed class DbServiceResult
    //    {
    //        private IDictionary<string, IList<string>> errors;

    //        public int Id { get; set; }

    //        public string Message { get; set; }

    //        public bool Success { get; set; }

    //        public Exception Exception { get; set; }

    //        public DbServiceResult()
    //        {
    //            this.errors = (IDictionary<string, IList<string>>)new Dictionary<string, IList<string>>();
    //        }

    //        public DbServiceResult(IEnumerable<KeyValuePair<string, string>> errors)
    //          : this()
    //        {
    //            foreach (KeyValuePair<string, string> error in errors)
    //                this.AddError(error.Key, error.Value);
    //        }

    //        //public DbServiceResult(ValidationResult validationResult)
    //        //  : this()
    //        //{
    //        //    using (IEnumerator<ValidationFailure> enumerator = ((IEnumerable<ValidationFailure>)validationResult.get_Errors()).GetEnumerator())
    //        //    {
    //        //        while (((IEnumerator)enumerator).MoveNext())
    //        //        {
    //        //            ValidationFailure current = enumerator.Current;
    //        //            this.AddError(current.get_PropertyName(), current.get_ErrorMessage());
    //        //        }
    //        //    }
    //        //}

    //        public DbServiceResult(DbServiceResult other)
    //          : this()
    //        {
    //            this.errors = (IDictionary<string, IList<string>>)new Dictionary<string, IList<string>>();
    //            foreach (KeyValuePair<string, IList<string>> keyValuePair in other)
    //                this.errors.Add(keyValuePair);
    //        }

    //        public DbServiceResult(WLib.Core.Services.Exceptions.ValidationException ex)
    //          : this()
    //        {
    //            this.errors = (IDictionary<string, IList<string>>)new Dictionary<string, IList<string>>();
    //            foreach (KeyValuePair<string, IList<string>> keyValuePair in ex)
    //                this.errors.Add(keyValuePair);
    //        }

    //        public IEnumerable<KeyValuePair<string, IList<string>>> Errors
    //        {
    //            get
    //            {
    //                return (IEnumerable<KeyValuePair<string, IList<string>>>)this.errors;
    //            }
    //        }

    //        public DbServiceResult AddError(string error)
    //        {
    //            return this.AddError(string.Empty, error);
    //        }

    //        public DbServiceResult AddError(string key, string error)
    //        {
    //            if (!this.errors.ContainsKey(key))
    //                this.errors[key] = (IList<string>)new List<string>();
    //            this.errors[key].Add(error);
    //            return this;
    //        }

    //        public DbServiceResult AddErrorFormat(string error, params object[] args)
    //        {
    //            return this.AddErrorFormat(string.Empty, error, args);
    //        }

    //        public DbServiceResult AddErrorFormat(string key, string error, params object[] args)
    //        {
    //            if (args == null || args.Length == 0)
    //                return this.AddError(key, error);
    //            return this.AddError(key, string.Format(error, args));
    //        }

    //        public void AddErrors(DbServiceResult other, string keyPrefix = null)
    //        {
    //            if (other == null)
    //                return;
    //            foreach (KeyValuePair<string, IList<string>> keyValuePair in other)
    //            {
    //                foreach (string error in (IEnumerable<string>)keyValuePair.Value)
    //                    this.AddError(string.IsNullOrWhiteSpace(keyPrefix) || string.IsNullOrWhiteSpace(keyValuePair.Key) ? keyValuePair.Key : keyPrefix + keyValuePair.Key, error);
    //            }
    //        }

    //        public IEnumerator<KeyValuePair<string, IList<string>>> GetEnumerator()
    //        {
    //            return this.errors.GetEnumerator();
    //        }

    //        public override string ToString()
    //        {
    //            StringBuilder stringBuilder = new StringBuilder();
    //            foreach (KeyValuePair<string, IList<string>> error in (IEnumerable<KeyValuePair<string, IList<string>>>)this.errors)
    //            {
    //                foreach (string str in (IEnumerable<string>)error.Value)
    //                    stringBuilder.AppendLine(str);
    //            }
    //            return stringBuilder.ToString();
    //        }
    //    }
    //}
}
