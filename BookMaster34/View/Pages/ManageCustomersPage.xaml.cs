using BookMaster34.AppData;
using BookMaster34.Models;
using BookMaster34.View.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookMaster34.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManageCustomersPage.xaml
    /// </summary>
    public partial class ManageCustomersPage : Page
    {
        private List<Customer> _customer = App.Get34Context().Customers.ToList();
        private Customer _selectedcustomer;
        public ManageCustomersPage()
        {
            InitializeComponent();
            _customer = App.Get34Context().Customers.ToList();
        }
        private void LoadData(List<Customer> customerList)
        {
            CustomersLV.ItemsSource = customerList;
        }
        private void SearchCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomersLV.Visibility = Visibility.Visible;
            string IdCustomer = CustomerIDTb.Text;
            string NameCustomer = NameCustomerTb.Text;

            if (string.IsNullOrWhiteSpace(IdCustomer) &&
            string.IsNullOrWhiteSpace(NameCustomer))
            {
                LoadData(_customer);
            }
            else
            {
                List<Customer> filteredCustomer = _customer.Where(customer => customer.Id.Contains(IdCustomer, StringComparison.OrdinalIgnoreCase) &&
                customer.Name.Contains(NameCustomer, StringComparison.OrdinalIgnoreCase)).ToList();
                LoadData(filteredCustomer);
            }
        }

        private void EditCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer? selectedCustomer = CustomersLV.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                EditCustomerWindow editCustomerWindow = new EditCustomerWindow(selectedCustomer);

                if (editCustomerWindow.ShowDialog() == true)
                {
                    CustomersLV.ItemsSource = _customer = App.Get34Context().Customers.ToList(); 
                }
            }
          else
            {
                FeedBackService.Error("Невозможно открыть окно для редактирования читателя.Сначала выберите его из списка");
            }

        }

        private void AddCustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerWindow editCustomerWindow = new EditCustomerWindow();
           if(editCustomerWindow.ShowDialog()==true)
            {
                CustomersLV.ItemsSource = _customer = App.Get34Context().Customers.ToList();
            }
        }

        private void CustomersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
