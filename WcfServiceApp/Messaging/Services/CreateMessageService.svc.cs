using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.BaseOperationContracts.CreationContracts;
using WcfServiceApp.Messaging.DataContracts;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateMessageService : ICreateMessageService
    {
        public void CreateMessage(MessageContract message)
        {
            try
            {
                MessageTable newMessage = CreateNewMessage(message);
                MessagePersistant messagePersistant = new MessagePersistant(null);
                messagePersistant.AddItem(newMessage);
                messagePersistant.SaveChange();
            }
            catch (Exception exception)
            {
                //
            }
        }

        private MessageTable CreateNewMessage(IMessageContract messageContract)
        {
            UserTable user = RetrieveUser(messageContract.UserName);
            MessageTable newMessage = new MessageTable
            {
                MESSAGETEXT = messageContract.Message,
                SENDERID = user.ID,
                MESSAGECREATED = DateTime.Now
            };
            return newMessage;
        }

        private UserTable RetrieveUser(string userName)
        {
            RetrieveUserClass retrieveUser = new RetrieveUserClass();
            var user = retrieveUser.GetEntityMatchingFunc(u => u.USERNAME == userName);
            if (user == null)
            {
                throw new Exception();
            }
            return user;
        }
    }
}