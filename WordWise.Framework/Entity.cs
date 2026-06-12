using System.ComponentModel.DataAnnotations;

namespace WordWise.Framework
{
    public class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }

        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
