using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WLib.Core.Bll.DataAccess.Model;

namespace WLib.Core.Bll.DataAccess
{
    public interface IDataAccess : IBaseDataAccess, IDisposable
    {
        Task<int> SaveWithoutTrackingAsync();

        IQueryable<T> GetAllIncludingDeleted<T>(params Expression<Func<T, object>>[] includes) where T : class;

        void Detach<T>(T obj) where T : class;

        Task<int> ValidateAndSaveAsync(bool noTracking = false);

        Task InsertOrUpdateAsync<T>(T entity) where T : BaseEntity, new();
        /// <summary>
        /// Generic method to get items by implementing typ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> SetOf<T>() where T : class;
    }
}
