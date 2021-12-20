﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepoInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetSingle(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddMany(List<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
