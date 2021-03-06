﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBaseDbLib.BasePocoInterfaces
{
    public interface IMessage : IBaseEntity
    {
        string MessageText { get; set; }
        long? SenderId { get; set; }
        DateTime? MessageCreated { get; set; }
    }
}
