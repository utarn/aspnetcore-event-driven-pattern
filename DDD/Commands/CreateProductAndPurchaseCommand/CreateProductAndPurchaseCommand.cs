using System.Threading;
using System.Threading.Tasks;
using aspnetcore_event_driven.Data;
using aspnetcore_event_driven.DDD.Event;
using MediatR;

namespace aspnetcore_event_driven.DDD.Commands.CreateProductAndPurchaseCommand
{
    public class CreateProductAndPurchaseCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public int Purchase { get; set; }

        public class CreateProductAndPurchaseCommandHandler : IRequestHandler<CreateProductAndPurchaseCommand, bool>
        {
            private readonly ApplicationDbContext _context;
            public CreateProductAndPurchaseCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(CreateProductAndPurchaseCommand request, CancellationToken cancellationToken)
            {
                var newProduct = new Product() {
                    Name = request.Name,
                    RemainingStock = 0
                };
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync(cancellationToken);
                
                var newPurchase = new Purchase() {
                    ProductId = newProduct.Id,
                    Quantity = request.Purchase
                };
                
                newPurchase.DomainEvents.Add(new ItemPurchasedEvent(newPurchase));
                await _context.Purchases.AddAsync(newPurchase);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}