using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.Messaging.DataContracts.MessageContracts;

namespace WcfServiceApp.Messaging.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRetrieveMessageService" in both code and config file together.
    [ServiceContract]
    public interface IRetrieveMessageService
    {
        [OperationContract]
        List<MessageTransactionInfoContract> GetMessageTransactionsMatchingUsernameAndReceiverEmail(string username, string recieverEmail);
    }
}
