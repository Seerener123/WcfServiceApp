using MessageDbLib.MessagingEntities;
using MessageDbLib.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageDbLib.DbContextFactorys;

namespace MessageDbLib.DbPersistances
{
    public class MessagePersistant : IDbPersistant<MessageTable>
    {
        private readonly string _dbContextType;
        private IList<MessageTable> _messages;

        public MessagePersistant(IList<MessageTable> messages, string dbContextType)
        {
            _dbContextType = dbContextType;
            _messages = messages != null ? new List<MessageTable>(messages) : new List<MessageTable>();
        }

        private void CheckingEntityValidity(MessageTable entity)
        {
            if (entity == null)
            {
                var message = "Entity value is null, thus entity cannot be added to message persistant pending collection.";
                throw new InvalidOperationException(message);
            }
        }

        public void AddToPending(MessageTable entity)
        {
            CheckingEntityValidity(entity);
            _messages.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_messages == null || _messages.Count <= 0)
            {
                var collectionNull = _messages == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal message persistant pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(MessageTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_messages.Any(m => m.Equals(entity)))
            {
                _messages.Remove(entity);
            }
        }

        public void SaveChange()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
                {
                    _dbContext.MessageTables.AddRange(_messages);
                    _dbContext.SaveChanges();
                    _messages.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
