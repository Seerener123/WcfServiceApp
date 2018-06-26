using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbUpdates
{
    public class UpdateUser : IDbUpdate<IUser>
    {
        public void AddToPending(IUser entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromPending(IUser entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateChange()
        {
            throw new NotImplementedException();
        }
    }
}
