using MessageDbLib.BaseDbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDbLib.Exceptions
{
    public class InvalidEntityDeletionException<TEntity> : Exception where TEntity : IBaseEntity
    {
        public bool IsEntityNull
        {
            get
            {
                return Entity == null;
            }
        }

        public TEntity Entity { get; private set; }

        public InvalidEntityDeletionException(TEntity entity, string message, Exception innerException) : base(message, innerException)
        {
            Entity = entity;
        }
    }
}