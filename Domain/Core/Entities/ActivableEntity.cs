namespace IFramework.Domain.Core.Entities
{
    public class ActivableEntity<TPrimaryKey> : Entity<TPrimaryKey>, IActivableEntity
    {
        /// <summary>
        /// Entity'nin durum bilgisini belirtir.
        /// </summary>
        public virtual Status Status { get; protected set; }
        public virtual void Active()
        {
            Status = Status.Active;
        }
        public virtual void Passive()
        {
            Status = Status.Passive;
        }
        public virtual void Deleted()
        {
            Status = Status.Delete;
        }
    }
}
