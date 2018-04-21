using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageDbLib.DbPersistances;
using MessageDbLib.MessagingEntities;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateUserService : ICreateUserService
    {
        public void PersistNewUser(UserTable user)
        {
            /*var advanceUser = new AdvancedUser()
            {
                USERNAME = user.USERNAME,
                PASSWORD = user.PASSWORD,
                DOB = user.DOB,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50d),
                ADVANCESTARTDATETIME = DateTime.Now
            };*/
            PersistUser newUser = new PersistUser(null);
            newUser.AddItem(user);
            newUser.SaveChange();
        }
    }
}