using RestfulAPIWithAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPIWithAspNet.Repository
{

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly MySQLContext _context;
        private DbSet<T> dataSet;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataSet = _context.Set<T>();
        }

        public T Add(T item)
        {
            dataSet.Add(item);
            Save();
            return item;
        }

        public T Update(T item)
        {
            dataSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }

        public void Remove(object id)
        {
            T entityToDelete = dataSet.Find(id);
            Remove(entityToDelete);
        }

        public void Remove(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dataSet.Attach(entityToDelete);

            }
            dataSet.Remove(entityToDelete);
        }

        public IEnumerable<T> GetAll()
        {
            return dataSet.ToList();
        }

        public T Find(object id)
        {
            return dataSet.Find(id);
        }

        public bool Exists(object Id)
        {
             dataSet.SingleOrDefault(b => b.Equals(Id));
            return true;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
