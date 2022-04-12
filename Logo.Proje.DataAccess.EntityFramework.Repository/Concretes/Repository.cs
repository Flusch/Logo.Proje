using Logo.Proje.DataAccess.EntityFramework.Repository.Abstracts;
using Logo.Proje.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Logo.Proje.DataAccess.EntityFramework.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly IUnitOfWork unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IQueryable<T> Get()
        {
            return unitOfWork.Context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();
        }
        public T GetById(Expression<Func<T, bool>> filter)
        {
            return unitOfWork.Context.Set<T>().Where(x => !x.IsDeleted).SingleOrDefault(filter);
        }
        public void Add(T entity)
        {
            unitOfWork.Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            var exist = GetById(x => x.Id == entity.Id);
            if (exist != null)
            {

                exist.LastUpdatedBy = entity.LastUpdatedBy;
                exist.LastUpdatedAt = DateTime.Now;
                unitOfWork.Context.Entry(exist).State = EntityState.Modified;
            }
        }
        public void Delete(T entity)
        {
            T exist = unitOfWork.Context.Set<T>().Find(entity.Id);
            if (exist != null)
            {
                exist.IsDeleted = true;
                exist.LastUpdatedBy = entity.LastUpdatedBy;
                exist.LastUpdatedAt = DateTime.Now;
                unitOfWork.Context.Entry(exist).State = EntityState.Modified;
            }
        }
    }
}
