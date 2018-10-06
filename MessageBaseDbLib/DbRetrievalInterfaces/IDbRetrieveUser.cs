using MessageBaseDbLib.BasePocoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBaseDbLib.DbRetrievalInterfaces
{
    public interface IDbRetrieveUser<TEntity> : IDbRetrieve<TEntity> where TEntity : IUser
    {
        TEntity GetEntityMatchingUsernameAndPassword(string username, string password);
        bool? EntityExistMatchingUsernameAndPassword(string username, string password);
    }
}
