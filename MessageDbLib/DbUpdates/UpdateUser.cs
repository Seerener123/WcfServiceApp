using MessageDbLib.BaseDbInterfaces;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.Exceptions.Updates;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbUpdates
{
    public class UpdateUser : IDbUpdate<UserTable>
    {
        private readonly string _dbContextType;
        private IList<UserTable> _usersToUpdate;

        public UpdateUser(IList<UserTable> users, string dbContextType)
        {
            _dbContextType = dbContextType;
            _usersToUpdate = users != null ? new List<UserTable>(users) : new List<UserTable>();
        }

        private void CheckingEntityValidity(UserTable entity)
        {
            if (entity == null || entity.Id == 0)
            {
                string reason = entity == null ? "is null" : "is not persisted object.";
                string message = string.Format("Entity value {0}, thus cannot be added to the user update pending collection.", reason);
                throw new InvalidEntityUpdateException<UserTable>(entity, message, null);
            }
        }

        public void AddToPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            _usersToUpdate.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_usersToUpdate == null || _usersToUpdate.Count <= 0)
            {
                var collectionNull = _usersToUpdate == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal user update pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_usersToUpdate.Any(m => m.Equals(entity)))
            {
                _usersToUpdate.Remove(entity);
            }
        }

        private void MarkCollectionAsUpdate(UserAbstractDbContext dbContext, IList<UserTable> usersToUpdate)
        {
            foreach (UserTable user in usersToUpdate)
            {
                dbContext.Entry(user).State = EntityState.Modified;
            }
        }

        public void UpdateChange()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    MarkCollectionAsUpdate(_dbContext, _usersToUpdate);
                    _dbContext.SaveChanges();
                    _usersToUpdate.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
