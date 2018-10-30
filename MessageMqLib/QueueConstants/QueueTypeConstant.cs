using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageMqLib.QueueConstants
{
    public class QueueTypeConstant
    {
        public const string MongoDbPersistentQueue = "MongoDbPersisentQueue";
        public const string MongoDbPersistentUserService = "MongoDbPersistentUserService";
        public const string MongoDbPersistentMessageService = "MongoDbPersistentMessageService";
    }
}
