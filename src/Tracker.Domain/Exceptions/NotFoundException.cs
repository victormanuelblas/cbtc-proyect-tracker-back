namespace Tracker.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public String EntityName { get; }
        public int EntityId { get; }

        public NotFoundException(int entityId, string entityName)
            : base($"The {entityName} entity with key '{entityId}' was not found.", 404)
        {
            EntityName = entityName;
            EntityId = entityId;
        }
    }
}