using MessageDbLib.MessagingEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp.Messaging.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICreateUserService" in both code and config file together.
    [ServiceContract]
    public interface ICreateUserService
    {
        [OperationContract]
        void CreateNewUser(UserTable user);

        [OperationContract]
        void CreateNewAdvancedUser(AdvancedUser user);
    }
}
