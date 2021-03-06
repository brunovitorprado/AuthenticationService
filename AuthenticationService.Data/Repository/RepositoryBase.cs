﻿using AuthenticationService.Data.Context;
using AuthenticationService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthenticationService.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AuthenticationContext dbContext = null;

        public RepositoryBase(AuthenticationContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public T Add(T obj)
        {
            dbContext.Set<T>().Add(obj);
            SaveChanges();

            return obj;
        }

        public T GetById(object id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public void Delete(T obj)
        {            
            dbContext.Set<T>().Remove(obj);
        }
        public void Update(T obj)
        {
            dbContext.Set<T>().Update(obj);
            SaveChanges();
        }

        public void SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
