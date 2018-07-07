using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;

namespace MessageDbLib.DbPersistances
{
    public class UserPersistant : IDbPersistant<UserTable>
    {
        private readonly string _dbContextType;
        private IList<UserTable> _newUsers;

        public UserPersistant(IList<UserTable> newUsers, string dbContextType)
        {
            _dbContextType = dbContextType;
            _newUsers = newUsers != null ? new List<UserTable>(newUsers) : new List<UserTable>();
        }

        private void CheckingEntityValidity(UserTable entity)
        {
            if (entity == null)
            {
                var message = "Entity value is null, thus operation is invalid";
                throw new InvalidOperationException(message);
            }
        }

        public void AddToPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            _newUsers.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_newUsers == null || _newUsers.Count <= 0)
            {
                var collectionNull = _newUsers == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_newUsers.Any(m => m.Equals(entity)))
            {
                _newUsers.Remove(entity);
            }
        }

        public void SaveChange()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    _dbContext.UserTables.AddRange(_newUsers);
                    _dbContext.SaveChanges();
                    _newUsers.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}