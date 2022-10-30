namespace IFramework.Domain.Core.Entities
{
    public interface IActivableEntity
    {
        /// <summary>
        /// Entity'nin durum bilgisini belirtir.
        /// </summary>
        Status Status { get; }
    }
}
