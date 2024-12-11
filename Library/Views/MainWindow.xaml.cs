using System.Windows;
using Library.Data;
using Library.Models;
using Library.Views;

namespace Library
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                // Проверяем пользователя
                var user = UserDbHelper.ValidateUser(username, password);

                if (user == null)
                {
                    MessageBox.Show("Неправильное имя пользователя или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Перенаправляем в зависимости от роли
                if (user.Role == "Администратор")
                {
                    var hubWindow = new HubWindow(user.User_ID);
                    hubWindow.Show();
                }
                else if (user.Role == "Читатель")
                {
                    var rentBooksWindow = new RentBooksUserWindow(user.User_ID);
                    rentBooksWindow.Show();
                }
                else
                {
                    MessageBox.Show("Неизвестная роль пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
