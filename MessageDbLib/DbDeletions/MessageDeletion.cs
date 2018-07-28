using MessageDbLib.DbContextFactorys;
using MessageDbLib.Exceptions;
using MessageDbLib.Exceptions.Deletions;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbDeletions
{
    public class MessageDeletion : IDbDeletion<MessageTable>
    {
        private readonly string _dbContextType;
        private IList<MessageTable> _messageToDelete;

        public MessageDeletion(IList<MessageTable> messages, string dbconetxtType)
        {
            _dbContextType = dbconetxtType;
            _messageToDelete = messages != null ? new List<MessageTable>(messages) : new List<MessageTable>();
        }

        private void CheckingEntityValidity(MessageTable entity)
        {
            if (entity == null || entity.ID == 0)
            {
                string reason = entity == null ? "is null" : "is not a persisted object";
                string message = string.Format("Entity value {0}, thus cannot be added to the message deletion pending collection.", reason);
                throw new InvalidEntityDeletionException<MessageTable>(entity, message, null);
            }
        }

        public void AddToPending(MessageTable entity)
        {
            CheckingEntityValidity(entity);
            _messageToDelete.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_messageToDelete == null || _messageToDelete.Count <= 0)
            {
                var collectionNull = _messageToDelete == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal message deletion pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(MessageTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_messageToDelete.Any(m => m.Equals(entity)))
            {
                _messageToDelete.Remove(entity);
            }
        }

        public void ExecuteDeletion()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
                {
                    _dbContext.MessageTables.RemoveRange(_messageToDelete);
                    _dbContext.SaveChanges();
                    _messageToDelete.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}