using System;
using System.ServiceModel;
using MessageDbLib.DbContextFactorys;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
using MessageDbLib.MessagingEntities;
using WcfServiceApp.Exceptions.Datacontacts;
using WcfServiceApp.Messaging.ServiceInterfaces;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateUserService : ICreateUserService
    {
        public void CreateNewAdvancedUser(AdvancedUser user)
        {
            try
            {
                if (user.UserName != null && user.UserName != "" && UsernameAlreadyExist(user.UserName))
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
                UserName = user.UserName,
                Password = user.Password,
                Dob = user.Dob,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50d),
                ADVANCESTARTDATETIME = DateTime.Now
            };*/
            
            try
            {
                if (user.UserName != null && user.UserName != "" && UsernameAlreadyExist(user.UserName))
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
            return retrieveUser.EntityExistMatchingFunc(u => u.UserName == userName);
        }
    }
}