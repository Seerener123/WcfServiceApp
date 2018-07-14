using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageDbLib.Constants;
using MessageDbLib.DbPersistances;
using MessageDbLib.MessagingEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageDbLibTest.PersistTest
{
    [TestClass]
    public class CreateUserTest
    {
        [TestMethod]
        public void CreateUserMsSql()
        {
            Random randomNumber = new Random();
            string firstName = "UnitTestOne" + randomNumber.Next(1000000);
            string secondName = "UnitTestMsql" + randomNumber.Next(1000000);

            UserTable user = new UserTable()
            {
                FIRSTNAME = firstName,
                SURNAME = secondName,
                DOB = DateTime.Now,
                GENDER = UserDataConstants.Male,
                USERNAME = firstName + "_" + secondName,
                PASSWORD = "password" + firstName + "_" + secondName
            };

            AdvancedUser advancedUser = new AdvancedUser()
            {
                FIRSTNAME = "advance " + firstName,
                SURNAME = "advance " + secondName,
                DOB = DateTime.Now,
                GENDER = UserDataConstants.Male,
                USERNAME = "advance " + firstName + "_" + secondName,
                PASSWORD = "password" + firstName + "_" + secondName,
                ADVANCESTARTDATETIME = DateTime.Now,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50)
            };

            UserPersistant createUser = new UserPersistant(null, UserDbContextConstant.MsSqlUserDbContext);
            createUser.AddToPending(user);
            createUser.AddToPending(advancedUser);
            createUser.SaveChange();
        }

        [TestMethod]
        public void CreateUserMysql()
        {
            Random randomNumber = new Random();
            string firstName = "UnitTestOne" + randomNumber.Next(1000000);
            string secondName = "UnitTestMYSQL" + randomNumber.Next(1000000);

            UserTable user = new UserTable()
            {
                FIRSTNAME = firstName,
                SURNAME = secondName,
                DOB = DateTime.Now,
                GENDER = UserDataConstants.Male,
                USERNAME = firstName + "_" + secondName,
                PASSWORD = "password" + firstName + "_" + secondName
            };

            AdvancedUser advancedUser = new AdvancedUser()
            {
                FIRSTNAME = "advance " + firstName,
                SURNAME = "advance " + secondName,
                DOB = DateTime.Now,
                GENDER = UserDataConstants.Male,
                USERNAME = "advance " + firstName + "_" + secondName,
                PASSWORD = "password" + firstName + "_" + secondName,
                ADVANCESTARTDATETIME = DateTime.Now,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50)
            };

            UserPersistant createUser = new UserPersistant(null, UserDbContextConstant.MySqlUserDbContext);
            createUser.AddToPending(user);
            createUser.AddToPending(advancedUser);
            createUser.SaveChange();
        }
    }
}
