using CKK.DB.CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Xml.Linq;

namespace CKK.DB.CKK.DB.Repository
{
    public class OrderRepository : IOrderRepository<Order>
    {
        
        public int CustomerId { get; set; }
        public int OrderId { get; set;}
        public int Quantity { get; set; }


        public int Add(Order entity)
        {//List<Order> orders = GET ALL();
            List<Order> orders = new List<Order>();

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity can not be found.");
            }

            if (entity.OrderId == 0)
            {
                throw new Exception("Order ID cannot be 0.");
            }

            var newOrderId = Add(entity);
            var existingOrder = GetOrderByCustomerId(newOrderId + 1);

            if (existingOrder != null)
            {
                return entity.OrderId;
            }
            else
            {
                throw new Exception("Order has been created.");
            }
        }

        public int Delete(int id)
        {//List<Order> orders = GET ALL();

            List<Order> orders = new List<Order>();
            if (id == OrderId)
            {
                Delete(id);
            }
            else
            {
                throw new Exception("Order ID does not exist.");
            }
            return 0;
        }

        public List<Order> GetAll()
        {//List<Order> orders = GET ALL();
            List<Order> orders = new List<Order>();
            foreach (var order in GetAll())
            {
                if(order == null)
                {
                    throw new Exception("Order does not exist.");
                }
            }
            return GetAll();
        }


        public Order GetById(int id)
        {//List<Order> orders = GET ALL();
            List<Order> orders = new List<Order>();
            if (id == OrderId)
            {
                return GetById(id);
            }
            else
            {
                throw new Exception("Order ID does not exist.");
            }
        }

        
        public Order GetOrderByCustomerId(int customerId)
        {//List<Order> orders = GET ALL();
            List <Order> orders = new List<Order>();
            var order = orders.FirstOrDefault(o => o.CustomerId == customerId);

            if (customerId != null && customerId == CustomerId)
            {
                return order;
            }

            //MAYBE         if (order != null)    {return order;}
            else
            {
                throw new Exception("Customer ID does not exist.");
            }
        }

        public int Update(Order entity)
        {//List<Order> orders = GET ALL();
            List<Order> orders = new List<Order>();
           
            if (entity != null)
            {
                var existingOrder = orders.FirstOrDefault(o => o.OrderId == entity.OrderId);
                if (existingOrder != null)
                {
                    existingOrder.CustomerId = entity.CustomerId;
                    existingOrder.OrderNumber= entity.OrderNumber;


                    return 1; 
                }
            }
            return 0; 
        }
    }
}
