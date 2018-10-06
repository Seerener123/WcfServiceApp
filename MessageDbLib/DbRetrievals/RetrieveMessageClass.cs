using MessageBaseDbLib.DbRetrievalInterfaces;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MessageDbLib.DbRetrievals
{
    public class RetrieveMessageClass : IDbRetrieve<MessageTable>
    {
        private readonly string _dbContextType;

        public RetrieveMessageClass(string dbContextType)
        {
            _dbContextType = dbContextType;
        }

        public bool? EntityExistMatchingFunc(Func<MessageTable, bool> funcOperation)
        {
            try
            {
                return ExecuteEntityMatchingOperation(funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        public bool? EntityExistMatchingId(long id)
        {
            try
            {
                return ExecuteEntityMatchingOperation(mt => mt.Id == id);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private bool ExecuteEntityMatchingOperation(Func<MessageTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.Any(funcOperation);
            }
        }

        public IList<MessageTable> GetAllEntities()
        {
            try
            {
                return RetrieveAllEntities();
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private IList<MessageTable> RetrieveAllEntities()
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.ToList();
            }
        }

        public IList<MessageTable> GetAllEntitiesFunc(Func<MessageTable, bool> funcOperation)
        {
            try
            {
                return RetrieveAllEntitiesMatchingCondition(funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private IList<MessageTable> RetrieveAllEntitiesMatchingCondition(Func<MessageTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.Where(funcOperation).ToList();
            }
        }

        public MessageTable GetEntityMatchingFunc(Func<MessageTable, bool> funcOperation)
        {
            try
            {
                return RetrieveEntityMatchingCondition(funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        public MessageTable GetEntityMatchingId(long id)
        {
            try
            {
                return RetrieveEntityMatchingCondition(mt => mt.Id == id);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private MessageTable RetrieveEntityMatchingCondition(Func<MessageTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.SingleOrDefault(funcOperation);
            }
        }

        public IList<MessageTable> GetAllEntitiesEagerLoadAssociatedEntitiesFunc<TAssoEntity>(Expression<Func<MessageTable, TAssoEntity>> includedFunc)
        {
            try
            {
                return RetrieveAllEntitiesEagerLoadAssociatedEntities(includedFunc);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }
        // ICollection<MessageTransactionTable>
        private IList<MessageTable> RetrieveAllEntitiesEagerLoadAssociatedEntities<TAssoEntity>(Expression<Func<MessageTable, TAssoEntity>> includedFunc)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.Include(includedFunc).ToList();
            }
        }

        public IList<MessageTable> GetAllEntitiesMatchingCondtionsEagerLoadAssociatedEntitiesFunc<TAssoEntity>(Expression<Func<MessageTable, TAssoEntity>> includedFunc,
            Func<MessageTable, bool> funcOperation)
        {
            try
            {
                return RetrieveAllEntitiesEagerLoadAssociatedEntities(includedFunc, funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private IList<MessageTable> RetrieveAllEntitiesEagerLoadAssociatedEntities<TAssoEntity>(Expression<Func<MessageTable, TAssoEntity>> includedFunc,
            Func<MessageTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTables.Include(includedFunc).Where(funcOperation).ToList();
            }
        }
    }
}
