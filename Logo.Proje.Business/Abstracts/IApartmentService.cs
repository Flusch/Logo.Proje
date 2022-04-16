using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Proje.Business.Abstracts
{
    public interface IApartmentService
    {
        Apartment GetApartmentById(Expression<Func<Apartment, bool>> filter);
        List<Apartment> GetAllApartments();
        void AddApartment(Apartment apartment);
        void UpdateApartmentById(Apartment apartment);
        void DeleteApartmentById(Apartment apartment);
    }
}
