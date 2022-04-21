using Logo.Proje.Business.Abstracts;
using Logo.Proje.DataAccess.EntityFramework.Repository.Abstracts;
using Logo.Proje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Logo.Proje.Business.Concretes
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IRepository<Message> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public Message GetMessageById(Expression<Func<Message, bool>> filter)
        {
            return _repository.GetById(filter);
        }
        public List<Message> GetAllMessages()
        {
            return _repository.Get().ToList();
        }
        public void AddMessage(Message message)
        {
            _repository.Add(message);
            _unitOfWork.Commit();
        }
        public void UpdateMessage(Message message) //fix: try to move this in repository
        {
            var exist = _repository.GetById(x => x.Id == message.Id);
            if (exist != null)
            {
                exist.From = message.From;
                exist.To = message.To;
                exist.Text = message.Text;
                exist.IsRead = message.IsRead;
                exist.LastUpdatedBy = "SYSTEM"; //fix: get current user
                exist.LastUpdatedAt = DateTime.Now;
                _repository.Update(exist);
                _unitOfWork.Commit();
            }
        }
        public void DeleteMessage(Message message)
        {
            _repository.Delete(message);
            _unitOfWork.Commit();
        }

        public List<Message> GetMyMessages(string id)
        {            
            return _unitOfWork.Context.Set<Message>().Where(x => x.To == id).ToList();
        }
    }
}
