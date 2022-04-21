using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Abstracts
{
    public interface IBillService
    {
        Bill GetBillById(Expression<Func<Bill, bool>> filter);
        List<Bill> GetAllBills();
        void AddBill(Bill bill);
        void UpdateBill(Bill bill);
        void DeleteBill(Bill bill);
        List<Bill> GetMyBills(string id);
    }
}
