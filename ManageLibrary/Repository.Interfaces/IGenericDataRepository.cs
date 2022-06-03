using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGenericDataRepository<T> where T : class
    {
        string UserId { get; set; }
        int CurrentUserProfileId { get; set; }
        int CommandTimeout { get; set; }
        IGenericDataRepository<T> CreateRepository();
        IGenericDataRepository<T> CreateRepository(int commandTimeout);
        void DisposeContext();
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, bool isAsc = false, int pageSize = 50, int page = 1,
            params Expression<Func<T, object>>[] navigationProperties);
        IList<M> GetListSelect<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties);
        IList<M> GetListSelectIncludeSub<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<M, object>>[] navigationProperties) where M : class;
        IList<T> GetList_Tracked(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IList<M> GetListSelectMany<M>(Expression<Func<T, bool>> where, Expression<Func<T, IEnumerable<M>>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, bool isAsc,
            params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle_Tracked(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingle_TrackedAsync(Expression<Func<T, bool>> where,
             params Expression<Func<T, object>>[] navigationProperties);
        void LoadNavigation_Tracked<M>(M obj, string propertyName) where M : class;
        void LoadNavigation_Tracked<M, N>(M obj, Expression<Func<M, N>> navigationProperty, params Expression<Func<N, object>>[] childrenProperty) where M : class where N : class;
        void Add(params T[] items);
        void AddWithOthers(params T[] items);
        Task<bool> AddAsync(params T[] items);
        Task<bool> BulkAddAsync(params T[] items);
        Task<bool> AddReloadNavAsync(List<Expression<Func<T, object>>> navs, params T[] items);
        void Update(params T[] items);
        void UpdateWithOthers(params T[] items);
        void Update<M>(params M[] items);
        void UpdateChildren_Tracked(T obj, string propertyName, params int[] listIds);
        void Save_Tracked();
        void Remove(params T[] items);
        void RemoveOthers<M>(params M[] items) where M : class;
        void Remove_Tracked<M>(params M[] items) where M : class;
        M GetList_IQueryable<M>(Func<IQueryable<T>, M> processQueryable, params Expression<Func<T, object>>[] navigationProperties);
        M GetList_IQueryable<M>(Func<IQueryable<T>, M> processQueryable, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        int Count(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        bool Exist(Expression<Func<T, bool>> where);

        #region Async method
        Task<bool> Save_TrackedAsync();
        Task<bool> UpdateAsync(params T[] items);
        Task<bool> BulkUpdateAsync(params T[] items);
        Task<bool> UpdateAsync(string navigationName, params T[] items);
        Task<bool> RemoveAsync(params T[] items);
        Task<int> CountAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<T>> GetListAsync(Expression<Func<T, bool>> where,
           params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<M>> GetListSelectAsync<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties);
        #endregion
    }
}
