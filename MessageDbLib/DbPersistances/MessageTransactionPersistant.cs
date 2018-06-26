﻿using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbPersistances
{
    public class MessageTransactionPersistant : IDbPersistant<MessageTransactionTable>
    {
        private IList<MessageTransactionTable> _messageTransactions;

        public MessageTransactionPersistant(IList<MessageTransactionTable> messageTransactions)
        {
            _messageTransactions = messageTransactions != null ? new List<MessageTransactionTable>(messageTransactions) : 
                new List<MessageTransactionTable>(); 
        }

        private void CheckingEntityValidity(MessageTransactionTable entity)
        {
            if (entity == null)
            {
                var message = "Entity value is null, thus operation is invalid";
                throw new InvalidOperationException(message);
            }
        }

        public void AddToPending(MessageTransactionTable entity)
        {
            CheckingEntityValidity(entity);
            _messageTransactions.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_messageTransactions == null || _messageTransactions.Count <= 0)
            {
                var collectionNull = _messageTransactions == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(MessageTransactionTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_messageTransactions.Any(m => m.Equals(entity)))
            {
                _messageTransactions.Remove(entity);
            }
        }

        public void SaveChange()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = new MessageDbContext())
                {
                    _dbContext.MessageTransactionTables.AddRange(_messageTransactions);
                    _dbContext.SaveChanges();
                    _messageTransactions.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}