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

        private void RefreshInventory()
        {
            unitOfWork.Products.GetAll();
            var allProducts = products.GetAll();
            productList = allProducts;
            inventoryListBox.ItemsSource = productList;
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

            //IMPLEMENTED NEW CODE WORKS
            if (itemListView.View is GridView gridView)
            {
                foreach (var column in gridView.Columns)
                {
                    column.Width = 0; // Reset width
                    column.Width = double.NaN; // Auto-size to content
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = itemListView.SelectedItem as Product;
            var selectedProd = inventoryListBox.SelectedItem as Product;

            if (itemListView.SelectedItems.Count == 0 && inventoryListBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select an item to delete.");
                    return;
                }
            
            try
            {
                if (selected != null)
                {
                    MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete this item?",
                        "Are you sure?", MessageBoxButton.YesNo);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        unitOfWork.Products.Delete(selected);
                        RefreshList();
                    }
                }
                else if (selectedProd != null)
                {
                    MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete this item?",
                        "Are you sure?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        unitOfWork.Products.Delete(selectedProd);
                        RefreshInventory();
                    }
                }
                    
            }
            finally 
            {
                
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
            var selected = itemListView.SelectedItem as Product;

            if (itemListView.SelectedItems.Count > 0)
            {

                if (selected != null)
                {
                    var product = unitOfWork.Products.GetById(selected.Id);
                    EditItem editItem = new EditItem(connectionFactory, product);
                    editItem.ShowDialog();

                    RefreshList();

                    //IMPLEMENTED NEW CODE WORKS
                    if (itemListView.View is GridView gridView)
                    {
                        foreach (var column in gridView.Columns)
                        {
                            column.Width = 0; // Reset width
                            column.Width = double.NaN; // Auto-size to content
                        }
                    }

                }

            }
            //NEW CODE WORKING ON IT FIGURING OUT HOW TO MAKE THE INVENTORYLISTBOX WORK WHEN AN ITEM IS SELECTED TO EDIT

            else if (inventoryListBox.SelectedItem is Product inventorySelected)
            {
                var product = unitOfWork.Products.GetById(inventorySelected.Id);
                EditItem editItem = new EditItem(connectionFactory, product);
                editItem.ShowDialog();

                RefreshInventory();
                RefreshList();

                if (itemListView.View is GridView gridView)
                {
                    foreach (var column in gridView.Columns)
                    {
                        column.Width = 0; // Reset width
                        column.Width = double.NaN; // Auto-size to content
                    }
                }
            }

            else
            {
                MessageBox.Show("Please select an item to edit.");
                return;
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                RefreshList();
                inventoryListBox.ItemsSource = null; // Clear the list
            }
            else
            {
                var filteredProducts = products.GetAll()
                    .Where(item => item.Name.IndexOf(searchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                inventoryListBox.ItemsSource = filteredProducts; // Bind the list to Product objects

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