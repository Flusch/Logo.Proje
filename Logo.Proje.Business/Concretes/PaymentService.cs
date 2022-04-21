using Logo.Proje.Business.Abstracts;
using Logo.Proje.Domain.MongoDbEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Concretes
{
    public class PaymentService : IPaymentService
    {
        private readonly IMongoCollection<Payment> _payments;
        public PaymentService(IMongoClient mongoClient)
        {
            _payments = mongoClient.GetDatabase("LogoDb").GetCollection<Payment>("payments");
        }
        public Payment GetPaymentById(Expression<Func<Payment, bool>> filter) => _payments.Find(filter).FirstOrDefault();
        public List<Payment> GetAllPayments() => _payments.Find(x => true).ToList();
        public void AddPayment(Payment payment)
        {
            payment.CreatedAt = DateTime.Now;
            _payments.InsertOne(payment);
        }
        public void UpdatePayment(Payment payment)
        {
            payment.LastUpdatedAt = DateTime.Now;
            payment.LastUpdatedBy = "SYSTEM";
            _payments.ReplaceOne(x => x.Id == payment.Id, payment);
        }
        public void DeletePayment(Payment payment)
        {
            payment.LastUpdatedAt = DateTime.Now;
            payment.LastUpdatedBy = "SYSTEM";
            payment.IsDeleted = true;
            _payments.ReplaceOne(x => x.Id == payment.Id, payment);
        }        
    }
}
