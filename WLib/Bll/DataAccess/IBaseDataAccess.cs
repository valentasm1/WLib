using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WLib.Core.Bll.DataAccess.Model;

namespace WLib.Core.Bll.DataAccess
{
    public interface IBaseDataAccess : IDisposable
    {
        IQueryable<T> GetAllReadOnly<T>(params Expression<Func<T, object>>[] includes) where T : class;

        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includes) where T : class;


        Task<T> InsertOrUpdateAsync<T, O>(T entity, Expression<Func<T, O>> onlyFields = null) where T : BaseEntity, new() where O : class;

        void Delete<T>(T entity) where T : class;

        void Delete<T>(int id) where T : BaseEntity, new();

        Task<int> SaveAsync();

        void Rollback();

        int SqlExecute(string sql, params object[] parameters);

        Task<int> SqlExecuteAsync(string sql, params object[] parameters);
    }
}
