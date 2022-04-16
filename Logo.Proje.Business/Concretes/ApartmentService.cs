using Logo.Proje.Business.Abstracts;
using Logo.Proje.DataAccess.EntityFramework.Repository.Abstracts;
using Logo.Proje.DataAccess.EntityFramework.Repository.Concretes;
using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Proje.Business.Concretes
{
    public class ApartmentService : IApartmentService
    {
        private readonly IRepository<Apartment> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentService(IRepository<Apartment> repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }
        public Apartment GetApartmentById(Expression<Func<Apartment, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public List<Apartment> GetAllApartments()
        {
            return _repository.Get().ToList();
        }
        public void AddApartment(Apartment apartment)
        {
            throw new NotImplementedException();
        }
        public void UpdateApartmentById(Apartment apartment)
        {
            throw new NotImplementedException();
        }
        public void DeleteApartmentById(Apartment apartment)
        {
            throw new NotImplementedException();
        }
    }
}
