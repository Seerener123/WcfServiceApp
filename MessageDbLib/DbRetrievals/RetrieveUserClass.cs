using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbRetrievals
{
    public class RetrieveUserClass : IDbRetreive<UserTable>
    {
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
            using (var _dbContext = new MessageDbContext())
            {
                return _dbContext.UserTables
                    .ToList();
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
                return this.FindEntityMatching(x => x.ID == id);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private UserTable FindEntityMatching(Func<UserTable, bool> funcOperation)
        {
            using (var _dbContext = new MessageDbContext())
            {
                return _dbContext.UserTables
                    .SingleOrDefault(funcOperation);
            }
        }
    }
}