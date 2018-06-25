using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbDeletions
{
    public interface IDbDeletion
    {
        void AddToPending();
        void RemoveFromPending();
        void ExecuteDeletion();
    }
}
