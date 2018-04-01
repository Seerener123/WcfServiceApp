using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.BaseOperationContracts.CreationContracts;
using WcfServiceApp.Messaging.DataContracts;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateMessageService : ICreateMessageService
    {
        public void CreateMessage(UserTable user, MessageContract message)
        {
            throw new NotImplementedException();
        }
    }
}