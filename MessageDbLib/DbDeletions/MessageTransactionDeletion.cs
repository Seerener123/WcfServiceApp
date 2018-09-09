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
    public class MessageTransactionDeletion : IDbDeletion<MessageTransactionTable>
    {
        private readonly string _dbContextType;
        private IList<MessageTransactionTable> _messageTransactionsToDelete;

        public MessageTransactionDeletion(IList<MessageTransactionTable> messageTransactions, string dbContextType)
        {
            _dbContextType = dbContextType;
            _messageTransactionsToDelete = messageTransactions != null ? new List<MessageTransactionTable>(messageTransactions) :
                new List<MessageTransactionTable>();
        }

        private void CheckingEntityValidity(MessageTransactionTable entity)
        {
            if (entity == null || entity.Id == 0)
            {
                string reason = entity == null ? "is null" : "is not a persisted object";
                string message = string.Format("Entity value {0}, thus cannot be added to the message transaction deletion pending collection.", reason);
                throw new InvalidEntityDeletionException<MessageTransactionTable>(entity, message, null);
            }
        }

        public void AddToPending(MessageTransactionTable entity)
        {
            CheckingEntityValidity(entity);
            _messageTransactionsToDelete.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_messageTransactionsToDelete == null || _messageTransactionsToDelete.Count <= 0)
            {
                var collectionNull = _messageTransactionsToDelete == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal message transaction deletion pending collection is {0}.", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(MessageTransactionTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_messageTransactionsToDelete.Any(m => m.Equals(entity)))
            {
                _messageTransactionsToDelete.Remove(entity);
            }
        }

        public void ExecuteDeletion()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
                {
                    _dbContext.MessageTransactionTables.RemoveRange(_messageTransactionsToDelete);
                    _dbContext.SaveChanges();
                    _messageTransactionsToDelete.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}