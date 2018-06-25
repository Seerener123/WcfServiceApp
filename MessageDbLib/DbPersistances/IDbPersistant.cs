﻿using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.DbPersistances
{
    public interface IDbPersistant<TEntity> where TEntity : IBaseEntity
    {
        void AddToPending(TEntity entity);
        void RemoveFromPending(TEntity entity);
        void SaveChange();
    }
}