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
        public void AddToPending(MessageTable entity)
        {
            throw new NotImplementedException();
        }

        public void ExecuteDeletion()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromPending(MessageTable entity)
        {
            throw new NotImplementedException();
        }
    }
}