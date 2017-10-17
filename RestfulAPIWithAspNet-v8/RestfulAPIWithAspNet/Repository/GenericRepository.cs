using RestfulAPIWithAspNet.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestfulAPIWithAspNet.Models.Entities;
using System;

namespace RestfulAPIWithAspNet.Repository
{

    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
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
            _context.SaveChanges();
            return item;
        }

        public T Update(T item)
        {
            var result = dataSet.SingleOrDefault(i => i.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }

        public void Remove(object id)
        {
            T result = dataSet.SingleOrDefault(i => i.Id.Equals(id));
            dataSet.Remove(result);
            _context.SaveChanges();
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
            return dataSet.SingleOrDefault(m => m.Id.Equals(id));
        }

        public bool Exists(object Id)
        {
            return dataSet.Any(b => b.Id.Equals(Id));
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