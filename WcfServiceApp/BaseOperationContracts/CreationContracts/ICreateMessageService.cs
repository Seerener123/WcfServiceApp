using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.Messaging.DataContracts;

namespace WcfServiceApp.BaseOperationContracts.CreationContracts
{
    [ServiceContract]
    public interface ICreateMessageService
    {
        [OperationContract]
        void CreateMessage(MessageContract message);
    }
}
