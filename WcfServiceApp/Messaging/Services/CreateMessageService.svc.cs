using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.DbContexts;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.Exceptions.Datacontacts;
using WcfServiceApp.Messaging.DataContracts;
using System.Data.Entity;
using MessageDbLib.Constants;
using MessageDbLib.DbContextFactorys;
using WcfServiceApp.Messaging.ServiceInterfaces;

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

                using (var _dbcontext = new MessageDbContext())
                {
                    var messages = _dbcontext.MessageTables.Include(m => m.User)
                        .ToList();
                }
                MessageTable newMessage = CreateNewMessage(message);
                PersistMessage(newMessage);
                CreateMessageTransaction(message, newMessage);
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
            var error = new EntityErrorContract
            {
                Message = message
            };
            throw new FaultException<EntityErrorContract>(error);
        }
    }
}