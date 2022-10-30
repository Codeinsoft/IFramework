namespace IFramework.Domain.Core.Entities
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Entity'nin primary key property'sidir.
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>

        public Entity()
        {

        }
    }
}
