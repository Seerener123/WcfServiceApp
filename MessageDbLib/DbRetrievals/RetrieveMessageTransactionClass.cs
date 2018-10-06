using MessageBaseDbLib.DbRetrievalInterfaces;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbContexts;
using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbRetrievals
{
    public class RetrieveMessageTransactionClass : IDbRetrieve<MessageTransactionTable>
    {
        private readonly string _dbContextType;

        public RetrieveMessageTransactionClass(string dbContextType)
        {
            _dbContextType = dbContextType;
        }

        public bool? EntityExistMatchingFunc(Func<MessageTransactionTable, bool> funcOperation)
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

        private bool ExecuteEntityMatchingOperation(Func<MessageTransactionTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.Any(funcOperation);
            }
        }

        public IList<MessageTransactionTable> GetAllEntities()
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

        private IList<MessageTransactionTable> RetrieveAllEntities()
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.ToList();
            }
        }

        public IList<MessageTransactionTable> GetAllEntitiesFunc(Func<MessageTransactionTable, bool> funcOperation)
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

        private IList<MessageTransactionTable> RetrieveAllEntitiesMatchingCondition(Func<MessageTransactionTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.Where(funcOperation).ToList();
            }
        }

        public MessageTransactionTable GetEntityMatchingFunc(Func<MessageTransactionTable, bool> funcOperation)
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

        public MessageTransactionTable GetEntityMatchingId(long id)
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

        private MessageTransactionTable RetrieveEntityMatchingCondition(Func<MessageTransactionTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.SingleOrDefault(funcOperation);
            }
        }

        public IList<MessageTransactionTable> GetAllEntitiesEagerLoadAssociatedEntitiesFunc<TAssoEntity>(Expression<Func<MessageTransactionTable, TAssoEntity>> includedFunc)
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

        private IList<MessageTransactionTable> RetrieveAllEntitiesEagerLoadAssociatedEntities<TAssoEntity>(Expression<Func<MessageTransactionTable, TAssoEntity>> includedFunc)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.Include(includedFunc).ToList();
            }
        }

        public IList<MessageTransactionTable> GetAllEntitiesMatchingCondtionsEagerLoadAssociatedEntitiesFunc<TAssoEntity>(Expression<Func<MessageTransactionTable, TAssoEntity>> includedFunc,
            Func<MessageTransactionTable, bool> funcOperation)
        {
            try
            {
                return RetrieveAllEntitiesMatchingConditionsEagerLoadAssociatedEntities(includedFunc, funcOperation);
            }
            catch (Exception exception)
            {
                //
            }
            return null;
        }

        private IList<MessageTransactionTable> RetrieveAllEntitiesMatchingConditionsEagerLoadAssociatedEntities<TAssoEntity>(Expression<Func<MessageTransactionTable, TAssoEntity>> includedFunc,
            Func<MessageTransactionTable, bool> funcOperation)
        {
            using (MessageAbstractDbContext _dbContext = MessageDbFactory.GetMessageDbContext(_dbContextType))
            {
                return _dbContext.MessageTransactionTables.Include(includedFunc).Where(funcOperation).ToList();
            }
        }
    }
}
