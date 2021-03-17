using aspnetcore_event_driven.Data;

namespace aspnetcore_event_driven.DDD.Event
{
    public class ItemPurchasedEvent : DomainEvent
    {
        public Purchase Item { get; }
        public ItemPurchasedEvent(Purchase item)
        {
            Item = item;
        }
    }
}