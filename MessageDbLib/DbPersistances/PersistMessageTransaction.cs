using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbPersistances
{
    public class PersistMessageTransaction : IDbPersistant<MessageTransactionTable>
    {
        private IList<MessageTransactionTable> _messageTransactions;

        public PersistMessageTransaction(IList<MessageTransactionTable> messageTransactions)
        {
            _messageTransactions = messageTransactions
                ?? new List<MessageTransactionTable>(); 
        }

        public void AddItem(MessageTransactionTable entity)
        {
            _messageTransactions.Add(entity);
        }

        public void SaveChange()
        {
            if (_messageTransactions == null && _messageTransactions.Count <= 0)
            {
                return;
            }

            try
            {
                using (var _dbContext = new MessageDbContext())
                {
                    _dbContext.MessageTransactionTables
                        .AddRange(_messageTransactions);
                    _dbContext.SaveChanges();
                    _messageTransactions.Clear();
                }
            }
            catch (Exception exception)
            {
                // add exception code here
            }
        }
    }
}