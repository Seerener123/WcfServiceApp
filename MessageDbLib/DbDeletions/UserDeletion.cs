using MessageDbLib.DbContextFactorys;
using MessageDbLib.MessagingEntities;
using MessageDbLib.Exceptions.Deletions;
using System;
using System.Collections.Generic;
using System.Linq;
using MessageBaseDbLib.DbDeletionInterfaces;

namespace MessageDbLib.DbDeletions
{
    public class UserDeletion : IDbDeletion<UserTable>
    {
        private readonly string _dbContextType;
        private IList<UserTable> _userToDelete;

        public UserDeletion(IList<UserTable> usersToDelete, string dbContextType)
        {
            _dbContextType = dbContextType;
            _userToDelete = usersToDelete != null ? new List<UserTable>(usersToDelete) : new List<UserTable>();
        }

        private void CheckingEntityValidity(UserTable entity)
        {
            if (entity == null || entity.Id == 0)
            {
                string reason = entity == null ? "is null" : "is not a persisted object";
                string message = string.Format("Entity value {0}, thus cannot be added to the user deletion pending collection.", reason);
                throw new InvalidEntityDeletionException<UserTable>(entity, message, null);
            }
        }

        public void AddToPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            _userToDelete.Add(entity);
        }

        private void CheckingInternalCollectionValidity()
        {
            if (_userToDelete == null || _userToDelete.Count <= 0)
            {
                var collectionNull = _userToDelete == null;
                var state = collectionNull ? "null" : "empty";
                var message = string.Format("Internal user deletion pending collection is {0}", state);
                throw new InvalidOperationException(message);
            }
        }

        public void RemoveFromPending(UserTable entity)
        {
            CheckingEntityValidity(entity);
            CheckingInternalCollectionValidity();

            if (_userToDelete.Any(m => m.Equals(entity)))
            {
                _userToDelete.Remove(entity);
            }
        }

        public void ExecuteDeletion()
        {
            CheckingInternalCollectionValidity();

            try
            {
                using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    _dbContext.UserTables.RemoveRange(_userToDelete);
                    _dbContext.SaveChanges();
                    _userToDelete.Clear();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}