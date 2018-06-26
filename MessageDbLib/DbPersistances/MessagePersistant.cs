using MessageDbLib.MessagingEntities;
using MessageDbLib.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbPersistances
{
    public class MessagePersistant : IDbPersistant<MessageTable>
    {
        private IList<MessageTable> _messages;

        public MessagePersistant(IList<MessageTable> messages)
        {
            _messages = messages != null ? new List<MessageTable>(messages) :
                new List<MessageTable>();
        }

        private void CheckingEntityValidity(MessageTable entity)
        {
            if (entity == null)
            {
                var message = "Entity value is null, thus operation is invalid";
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
                var message = string.Format("Internal pending collection is {0}", state);
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
                using (var _dbContext = new MessageDbContext())
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
