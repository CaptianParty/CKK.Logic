using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using CKK.Persistance.Interfaces;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.Data.Common;
using System.Data;
using System.Windows.Controls.Primitives;

namespace CKK.UI
{
    public partial class MainWindow : Window
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IProductRepository products;

        List<Product> productList = new List<Product>();
        private readonly IConnectionFactory connectionFactory;
        public MainWindow()
        {
            connectionFactory = new DatabaseConnectionFactory();
            unitOfWork = new UnitOfWork(connectionFactory);
            products = unitOfWork.Products;

            InitializeComponent();



            LoginWPF login = new LoginWPF();
            login.ShowDialog();
            if (login.DialogResult == true)
            {
                unitOfWork.Products.GetAll();
                RefreshList();
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
            RefreshList();


        }

        private void RefreshList()
        {
            unitOfWork.Products.GetAll();
            var allProducts = products.GetAll();
            productList = allProducts;
            itemListView.ItemsSource = productList;

        }

        //FIXED

        private void RefreshListByName()
        {

            if (nameRadioButton != null && nameRadioButton.IsChecked == true)
            {
                var orderedByName = unitOfWork.Products.GetAll().OrderBy(p => p.Name).ToList();

                itemListView.ItemsSource = orderedByName;

            }

            if (idRadioButton != null && idRadioButton.IsChecked == true)
            {
                var orderecById = unitOfWork.Products.GetAll().OrderBy(p => p.Id).ToList();

                itemListView.ItemsSource = orderecById;
            }

        }





        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            Product newProd = new Product();

            if (nameTextBox.Text == "" || priceTextBox.Text == "" || quantityTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }
            newProd.Name = nameTextBox.Text;
            newProd.Price = decimal.Parse(priceTextBox.Text);
            newProd.Quantity = int.Parse(quantityTextBox.Text);

            unitOfWork.Products.Add(newProd);

            RefreshList();

            nameTextBox.Clear();
            priceTextBox.Clear();
            quantityTextBox.Clear();

        }


        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemListView.SelectedItems.Count > 0)
            {
                var selected = itemListView.SelectedItem as Product;

                if (selected != null)
                {
                    MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete this item?",
                        "Are you sure?", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        //changed from selected to 1
                        unitOfWork.Products.Delete(1);
                    }
                }
                RefreshList();
            }

        }

        private void numberTextBox_PreviewTextInput(object sender,
            System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, @"^[0-9.]*$");
        }

        //FIXED
        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            if (itemListView.SelectedItems.Count > 0)
            {
                var selected = itemListView.SelectedItem as Product;

                if (selected != null)
                {
                    var product = unitOfWork.Products.GetById(selected.Id);
                    EditItem editItem = new EditItem(connectionFactory, product);
                    editItem.ShowDialog();
                }
                RefreshList();

            }
           
         
            else
            {
                MessageBox.Show("Please select an item to edit.");
                return;
            }

        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                RefreshList();
                inventoryListBox.Items.Clear();
            }

            else
            {
                inventoryListBox.Items.Clear();

                foreach (var item in products.GetAll())
                {
                    if (item.Name.IndexOf(searchTextBox.Text,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        inventoryListBox.Items.Add($"Id number: {item.Id}," +
                            $"\nName: {item.Name}, " +
                            $"\nPrice: ${item.Price}," +
                            $"\nQuantity: {item.Quantity},\n");
                    }
                }

            }
        }
        private void sortRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (nameRadioButton != null && nameRadioButton.IsChecked == true)
            {
                RefreshListByName();
            }
            if (nameRadioButton != null && nameRadioButton.IsChecked == false)
            {
                RefreshList();
            }

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            RefreshList();
        }
    }
}