using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.CKK.DB.Interfaces
{
    public interface IUnitOfWork<TRepo>
    {
        IProductRepository<TRepo> Products { get; }
        IOrderRepository<TRepo> Orders { get; }
        IShoppingCartRepository<TRepo> ShoppingCarts { get; }
    }
}
