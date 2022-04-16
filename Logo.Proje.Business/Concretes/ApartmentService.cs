using Logo.Proje.Business.Abstracts;
using Logo.Proje.DataAccess.EntityFramework.Repository.Abstracts;
using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            return _repository.GetById(filter);
        }
        public List<Apartment> GetAllApartments()
        {
            return _repository.Get().ToList();
        }
        public void AddApartment(Apartment apartment)
        {
            _repository.Add(apartment);
            _unitOfWork.Commit();
        }
        public void UpdateApartment(Apartment apartment) //fix
        {
            var exist = _repository.GetById(x => x.Id == apartment.Id);
            if (exist != null)
            {
                exist.Block = apartment.Block;
                exist.Floor = apartment.Floor;
                exist.Number = apartment.Number;
                exist.RoomCount = apartment.RoomCount;
                exist.IsSomeoneLiving = apartment.IsSomeoneLiving;
                exist.ResidentId = apartment.ResidentId;
                exist.LastUpdatedBy = "Yavuz Selim"; //fix
                exist.LastUpdatedAt = DateTime.Now;
                _repository.Update(exist);
                _unitOfWork.Commit();
            }
        }
        public void DeleteApartment (Apartment apartment)
        {
            _repository.Delete(apartment);
            _unitOfWork.Commit();
        }
    }
}
