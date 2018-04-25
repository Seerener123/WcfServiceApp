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
            _messages = messages ?? new List<MessageTable>(); 
        }

        public void AddItem(MessageTable entity)
        {
            if (entity != null)
            {
                _messages.Add(entity);
            }
        }

        public void SaveChange()
        {
            if (_messages == null || _messages.Count <= 0)
            {
                return;
            }

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
                //
            }
        }
    }
}
