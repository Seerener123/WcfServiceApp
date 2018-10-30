using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using MessageDbLib.DbContexts;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.Exceptions.Datacontacts;
using WcfServiceApp.Messaging.DataContracts;
using System.Data.Entity;
using MessageDbLib.DbContextFactorys;
using WcfServiceApp.Messaging.ServiceInterfaces;
using MessageMqLib.MqProducerClasses;
using MessageMqLib.QueueConstants;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateMessageService : ICreateMessageService
    {
        public void CreateMessage(MessageContract message)
        {
            try
            {
                if (message.EmailAccounts == null || message.EmailAccounts.Count <= 0)
                {
                    throw new InvalidOperationException("Message contract does not have ant emails attahed.");
                }

                /*using (var _dbcontext = new MessageDbContext())
                {
                    var messages = _dbcontext.MessageTables.Include(m => m.User)
                        .ToList();
                    // System.Data.Entity;
                }*/

                MessageTable newMessage = CreateNewMessage(message);
                PersistMessage(newMessage);
                CreateMessageTransaction(message, newMessage);
                PersistMessageToMongoDbService(newMessage);
            }
            catch (FaultException<MessageQueueErrorContract> exception)
            {
                throw;
            }
            catch (Exception exception)
            {
                RerouteErrorMessage(exception.Message);
            }
        }

        private MessageTable CreateNewMessage(IMessageContract messageContract)
        {
            UserTable user = RetrieveUser(messageContract.UserName);
            MessageTable newMessage = new MessageTable
            {
                MessageText = messageContract.Message,
                SenderId = user.Id,
                MessageCreated = messageContract.MessageCreated
            };
            return newMessage;
        }

        private UserTable RetrieveUser(string userName)
        {
            RetrieveUserClass retrieveUser = new RetrieveUserClass(DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
            var user = retrieveUser.GetEntityMatchingFunc(u => u.UserName == userName);
            if (user == null)
            {
                throw new Exception();
            }
            return user;
        }

        private void PersistMessage(MessageTable message)
        {
            MessagePersistant messagePersistant = new MessagePersistant(null, DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
            messagePersistant.AddToPending(message);
            messagePersistant.SaveChange();
        }

        private void CreateMessageTransaction(IMessageContract messageContract, 
            MessageTable message)
        {
            List<MessageTransactionTable> messageTransactions = new List<MessageTransactionTable>();
            
            foreach (var emailAddress in messageContract.EmailAccounts)
            {
                var messageTransaction = new MessageTransactionTable
                {
                    EmailAddress = emailAddress,
                    MessageId = message.Id,
                    MessageReceived = false
                };
                messageTransactions.Add(messageTransaction);
            }

            PersistMessageTransaction(messageTransactions);
        }

        private void PersistMessageTransaction(List<MessageTransactionTable> messageTransactions)
        {
            MessageTransactionPersistant transactionPersistant = new MessageTransactionPersistant(messageTransactions, 
                DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
            transactionPersistant.SaveChange();
        }

        private void RerouteErrorMessage(string message)
        {
            EntityErrorContract error = new EntityErrorContract
            {
                Message = message
            };
            throw new FaultException<EntityErrorContract>(error);
        }

        private void PersistMessageToMongoDbService(MessageTable message)
        {
            try
            {
                RabbitMqProducerClass rabbitMqProducer = new RabbitMqProducerClass(QueueTypeConstant.MongoDbPersistentUserService, 
                    QueueTypeConstant.MongoDbPersistentUserService);
                rabbitMqProducer.ExecuteMessageQueueing(message);
            }
            catch (Exception exception)
            {
                MessageQueueErrorContract error = new MessageQueueErrorContract()
                {
                    Message = "Error encountered when trying to queue to message queue.",
                    ExceptionMessage = exception.Message
                };
                throw new FaultException<MessageQueueErrorContract>(error);
            }
        }
    }
}