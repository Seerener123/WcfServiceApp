using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbRetrievals;
using MessageDbLib.MessagingEntities;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RetrieveUserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RetrieveUserService.svc or RetrieveUserService.svc.cs at the Solution Explorer and start debugging.
    public class RetrieveUserService : IRetrieveUserService
    {
        public List<UserTable> GetAllUsers()
        {
            try
            {
                RetrieveUserClass retrieveUser = new RetrieveUserClass(DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
                IList<UserTable> user = retrieveUser.GetAllEntities();
                return user.ToList();
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

        private void ThrowErrorMessage(string message)
        {
            var error = new EntityErrorContract
            {
                Message = message
            };
            throw new FaultException<EntityErrorContract>(error);
        }
    }
}
