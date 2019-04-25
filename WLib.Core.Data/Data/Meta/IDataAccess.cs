using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WLib.Core.Data.Domain.Entities;

namespace WLib.Core.Data.Data.Meta
{
    public interface IDataAccess : IBaseDataAccess, IDisposable
    {
        Task<int> SaveWithoutTrackingAsync();

        IQueryable<T> GetAllIncludingDeleted<T>(params Expression<Func<T, object>>[] includes) where T : class;

        void Detach<T>(T obj) where T : class;

        Task<int> ValidateAndSaveAsync(bool noTracking = false);

        Task InsertOrUpdateAsync<T>(T entity) where T : BaseEntity, new();
    }
}
