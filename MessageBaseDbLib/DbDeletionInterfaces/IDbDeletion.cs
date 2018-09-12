using MessageBaseDbLib.BasePocoInterfaces;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace MessageBaseDbLib.DbDeletionInterfaces
{
    public interface IDbDeletion<TEntity> where TEntity : IBaseEntity
    {
        void AddToPending(TEntity entity);
        void RemoveFromPending(TEntity entity);
        void ExecuteDeletion();
    }
}
