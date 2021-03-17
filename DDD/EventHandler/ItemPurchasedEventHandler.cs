using System.Threading;
using System.Threading.Tasks;
using aspnetcore_event_driven.Data;
using aspnetcore_event_driven.DDD.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_event_driven.DDD.EventHandler
{
    public class ItemPurchasedEventHandler : INotificationHandler<DomainEventNotification<ItemPurchasedEvent>>
    {
        private readonly ApplicationDbContext _context;

        public ItemPurchasedEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DomainEventNotification<ItemPurchasedEvent> notification, CancellationToken cancellationToken)
        {
            var purchaseItem = notification.DomainEvent.Item;
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == purchaseItem.ProductId, cancellationToken);
            if (product != null)
            {
                product.RemainingStock += purchaseItem.Quantity;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}