using MessageDbLib.Constants;
using MessageDbLib.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbContextFactorys
{
    public static class MessageDbFactory
    {
        private static Dictionary<string, Func<MessageAbstractDbContext>> _messageDbFactoryOptionsList = new Dictionary<string, Func<MessageAbstractDbContext>>();

        static MessageDbFactory()
        {
            _messageDbFactoryOptionsList.Add(MessageDbContextConstant.MsSqlMessageDbContext, () => { return new MessageDbContext(); });
            _messageDbFactoryOptionsList.Add(MessageDbContextConstant.MySqlMessageDbContext, () => { return new MessageMySqlDbContext(); });
        }

        public static MessageAbstractDbContext GetMessageDbContext(string dbcontextOption)
        {
            try
            {
                return _messageDbFactoryOptionsList[dbcontextOption].Invoke();
            }
            catch (Exception excetion)
            {
                throw new InvalidOperationException("The option assigned to the MessageDbFactory does not exist in the factories internal collection", excetion);
            }
        }
    }
}
