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

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {
        public StoreItem storeItem;
        public EditItem(StoreItem si)
        {
            storeItem = si;
            InitializeComponent();
            loadItem();
        }

        private void loadItem()
        {
            idTextBox.Text = storeItem.Product.Id.ToString();
            nameTextBox.Text = storeItem.Product.Name;
            priceTextBox.Text = storeItem.Product.Price.ToString();
            quantityTextBox.Text = storeItem.Quantity.ToString();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }else
            {
                storeItem.Product.Name = nameTextBox.Text;
                storeItem.Product.Price = decimal.Parse(priceTextBox.Text);
                storeItem.Quantity = int.Parse(quantityTextBox.Text);
                this.DialogResult = true;
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
