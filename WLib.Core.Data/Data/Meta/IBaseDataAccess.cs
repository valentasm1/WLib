using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WLib.Core.Data.Domain.Entities;

namespace WLib.Core.Data.Data.Meta
{
    public interface IBaseDataAccess : IDisposable
    {
        IQueryable<T> GetAllReadOnly<T>(params Expression<Func<T, object>>[] includes) where T : class;

        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class;

        Task InsertAsync<T>(T entity) where T : class;

        Task InsertRangeAsync<T>(IEnumerable<T> entities) where T : class;

        void Update<T>(T entity) where T : class;

        void Update<T, O>(T entity, Expression<Func<T, O>> onlyFields) where T : class where O : class;

        Task InsertOrUpdateAsync<T, O>(T entity, Expression<Func<T, O>> onlyFields = null) where T : BaseEntity, new() where O : class;

        void Delete<T>(T entity) where T : class;

        void Delete<T>(int id) where T : BaseEntity, new();

        Task<int> SaveAsync();

        void Rollback();

        Task<int> SqlExecute(string sql, params object[] parameters);

        Task<int> SqlExecuteAsync(string sql, params object[] parameters);
    }
}
