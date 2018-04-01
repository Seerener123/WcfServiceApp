using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;

namespace MessageDbLib.DbPersistances
{
    public class PersistUser : IDbPersistant<UserTable>
    {
        private IList<UserTable> _newUsers;

        public PersistUser(IList<UserTable> newUsers)
        {
            _newUsers = newUsers ?? new List<UserTable>();
        }

        public void AddItem(UserTable entity)
        {
            if (entity != null)
            {
                _newUsers.Add(entity);
            }
        }

        public void SaveChange()
        {
            if (_newUsers == null || _newUsers.Count <= 0)
            {
                return;
            }

            try
            {
                using (var _dbContext = new MessageDbContext())
                {
                    _dbContext.UserTables.AddRange(_newUsers);
                    _dbContext.SaveChanges();
                    _newUsers.Clear();
                }
            }
            catch (Exception exception)
            {
                //
            }
        }
    }
}