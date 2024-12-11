using System.Windows;
using Library.Data;
using Library.Models;

namespace Library.Views
{
    public partial class BooksWindow : Window
    {
        public BooksWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                var books = BooksDbHelper.LoadUniqueBooks();
                BooksListView.ItemsSource = books;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
