using Logo.Proje.Domain.MongoDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logo.Proje.Business.Abstracts
{
    public interface ICardService
    {
        //Card GetCardById(Expression<Func<Card, bool>> filter);
        List<Card> GetAllCards();
        void AddCard (Card card);
        void UpdateCard (Card card);
        void DeletePCard (Card card);
    }
}
