using System.Transactions;
using Domain.Entities;

namespace ASP.NET_MVC_Core._Course.DomainEvents;

public class ProductAdded: IDomainEvent
{
    public Product Product { get; }

    public ProductAdded(Product product)
    {
        Product = product;
    }

   
}