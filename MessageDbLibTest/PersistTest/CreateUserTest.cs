using System;
using System.Collections.Generic;
using System.Linq;
using MessageBaseDbLib.Constants;
using MessageDbLib.DbPersistances;
using MessageDbLib.DbRetrievals;
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
                FirstName = firstName,
                Surname = secondName,
                Dob = DateTime.Now,
                Gender = UserDataConstants.Male,
                UserName = firstName + "_" + secondName,
                Password = "password" + firstName + "_" + secondName
            };

            AdvancedUser advancedUser = new AdvancedUser()
            {
                FirstName = "advance " + firstName,
                Surname = "advance " + secondName,
                Dob = DateTime.Now,
                Gender = UserDataConstants.Male,
                UserName = "advance " + firstName + "_" + secondName,
                Password = "password" + firstName + "_" + secondName,
                ADVANCESTARTDATETIME = DateTime.Now,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50)
            };

            UserPersistant createUser = new UserPersistant(null, DbContextConstant.MsSqlDbContext);
            createUser.AddToPending(user);
            createUser.AddToPending(advancedUser);
            createUser.SaveChange();
        }

        //[TestMethod]
        public void CreateUserMysql()
        {
            Random randomNumber = new Random();
            string firstName = "UnitTestOne" + randomNumber.Next(1000000);
            string secondName = "UnitTestMYSQL" + randomNumber.Next(1000000);

            UserTable user = new UserTable()
            {
                FirstName = firstName,
                Surname = secondName,
                Dob = DateTime.Now,
                Gender = UserDataConstants.Male,
                UserName = firstName + "_" + secondName,
                Password = "password" + firstName + "_" + secondName
            };

            AdvancedUser advancedUser = new AdvancedUser()
            {
                FirstName = "advance " + firstName,
                Surname = "advance " + secondName,
                Dob = DateTime.Now,
                Gender = UserDataConstants.Male,
                UserName = "advance " + firstName + "_" + secondName,
                Password = "password" + firstName + "_" + secondName,
                ADVANCESTARTDATETIME = DateTime.Now,
                ADVANCEENDDATETIME = DateTime.Now.AddDays(50)
            };

            UserPersistant createUser = new UserPersistant(null, DbContextConstant.MySqlDbContext);
            createUser.AddToPending(user);
            createUser.AddToPending(advancedUser);
            createUser.SaveChange();
        }

        //[TestMethod]
        public void MigrateUserFromMssqlUserToMysqlUser()
        {
            RetrieveUserClass retrieveUser = new RetrieveUserClass(DbContextConstant.MsSqlDbContext);
            IList<UserTable> allCurrentMsUsers = retrieveUser.GetAllEntities().Take(10).ToList();
            Console.WriteLine("msuser count: " + allCurrentMsUsers.Count);

            UserPersistant userPersistant = new UserPersistant(allCurrentMsUsers, DbContextConstant.MySqlDbContext);
            userPersistant.SaveChange();
        }
    }
}
