﻿using RestfulAPIWithAspNet.Models;
using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Repository
{
    public interface IContactsRepository
    {
        void Add(Contacts item);
        IEnumerable<Contacts> GetAll();
        Contacts Find(string key);
        void Remove(string Id);
        void Update(Contacts item);
    }
}
