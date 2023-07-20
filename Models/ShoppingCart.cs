namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer? _Customer;
        private ShoppingCartItem _product1;
        private ShoppingCartItem _product2;
        private ShoppingCartItem _product3;
        public ShoppingCart(Customer cust)
        {
            _Customer = cust;
        }

          public int GetCustomerId(int _Customer)
        {
            return _Customer;
        }

        public ShoppingCartItem AddProduct(Product prod) 
        {    
                return AddProduct(prod, 1);
        }

        public ShoppingCartItem GetProductById(int id)
        {
            if (_product1 != null && id == _product1.GetProduct().GetId())
            {
                
                return _product1;
            }

            if (_product2 != null && id == _product2.GetProduct().GetId())
            {
                ;
                return _product2;
            }

            if (_product3 != null && id == _product3.GetProduct().GetId())
            {
              
                return _product3;
            }
            return null;


        }

        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {if (quantity<0) 
            {
                return null;
            }
            if (_product1 != null && prod.GetId() == _product1.GetProduct().GetId())
            {
                _product1.SetQuantity(_product1.GetQuantity() + quantity);
                return _product1;
            }
            
            if (_product2 != null && prod.GetId() == _product2.GetProduct().GetId())
            {
                _product2.SetQuantity(_product2.GetQuantity() + quantity);
                return _product2;
            }
        
            if (_product3 != null && prod.GetId() == _product3.GetProduct().GetId())
            {
                _product3.SetQuantity(_product2.GetQuantity() + quantity);
                return _product3;
            }


            if (_product1 == null)
            {
                _product1 = new ShoppingCartItem(prod, quantity);
                return _product1;
            }
            
            
            if (_product2 == null)
            {
                _product2 = new ShoppingCartItem(prod, quantity);
                return _product3;
            }


            if (_product3 == null)
            {
                _product3 = new ShoppingCartItem(prod, quantity);
                return _product3;
            }
                return null;
        }
        public ShoppingCartItem RemoveProduct(Product product, int quantity)
        {
            if (_product1 != null && product.GetId() == _product1.GetProduct().GetId())
            {
                if ((_product1.GetQuantity() - quantity) >= 0)
                {
                    _product1.SetQuantity(_product1.GetQuantity() - quantity);

                    return _product1;
                }
                else 
                {
                    _product1.SetQuantity(0);
                    _product1 = null;
                    return _product1;
                }
                }

            if (_product2 != null && product.GetId() == _product2.GetProduct().GetId())
            {
                if ((_product2.GetQuantity() - quantity) >= 0)
                {
                    _product2.SetQuantity(_product2.GetQuantity() - quantity);

                    return _product2;
                }
                else 
                {
                   _product2.SetQuantity(0);
                    _product2 = null;
                    return _product2;
                }
            }

            if (_product3 != null && product.GetId() == _product3.GetProduct().GetId())
            {
                if ((_product3.GetQuantity() - quantity) >= 0)
            {
                    _product3.SetQuantity(_product2.GetQuantity() - quantity);
                    
                    return _product3;
            }

            else
                {
                    _product3.SetQuantity(0);
                    _product3 = null;
                    return _product3;
                }
            }
            
            return null;
        }
        public decimal GetTotal()
        {
            decimal total = 0;
            if (_product1!=null ) 
                {
                    total = total + _product1.GetProduct().GetPrice() * _product1.GetQuantity();
                }
            
            if (_product2 != null)
            {
                total = total + _product2.GetProduct().GetPrice() * _product2.GetQuantity();
            }
          
            if (_product3 != null)
            {
                total = total + _product3.GetProduct().GetPrice() * _product3.GetQuantity();
            }
            return total;
        }
        public ShoppingCartItem GetProduct(int prodNum)
        {
            if (prodNum == 1)
            {
                return _product1;
            }
            if (prodNum == 2 )
            {
                return _product2;
            }
            if (prodNum == 3)
            {
                return _product3;
            }

            return null;
        }
        
    }
}
