using MessageBaseDbLib.BasePocoInterfaces;
using System;

namespace MessageDbLib.Exceptions.Deletions
{
    public class InvalidEntityDeletionException<TEntity> : Exception, IBaseEntityException<TEntity> where TEntity : IBaseEntity
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