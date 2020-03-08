using HomeAutomata.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HomeAutomata.Data.Repositories
{
    public class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext Db;
        private readonly bool _disposeContext;
        protected DbSet<T> Table;

        public AppDbContext Context => Db;

        public IQueryable<T> QueryTable => Table;

        public BaseRepo(DbContextOptions<AppDbContext> options) : this(new AppDbContext(options))
        {
            _disposeContext = true;
        }

        protected BaseRepo(AppDbContext appDbContext)
        {
            Db = appDbContext;
            Table = Db.Set<T>();
        }

        public int Count => Table.Count();

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        public int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int AddReturnEntityId(T entity, bool persist = true)
        {
            Table.Add(entity);
            SaveChanges();
            return entity.Id;
        }

        public bool Any()
        {
            return Table.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return Table.Any(where);
        }

        public int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        internal T GetEntryFromChangeTracker(int? id)
        {
            return Db.ChangeTracker.Entries<T>()
                .Select((EntityEntry e) => (T)e.Entity)
                    .FirstOrDefault(x => x.Id == id);
        }

        public int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public T Find(int id) => Table.Find(id);

        public T Find(Expression<Func<T, bool>> where)
            => Table.Where(where).FirstOrDefault();

        public T Find<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(@where).Include(include).FirstOrDefault();

        public T First() => Table.FirstOrDefault();

        public T First(Expression<Func<T, bool>> where) => Table.FirstOrDefault(where);

        public T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include).FirstOrDefault();

        public IEnumerable<T> GetAll() => Table;

        public IEnumerable<T> GetAll<TIncludeField>(Expression<Func<T, TIncludeField>> include) => Table.Include(include);

        public IEnumerable<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy) : Table.OrderByDescending(orderBy);

        public IEnumerable<T> GetAll<TIncludeField, TSortField>(Expression<Func<T, TIncludeField>> include, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Include(include).OrderBy(orderBy) : Table.Include(include).OrderByDescending(orderBy);

        public IEnumerable<T> GetRange(int skip, int take) => GetRange(Table, skip, take);

        public IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take) => query.Skip(skip).Take(take);

        public IEnumerable<T> GetSome(Expression<Func<T, bool>> where) => Table.Where(where);

        public IEnumerable<T> GetSome<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include);

        public IEnumerable<T> GetSome<TSortField>(Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Where(where).OrderBy(orderBy) : Table.Where(where).OrderByDescending(orderBy);

        public IEnumerable<T> GetSome<TIncludeField, TSortField>(Expression<Func<T, bool>> where,
                                                                 Expression<Func<T, TIncludeField>> include,
                                                                 Expression<Func<T, TSortField>> orderBy,
                                                                 bool ascending = true)
            => ascending ?
                Table.Where(where).OrderBy(orderBy).Include(include) :
                Table.Where(where).OrderByDescending(orderBy).Include(include);

        public int SaveChanges()
        {
            try
            {
                // add auto history logging
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        #region Disposing

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            if (_disposeContext)
            {
                Db.Dispose();
                // Console.WriteLine("---------   DB Disposed   ---------");
            }
            _disposed = true;
        }

        #endregion Disposing
    }
}