using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CKK.Persistance.Models;
using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using CKK.Persistance.Interfaces;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace CKK.UI
{
    public partial class MainWindow : Window
    {
        public IStore Store;
        ObservableCollection<StoreItem> storeItems = new ObservableCollection<StoreItem>();

        public MainWindow()
        {
            Store = new FileStore();
            LoginWPF login = new LoginWPF();
            login.ShowDialog();
            if (login.DialogResult == true)
            {
                InitializeComponent();
            }
            else
            {
                this.Close();
            }
            lvItemList.ItemsSource = storeItems;
            RefreshList();
        }

        private void RefreshList()
        {
            storeItems.Clear();
            foreach (var item in Store.GetStoreItems())
            {
                storeItems.Add(item);
            }
        }

        private void RefreshListByName()
        {
            storeItems.Clear();
            foreach (var item in Store.GetStoreItems().OrderBy(x=>x.Product.Name))
            {
                storeItems.Add(item);
            }
        }

        private void RefreshListByQuantity()
        {
            storeItems.Clear();
            foreach (var item in Store.GetStoreItems().OrderByDescending(x => x.Quantity))
            {
                storeItems.Add(item);
            }
        }

        private void RefreshListByPrice()
        {
            storeItems.Clear();
            foreach (var item in Store.GetStoreItems().OrderByDescending(x => x.Product.Price))
            {
                storeItems.Add(item);
            }
        }

        private void RefreshListById()
        {
            if (inventoryListBox != null) 
            {
                storeItems.Clear();
                foreach (var item in Store.GetStoreItems().OrderBy(x => x.Product.Id))
                {
                    storeItems.Add(item);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "" && nameTextBox.Text != "" 
                && priceTextBox.Text != "" && quantityTextBox.Text != "")
            {
                Product product = new Product();
                product.Name = nameTextBox.Text;
                product.Price = Convert.ToDecimal(priceTextBox.Text);

                Store.AddStoreItem(product, Convert.ToInt32(quantityTextBox.Text));
                RefreshList();
            }

            if (nameTextBox != null && priceTextBox != null && quantityTextBox != null)
            {
                nameTextBox.Clear();
                priceTextBox.Clear();
                quantityTextBox.Clear();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvItemList.SelectedItems.Count > 0)
            {
                var selected = lvItemList.SelectedItem as StoreItem;

                if (selected != null)
                {
                    MessageBoxResult result = MessageBox.Show(this, "Are you sure you want to delete this item?", 
                        "Are you sure?", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        Store.DeleteStoreItem(selected.Product.Id);
                    }
                }
                RefreshList();
                inventoryListBox.Items.Remove(inventoryListBox.SelectedItem);
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

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (StoreItem)lvItemList.SelectedItem;
            if (selected != null)
            {
                EditItem editItem = new EditItem(selected);
                editItem.ShowDialog();
                if (editItem.DialogResult == true)
                {
                    RefreshList();
                    if (Store is ISavable store)
                    {
                        store.Save();
                    }
                }
            }
        }

        // NewCode
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

                foreach (var item in Store.GetStoreItems())
                {
                    if (item.Product.Name.IndexOf(searchTextBox.Text, 
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                            inventoryListBox.Items.Add($"Id number: {item.Product.Id}," +
                                $"\nName: {item.Product.Name}, " +
                                $"\nPrice: ${item.Product.Price}," +
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
            else if (quantityRadioButton != null && quantityRadioButton.IsChecked == true)
            {
                RefreshListByQuantity();
            }
            else if (priceRadioButton != null && priceRadioButton.IsChecked == true)
            {
                RefreshListByPrice();
            }
            else if (idRadioButton.IsChecked == true)
            {
                RefreshListById();
            }
        }

        //ADDED TO CLEAR THE INVENTORY LISTBOX WHEN THE WINDOW IS LOADED
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (inventoryListBox != null)
            {
                inventoryListBox.Items.Clear();
            }
        }

       
    }
}