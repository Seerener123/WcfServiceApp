using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfServiceApp.Exceptions.Datacontacts;
using WcfServiceApp.Messaging.ServiceInterfaces;
using WcfServiceApp.Messaging.DataContracts.MessageContracts;
using MessageDbLib.DbRetrievals;
using MessageBaseDbLib.Constants;
using MessageDbLib.MessagingEntities;

namespace WcfServiceApp.Messaging.Services
{
    public class RetrieveMessageService : IRetrieveMessageService
    {
        public List<MessageTransactionInfoContract> GetMessageTransactionsMatchingUsernameAndReceiverEmail(string username, string recieverEmail)
        {
            try
            {
                RetrieveUserClass retrieveUser = new RetrieveUserClass(DbContextConstant.MsSqlDbContext);
                long senderId = retrieveUser.GetEntityMatchingFunc(u => u.UserName == username).Id;
                RetrieveMessageTransactionClass retrieveMessageTransaction = new RetrieveMessageTransactionClass(DbContextConstant.MsSqlDbContext);
                return retrieveMessageTransaction.GetAllEntitiesMatchingCondtionsEagerLoadAssociatedEntitiesFunc(t => t.Message, t => t.Message.SenderId == senderId)
                    .Select(m => CreateRetrieveMessageContract(username, m.EmailAddress, m.Message.MessageCreated.Value))
                    .ToList();
            }
            catch (Exception exception)
            {
                var error = new EntityErrorContract
                {
                    Message = exception.Message
                };
                throw new FaultException<EntityErrorContract>(error);
            }
        }

        private MessageTransactionInfoContract CreateRetrieveMessageContract(string senderName, string receiverName, DateTime createDate)
        {
            return new MessageTransactionInfoContract
            {
                SenderName = senderName,
                ReceiverName = receiverName,
                MessageCreated = createDate
            };
        }

        /*private void CodeDump()
        {
            using (var _dbcontext = new MessageDbContext())
            {
                long senderId = _dbcontext.UserTables.Single(u => u.UserName == username).Id;
                return _dbcontext.MessageTransactionTables.Include(t => t.Message).Where(t => t.EmailAddress == recieverEmail &&
                        (t.Message != null && t.Message.SenderId == senderId))
                    .Select(m => CreateRetrieveMessageContract(username, m.EmailAddress, m.Message.MessageCreated.Value))
                    .ToList();
            }
        }*/
    }
}
