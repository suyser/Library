using System.Collections.Generic;
using System.Windows;
using Library.Models;
using Library.Data;

namespace Library.Views
{
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                var users = UserDbHelper.LoadUsers();
                UsersListView.ItemsSource = users;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
