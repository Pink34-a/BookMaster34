using BookMaster34.AppData;
using BookMaster34.Models;
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

namespace BookMaster34.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window

    {
        private List<City> _cityes;
        public EditCustomerWindow()
        {
            InitializeComponent();
            _cityes = App.Get34Context().Cities.ToList();
            LoadCities();
            Title = "Добавить читателя";
            AddBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Collapsed;
            IDclienTb.Text = GenerateId();
        }
        public EditCustomerWindow(Customer selectedCustomer)
        {
            InitializeComponent();
            _cityes = App.Get34Context().Cities.ToList();
            LoadCities();
            Title = "Редактирование читателя";
            AddBtn.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Visible;
            IDclienTb.Text = selectedCustomer.Id;
            DataContext = selectedCustomer;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer();
        }

        private void AddCustomer()
        {
            try
            {
                //Проверка заполнения всех полей
                if (string.IsNullOrWhiteSpace(ClientNameTb.Text) ||
                string.IsNullOrWhiteSpace(AddressClientTb.Text) ||
                string.IsNullOrWhiteSpace(PhoneCostomerTb.Text) || string.IsNullOrWhiteSpace(EmailCostomerTb.Text))
                {
                    FeedBackService.Warning("Заполните все поля!");
                }
                else
                {
                    Customer newCustomer = new Customer()
                    {
                        Id = IDclienTb.Text,
                        Name = ClientNameTb.Text,
                        Address = AddressClientTb.Text,
                        CityId = (int)ZipCityCmb.SelectedValue,
                        Phone = PhoneCostomerTb.Text,
                        Email = EmailCostomerTb.Text,
                        Zip = ZipCityTb.Text
                    };
                    App.Get34Context().Customers.Add(newCustomer);
                    App.Get34Context().SaveChanges();
                    FeedBackService.Warning("Читатель успешно добавлен!");
                    DialogResult = true;
                }
            }
            catch (Exception exception)
            {
                FeedBackService.Error(exception);
            }
        }

        private void LoadCities()
        {
            ZipCityCmb.ItemsSource = _cityes;

        }
        private void EditCustomer()
        {
            try
            {
                App.Get34Context().SaveChanges();
                FeedBackService.Information("Данные читателя успешно сохранены!");
            }
            catch (Exception ex) 
            {
                FeedBackService.Error(ex);
            }
        }
       

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCustomer();
        }
        private string GenerateId()
        {
            int lastId = Convert.ToInt32(App.Get34Context().Customers.Max(x => x.Id).Substring(1));
            //=> "C1015" =>"1015"=>1015

            ++lastId;// => 1015 +1 +>1016
            return $"C{lastId}";//"C1016"
        }
    }
    }

