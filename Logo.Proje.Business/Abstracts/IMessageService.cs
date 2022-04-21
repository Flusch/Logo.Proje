using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Logo.Proje.Business.Abstracts
{
    public interface IMessageService
    {
        Message GetMessageById(Expression<Func<Message, bool>> filter);
        List<Message> GetAllMessages();
        void AddMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage (Message message);
        List<Message> GetMyMessages(string id);
    }
}
