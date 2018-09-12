using MessageBaseDbLib.BasePocoInterfaces;
using System;
using System.Collections.Generic;
/*using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace MessageBaseDbLib.DbRetrievalInterfaces
{
    public interface IDbRetreive<TEntity> where TEntity : IBaseEntity
    {
        IList<TEntity> GetAllEntities();
        TEntity GetEntityMatchingId(long id);
        TEntity GetEntityMatchingFunc(Func<TEntity, bool> funcOperation);

        bool EntityExistMatchingId(long id);
        bool EntityExistMatchingFunc(Func<TEntity, bool> funcOperation);
    }
}