using System.Windows;

namespace Library.Views
{
    public partial class HubWindow : Window
    {
        private int currentUserId; // Определение переменной currentUserId

        public HubWindow(int userId) // Принимаем userId в конструкторе
        {
            InitializeComponent();
            currentUserId = userId; // Инициализируем currentUserId
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new UsersWindow();
            usersWindow.Show();
        }

        private void BooksInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно с книгами
            var booksWindow = new BooksInfoWindow();
            booksWindow.Show();
        }
        private void AvailableBooksButton_Click(object sender, RoutedEventArgs e)
        {
            var availableBooksWindow = new AvailableBooksWindow(currentUserId);
            availableBooksWindow.Show();
        }


        private void RentedBooksButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно с арендой книг
            var rentedBooksWindow = new RentedBooksWindow();
            rentedBooksWindow.Show();
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            var booksWindow = new BooksWindow();
            booksWindow.Show();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }

    }
}
