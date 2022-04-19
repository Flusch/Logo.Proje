using Logo.Proje.Business.Abstracts;
using Logo.Proje.Domain.MongoDbEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Concretes
{
    public class CardService : ICardService
    {
        private readonly IMongoCollection<Card> _cards;
        public CardService(IMongoClient mongoClient)
        {
            _cards = mongoClient.GetDatabase("LogoDb").GetCollection<Card>("cards");
        }
        public Card GetCardById(Expression<Func<Card, bool>> filter) => _cards.Find(filter).FirstOrDefault();
        public List<Card> GetAllCards() => _cards.Find(x => true).ToList();
        public void AddCard(Card card)
        {
            card.CreatedAt = DateTime.Now;
            card.CreatedBy = "SYSTEM"; //fix: get current user
            //card.OwnerId = "";
            _cards.InsertOne(card);
        }
        public void UpdateCard(Card card)
        {
            card.LastUpdatedAt = DateTime.Now;
            card.LastUpdatedBy = "SYSTEM"; //fix: get current user
            _cards.ReplaceOne(x => x.Id == card.Id, card);
        }
        //In normal circumstances data is valuable.
        //But card information is sensitive, we can not store it in database.
        public void DeleteCard(Card card) => _cards.DeleteOne(card => card.Id == card.Id);
    }
}
