using Logo.Proje.Domain.MongoDbEntities;
using System.Collections.Generic;

namespace Logo.Proje.Business.Abstracts
{
    public interface IPaymentService
    {
        //Payment GetPaymentById(Expression<Func<Payment, bool>> filter);
        List<Payment> GetAllPayments();
        void AddPayment (Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(Payment payment);
    }
}
