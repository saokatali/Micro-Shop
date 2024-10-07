namespace Ordering.API.Domain.Events
{
    public class EventBase
    {
        public Guid CorRelationID { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
