﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceApp.PracticeTs.Contracts;

namespace WcfServiceApp.PracticeTs.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransferService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TransferService.svc or TransferService.svc.cs at the Solution Explorer and start debugging.
    public class TransferService : ITransferService
    {
        public int Addition(PracticeContract practiceContract)
        {
            Console.WriteLine("Message" + practiceContract.Message);
            return practiceContract.Number + new Random().Next(100);
        }

        public void Send(PracticeContract practiceContract)
        {
            Console.WriteLine(practiceContract.Message);
        }
    }
}
