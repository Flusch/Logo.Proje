using Logo.Proje.Business.Abstracts;
using Logo.Proje.DataAccess.EntityFramework.Repository.Abstracts;
using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Concretes
{
    public class BillService : IBillService
    {
        private readonly IRepository<Bill> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IRepository<Bill> repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }
        public Bill GetBillById(Expression<Func<Bill, bool>> filter)
        {
            return _repository.GetById(filter);
        }
        public List<Bill> GetAllBills()
        {
            return _repository.Get().ToList();
        }
        public void AddBill(Bill bill)
        {
            _repository.Add(bill);
            _unitOfWork.Commit();
        }
        public void UpdateBill(Bill bill) //fix: try to move this in repository
        {
            var exist = _repository.GetById(x => x.Id == bill.Id);
            if (exist != null)
            {
                exist.Type = bill.Type;
                exist.ApartmentId = bill.ApartmentId;
                exist.Amount = bill.Amount;
                exist.BillDate = bill.BillDate;
                exist.DueDate = bill.DueDate;
                exist.IsPaid = bill.IsPaid;
                exist.PaymentDate = bill.PaymentDate;
                //exist.Apartment = bill.Apartment;
                exist.LastUpdatedBy = "SYSTEM"; //fix: get current user
                exist.LastUpdatedAt = DateTime.Now;
                _repository.Update(exist);
                _unitOfWork.Commit();
            }
        }
        public void DeleteBill(Bill bill)
        {
            _repository.Delete(bill);
            _unitOfWork.Commit();
        }
        public List<Bill> GetMyBills(string id)
        {
            var apartmentId = _unitOfWork.Context.Set<Apartment>().Where(x => x.ResidentId == id).Select(x => x.Id).FirstOrDefault();
            return _unitOfWork.Context.Set<Bill>().Where(x => x.ApartmentId == apartmentId).ToList();
        }
    }
}
