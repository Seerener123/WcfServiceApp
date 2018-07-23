using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbDeletions
{
    public class UserDeletion : IDbDeletion<UserTable>
    {
        public void AddToPending(UserTable entity)
        {
            throw new NotImplementedException();
        }

        public void ExecuteDeletion()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromPending(UserTable entity)
        {
            throw new NotImplementedException();
        }
    }
}