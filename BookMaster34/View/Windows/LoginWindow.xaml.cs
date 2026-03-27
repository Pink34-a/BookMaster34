using BookMaster34.AppData;
using BookMaster34.Models;
using System.Windows;

namespace BookMaster34.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();


        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                Administrator administrator = App.Get34Context().Administrators.FirstOrDefault(administrator => administrator.Username == LoginTb.Text && administrator.Password == PasswordPb.Password);
                if (administrator != null)
                {
                    if (RememberCb.IsChecked == true) CredentialService.SaveCredentials(LoginTb.Text, PasswordPb.Password);
                    else CredentialService.ClearCredentials();

                        FeedBackService.Information("Успешная авторизация");
                
                        //DialogResult возвращает результат работы диалогового окна akuna matata
                        DialogResult = true;
                }
                else
                {
                    FeedBackService.Error("Пользователь не найден");
                }
                CredentialService.Administrator = administrator;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private bool Validate ()
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                FeedBackService.Warning("Введите логин.");
                    LoginTb.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                FeedBackService.Warning("Введите пароль.");
                LoginTb.Focus();
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CredentialService.AutoLogin && !string.IsNullOrWhiteSpace(CredentialService.SavedLogin))
            {
                LoginTb.Text= CredentialService.SavedLogin;
                PasswordPb.Password= CredentialService.SavedPassword;
                RememberCb.IsChecked = CredentialService.AutoLogin;
            }
        }
    }
}
