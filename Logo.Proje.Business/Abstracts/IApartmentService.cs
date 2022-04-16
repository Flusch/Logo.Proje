using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Abstracts
{
    public interface IApartmentService
    {
        Apartment GetApartmentById(Expression<Func<Apartment, bool>> filter);
        List<Apartment> GetAllApartments();
        void AddApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment);
        void DeleteApartment (Apartment apartment);
    }
}
