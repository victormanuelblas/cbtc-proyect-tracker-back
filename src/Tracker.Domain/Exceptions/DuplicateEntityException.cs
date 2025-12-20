namespace Tracker.Domain.Exceptions
{
    public class DuplicateEntityException : DomainException
    {
        public String EntityName { get; }
        public String DuplicateField { get; }

        public DuplicateEntityException(string entity, string field, String value)
            : base($"The {entity} entity, field '{field}' with value '{value}' already exists.", 409)
        {
            EntityName = entity;
            DuplicateField = field;
        }
    }
}