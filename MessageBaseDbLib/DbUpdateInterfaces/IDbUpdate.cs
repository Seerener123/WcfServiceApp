using MessageBaseDbLib.BasePocoInterfaces;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace MessageBaseDbLib.DbUpdateInterfaces
{
    public interface IDbUpdate<TEntity> where TEntity : IBaseEntity
    {
        void AddToPending(TEntity entity);
        void RemoveFromPending(TEntity entity);
        void UpdateChange();
    }
}
