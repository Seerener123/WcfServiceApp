﻿using MessageBaseDbLib.DbRetrievalInterfaces;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageDbLib.DbRetrievals
{
    public class RetrieveUserClass : IDbRetrieveUser<UserTable>
    {
        private readonly string _dbContextType;

        public RetrieveUserClass(string dbContextType)
        {
            _dbContextType = dbContextType;
        }

        public IList<UserTable> GetAllEntities()
        {
            try
            {
                return this.RetrieveAllEntities();
            }
            catch (Exception exception)
            {
                // logging
            }
            return null;
        }

        private IList<UserTable> RetrieveAllEntities()
        {
            using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
            {
                // DatabaseOptionConfigRetriever.DatabaseOptionAppSetting
                return _dbContext.UserTables.ToList();
            }
        }

        public IList<UserTable> GetAllEntitiesFunc(Func<UserTable, bool> funcOperation)
        {
            try
            {
                return this.RetrieveAllEntitiesFiltered(funcOperation);
            }
            catch (Exception exception)
            {
                // logging
            }
            return null;
        }

        private IList<UserTable> RetrieveAllEntitiesFiltered(Func<UserTable, bool> funcOperation)
        {
            using (var _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
            {
                return _dbContext.UserTables.Where(funcOperation).ToList();
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

        public UserTable GetEntityMatchingUsernameAndPassword(string username, string password)
        {
            try
            {
                return FindEntityMatching(u => u.UserName == username && u.Password == password);
            }
            catch (Exception exception)
            {
                // logging
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

        public bool? EntityExistMatchingId(long id)
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
            return null;
        }

        public bool? EntityExistMatchingFunc(Func<UserTable, bool> funcOperation)
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
            return null;
        }

        public bool? EntityExistMatchingUsernameAndPassword(string username, string password)
        {
            try
            {
                using (UserAbstractDbContext _dbContext = UserDbFactory.GetUserDbContext(_dbContextType))
                {
                    return _dbContext.UserTables.Any(u => u.UserName == username && u.Password == password);
                }
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }
    }
}