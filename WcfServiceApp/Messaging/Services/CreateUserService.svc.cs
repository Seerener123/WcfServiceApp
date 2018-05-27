using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.DbPersistances;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.Exceptions.Datacontacts;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateUserService : ICreateUserService
    {
        public void CreateNewAdvancedUser(AdvancedUser user)
        {
            try
            {
                PersistNewUser(user);
            }
            catch (Exception exception)
            {
                ThrowErrorMessage(exception.Message);
            }
        }

        public void CreateNewUser(UserTable user)
        {
            /*var advanceUser = new AdvancedUser()
            {
                USERNAME = user.USERNAME,
                PASSWORD = user.PASSWORD,
                DOB = user.DOB,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50d),
                ADVANCESTARTDATETIME = DateTime.Now
            };*/
            
            try
            {
                PersistNewUser(user);
            }
            catch (Exception exception)
            {
                ThrowErrorMessage(exception.Message);
            }
        }

        private void PersistNewUser(UserTable user)
        {
            UserPersistant newUser = new UserPersistant(null);
            newUser.AddItem(user);
            newUser.SaveChange();
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