namespace ApexiBee.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Guid id, string entity)
            : base($"No {entity} found with id {id}")
        {
        }

        public NotFoundException(string entity)
            : base($"No {entity} found")
        {
        }
    }
}
