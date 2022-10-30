namespace IFramework.Domain.Core.Entities
{
    public interface IEntity
    {
    }

    public interface IEntity<TPrimaryKey> : IEntity
    {
        /// <summary>
        /// Entity'nin primary key olan property'si
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
