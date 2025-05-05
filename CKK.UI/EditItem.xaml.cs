using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CKK.DB.Interfaces;
using CKK.DB.UOW;
using CKK.Persistance.Interfaces;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System.ComponentModel;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window    {
        private readonly UnitOfWork unitOfWork;
        private readonly IProductRepository products;
        public Product product { get; set; } = new Product();
        IConnectionFactory connectionFactory;
        public EditItem(IConnectionFactory _connectionFactory,Product _product)
        {
            product = _product;
            connectionFactory = _connectionFactory;
            unitOfWork = new UnitOfWork(connectionFactory);
            products = unitOfWork.Products;

            InitializeComponent();

            loadItem();
        }
        private void loadItem()
        {
            if(product.Id == 0)
            {
            var allProducts = unitOfWork.Products.GetAll();

            product = allProducts.FirstOrDefault();
            }

            if (product != null)
            {
                idTextBox.Text = product.Id.ToString();
                nameTextBox.Text = product.Name ?? string.Empty;
                priceTextBox.Text = product.Price.ToString();
                quantityTextBox.Text = product.Quantity.ToString();
            }
            else
            {
                MessageBox.Show("Product not found");
                return;
            }
        }

     

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }
            
            else
            {
                product.Name = nameTextBox.Text;
                product.Price = decimal.Parse(priceTextBox.Text);
                product.Quantity = int.Parse(quantityTextBox.Text);
                try
                {
                    products.Update(product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                this.DialogResult = true;
                
            }
            
        }

      

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

       
    }
}
