using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp.Messaging.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRetrieveUserService" in both code and config file together.
    [ServiceContract]
    public interface IRetrieveUserService
    {
        [OperationContract]
        List<UserTable> GetAllUsers();
    }
}
