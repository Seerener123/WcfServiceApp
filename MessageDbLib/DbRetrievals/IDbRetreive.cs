using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbRetrievals
{
    public interface IDbRetreive<TEntity> where TEntity : IBaseEntity
    {
        IList<TEntity> GetAllEntities();
        IList<TEntity> GetAllEntitiesFunc(Func<TEntity, bool> funcOperation);

        TEntity GetEntityMatchingId(long id);
        TEntity GetEntityMatchingUsernameAndPassword(string username, string password);
        TEntity GetEntityMatchingFunc(Func<TEntity, bool> funcOperation);

        bool EntityExistMatchingId(long id);
        bool EntityExistMatchingUsernameAndPassword(string username, string password);
        bool EntityExistMatchingFunc(Func<TEntity, bool> funcOperation);
    }
}