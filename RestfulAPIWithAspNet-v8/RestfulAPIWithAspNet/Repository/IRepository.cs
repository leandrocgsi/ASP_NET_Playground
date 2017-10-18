﻿using RestfulAPIWithAspNet.Models.Entities.Base;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T item);
        T Update(T item);
        void Remove(object Id);
        void Remove(T item);
        IEnumerable<T> GetAll();
        T Find(object Id);
        bool Exists(object Id);
    }
}
