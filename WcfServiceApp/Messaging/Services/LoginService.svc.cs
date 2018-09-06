using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbRetrievals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.Exceptions.Datacontacts;
using WcfServiceApp.Messaging.ServiceInterfaces;

namespace WcfServiceApp.Messaging.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoginService.svc or LoginService.svc.cs at the Solution Explorer and start debugging.
    public class LoginService : ILoginService
    {
        public bool ExecuteEncryptedLoginIn(string encryptedUser, string encryptedPassword)
        {
            try
            {
                string decryptedUsername = encryptedUser;
                string decryptedPassword = encryptedPassword;

                RetrieveUserClass retrieveUsers = new RetrieveUserClass(DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
                if (retrieveUsers.EntityExistMatchingUsernameAndPassword(decryptedUsername, decryptedPassword))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ThrowErrorMessage<LoginErrorContract>(exception.Message);
            }

            return false;
        }

        private void ThrowErrorMessage<TContract>(string message) where TContract : IErrorsContract, new()
        {
            TContract error = new TContract
            {
                Message = message
            };
            throw new FaultException<TContract>(error);
        }
    }
}
