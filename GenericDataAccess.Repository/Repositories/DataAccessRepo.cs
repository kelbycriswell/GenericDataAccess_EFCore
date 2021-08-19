using GenericDataAccess.Context.Base;
using GenericDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace GenericDataAccess.Repositories.DataAccess
{
    //Made this a this way just to test, Version 0.2 will be converted into a generic unit of work and utilize singletions
    //Will add xml documentation tags when complete
    public abstract class DataAccessRepo<TEntity, TDatabase> : IDataAccess<TEntity>
    where TEntity : class
    where TDatabase : DbContext, new()
    {
        protected bool disposedValue;
        protected DbContext _ctx;
        protected DbSet<TEntity> _dbSet;
        private IQueryable<TEntity> _baseQuery;

        public DataAccessRepo(bool lazyLoad = false, bool readOnly = false, params string[] include)
        {
            _ctx = new TDatabase();
            _ctx.Database.EnsureCreated();
            _ctx.ChangeTracker.LazyLoadingEnabled = lazyLoad;
            _ctx.ChangeTracker.QueryTrackingBehavior = readOnly ? QueryTrackingBehavior.NoTrackingWithIdentityResolution : QueryTrackingBehavior.TrackAll;
            _dbSet = _ctx.Set<TEntity>();
            _baseQuery = _dbSet;

            include.Where(w => !(string.IsNullOrEmpty(w) || string.IsNullOrWhiteSpace(w))).ToList().ForEach(fe =>
            {
                if (!string.IsNullOrEmpty(fe))
                {
                    _baseQuery = _baseQuery.Include(fe);
                }
            });
            //_baseQuery.Load();
            _ctx.SavingChanges += OnSavingHandler;
        }

        #region Create
        private void Insert(ref TEntity objToInsert)
        {
            _dbSet.Add(objToInsert);
            _ctx.Entry(objToInsert).State = EntityState.Added;
        }

        #endregion

        #region Read
        public virtual TEntity Find(params object[] pkey)
        {
            return _dbSet.Find(pkey);
        }

        public virtual TEntity FindWhere(Expression<Func<TEntity, bool>> predicate) => _baseQuery.FirstOrDefault(predicate);

        public virtual List<TEntity> GetAll() => _baseQuery.ToList();

        public virtual List<TEntity> GetOrderedWhere(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool ascending = true)
        {
            List<TEntity> list = new List<TEntity>();

            IQueryable<TEntity> query = _baseQuery;

            query = query.Where(predicate).Where(predicate);

            if (ascending)
            {
                return query.OrderBy(orderBy).ToList();
            }
            else
            {
                return query.OrderByDescending(orderBy).ToList();
            }
        }

        public virtual List<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _baseQuery.Where(predicate).Where(predicate).ToList();
        }

        #endregion

        #region Update
        private void Update(ref TEntity objToUpdate)
        {
            try
            {
                _ctx.Entry(objToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled exception in Update.", ex);
            }
        }

        public virtual void AddOrUpdate(ref TEntity objToUpdate)
        {
            if (_baseQuery.Contains(objToUpdate))
            {
                Update(ref objToUpdate);
            }
            else
            {
                Insert(ref objToUpdate);
            }

        }

        public virtual void AddOrUpdate(ref List<TEntity> objsToUpdate)
        {
            objsToUpdate.ToList().ForEach(fe =>
            {
                AddOrUpdate(ref fe);
            });
        }

        #endregion

        #region Delete
        public virtual void Delete(ref TEntity objToDelete)
        {
            throw new NotImplementedException();
        }

        #endregion

        public virtual int Save()
        {
            return Save(false);
        }

        public virtual int Save(bool forceSave)
        {
            List<string> failedObjects = new List<string>();
            try
            {
                _ctx.ChangeTracker.AutoDetectChangesEnabled = !forceSave;
                return _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateException($"Unable to save to the database, See innerException for details.", ex);
            }
        }

        private object[] GetPrimaryKeyValues(TEntity obj) => _ctx.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => obj.GetType().GetProperty(s.Name).GetValue(obj, null)).ToArray();

        protected virtual void Dispose(bool disposing, bool saveFirst)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!saveFirst || Save() != 0)
                    {
                        _ctx.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataAccess()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true, saveFirst: true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool SaveFirst = true)
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true, saveFirst: SaveFirst);
            GC.SuppressFinalize(this);
        }

        public void OnSavingHandler(object sender, SavingChangesEventArgs e)
        {
            foreach (var entity in _ctx.ChangeTracker.Entries<TEntity>().Where(w => !w.State.HasFlag(EntityState.Unchanged | EntityState.Detached)))
            {
                if (entity is IDbSetBase)
                {
                    entity.Property("ModifiedOn").CurrentValue = DateTimeOffset.Now;
                    if (entity.State == EntityState.Deleted)
                    {
                        entity.Property("Deleted").CurrentValue = true;
                        entity.State = EntityState.Modified;
                    }
                }
            }
        }

    }
}
