using MessageBaseDbLib.DbRetrievalInterfaces;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageDbLib.DbRetrievals
{
    public class RetrieveUserClass : IDbRetreive<UserTable>
    {
        private string _dbContextType;

        public RetrieveUserClass(string dbContextType)
        {
            _dbContextType = dbContextType;
        }

        public IList<UserTable> GetAllEntities()
        {
            try
            {
                return this.GettingAllEntities();
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private IList<UserTable> GettingAllEntities()
        {
            using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
            {
                // DatabaseOptionConfigRetriever.DatabaseOptionAppSetting
                return _dbContext.UserTables.ToList();
            }
        }

        public UserTable GetEntityMatchingFunc(Func<UserTable, bool> funcOperation)
        {
            try
            {
                return this.FindEntityMatching(funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        public UserTable GetEntityMatchingId(long id)
        {
            try
            {
                return this.FindEntityMatching(x => x.Id == id);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private UserTable FindEntityMatching(Func<UserTable, bool> funcOperation)
        {
            using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
            {
                // DatabaseOptionConfigRetriever.DatabaseOptionAppSetting
                return _dbContext.UserTables.SingleOrDefault(funcOperation);
            }
        }

        public bool EntityExistMatchingId(long id)
        {
            try
            {
                using (UserAbstractDbContext _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    return _dbContext.UserTables.Any(u => u.Id == id);
                }
            }
            catch (Exception exception)
            {
                //
            }
            return false;
        }

        public bool EntityExistMatchingFunc(Func<UserTable, bool> funcOperation)
        {
            try
            {
                using (UserAbstractDbContext _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    return _dbContext.UserTables.Any(funcOperation);
                }
            }
            catch (Exception exception)
            {
                //
            }
            return false;
        }
    }
}