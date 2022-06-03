using DataAccessLayer.DbContext;
using Domain.Model.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {

        public GenericDataRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericDataRepository(ApplicationDbContext context, int profileId)
        {
            this.context = context;
            this.currentUserProfileId = profileId;
        }

        protected int commandTimeout = 30;
        protected ApplicationDbContext context;// = new ApplicationDbContext();
        private string userId;
        private int currentUserProfileId = -1;

        public string UserId { get => userId; set => userId= value; }
        public int CurrentUserProfileId { get => currentUserProfileId; set => currentUserProfileId = value; }
        public int CommandTimeout { get => commandTimeout; set => commandTimeout = value; }



        public virtual void Add(params T[] items)
        {
            using (var db = context.Create(commandTimeout))
            {
                foreach (T item in items)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                //Set currentProfileId
                db.CurrentUserProfileId = currentUserProfileId;
                db.SaveChanges();
            }
        }
        public virtual void AddWithOthers(params T[] items)
        {
            using (var db = context.Create(commandTimeout))
            {
                db.AddRange(items);
                //Set currentProfileId
                db.CurrentUserProfileId = currentUserProfileId;
                db.SaveChanges();
            }
        }

        public Task<bool> AddAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddReloadNavAsync(List<Expression<Func<T, object>>> navs, params T[] items)
        {
            throw new NotImplementedException();
        }


        public Task<bool> BulkAddAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BulkUpdateAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IGenericDataRepository<T> CreateRepository()
        {
            var repo = new GenericDataRepository<T>(context.Create(CommandTimeout), currentUserProfileId);
            return repo;
        }

        public IGenericDataRepository<T> CreateRepository(int commandTimeout)
        {
            throw new NotImplementedException();
        }

        public void DisposeContext()
        {
            throw new NotImplementedException();
        }

        public bool Exist(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var db = context.Create(commandTimeout))
            {
                IQueryable<T> dbQuery = db.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, bool isAsc = false, int pageSize = 50, int page = 1, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetListAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<M> GetListSelect<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IList<M>> GetListSelectAsync<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<M> GetListSelectIncludeSub<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, bool isDistinct = false, params Expression<Func<M, object>>[] navigationProperties) where M : class
        {
            throw new NotImplementedException();
        }

        public IList<M> GetListSelectMany<M>(Expression<Func<T, bool>> where, Expression<Func<T, IEnumerable<M>>> selector, bool isDistinct = false, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public M GetList_IQueryable<M>(Func<IQueryable<T>, M> processQueryable, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var db = context.Create(commandTimeout))
            {
                IQueryable<T> dbQuery = db.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty.AsPath());
                return processQueryable(dbQuery);
            }
        }

        public M GetList_IQueryable<M>(Func<IQueryable<T>, M> processQueryable, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var db = context.Create(commandTimeout))
            {
                IQueryable<T> dbQuery = db.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include(navigationProperty.AsPath());
                return processQueryable(dbQuery.Where(where).AsQueryable<T>());
            }
        }

        public IList<T> GetList_Tracked(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, bool isAsc, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public T GetSingle_Tracked(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingle_TrackedAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void LoadNavigation_Tracked<M>(M obj, string propertyName) where M : class
        {
            throw new NotImplementedException();
        }

        public void LoadNavigation_Tracked<M, N>(M obj, Expression<Func<M, N>> navigationProperty, params Expression<Func<N, object>>[] childrenProperty)
            where M : class
            where N : class
        {
            throw new NotImplementedException();
        }

        public void Remove(params T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        public void RemoveOthers<M>(params M[] items) where M : class
        {
            throw new NotImplementedException();
        }

        public void Remove_Tracked<M>(params M[] items) where M : class
        {
            throw new NotImplementedException();
        }

        public void Save_Tracked()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save_TrackedAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(params T[] items)
        {
            throw new NotImplementedException();
        }

        public void Update<M>(params M[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string navigationName, params T[] items)
        {
            throw new NotImplementedException();
        }

        public void UpdateChildren_Tracked(T obj, string propertyName, params int[] listIds)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithOthers(params T[] items)
        {
            throw new NotImplementedException();
        }
    }
}
