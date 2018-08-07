using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.Constants;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
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
                if (user.USERNAME != null && user.USERNAME != "" && UsernameAlreadyExist(user.USERNAME))
                {
                    throw new InvalidOperationException("This username has already been taken.");
                }
                PersistNewUser(user);
            }
            catch (InvalidOperationException exception)
            {
                ThrowUserExistErrorMessage(exception.Message);
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
                if (user.USERNAME != null && user.USERNAME != "" && UsernameAlreadyExist(user.USERNAME))
                {
                    throw new InvalidOperationException("This username has already been taken.");
                }
                PersistNewUser(user);
            }
            catch (InvalidOperationException exception)
            {
                ThrowUserExistErrorMessage(exception.Message);
            }
            catch (Exception exception)
            {
                ThrowErrorMessage(exception.Message);
            }
        }

        private void PersistNewUser(UserTable user)
        {
            UserPersistant newUser = new UserPersistant(null, DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
            newUser.AddToPending(user);
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

        private void ThrowUserExistErrorMessage(string message)
        {
            var error = new UserExistErrorContract
            {
                Message = message
            };
            throw new FaultException<UserExistErrorContract>(error);
        }

        private bool UsernameAlreadyExist(string userName)
        {
            RetrieveUserClass retrieveUser = new RetrieveUserClass(DatabaseOptionConfigRetriever.DatabaseOptionAppSetting);
            return retrieveUser.EntityExistMatchingFunc(u => u.USERNAME == userName);
        }
    }
}