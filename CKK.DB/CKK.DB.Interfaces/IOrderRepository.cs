using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.DB.CKK.DB.Interfaces
{
    public interface IOrderRepository<TRepo>: IGenericRepository <Order>
    {
        Order GetOrderByCustomerId(int customerId);
    }
}
