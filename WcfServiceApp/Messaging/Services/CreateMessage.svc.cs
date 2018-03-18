using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.BaseOperationContracts.CreationContracts;

namespace WcfServiceApp.Messaging.Services
{
    public class CreateMessage : ICreate
    {
        public int Create(int number)//MessageContract message
        {
            System.Console.WriteLine("Test " + number);
            return number * 2;
        }
    }
}
