using System.Collections.Generic;
using System.Windows;
using Library.Models;
using Library.Data;

namespace Library.Views
{
    public partial class BooksInfoWindow : Window
    {
        public BooksInfoWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                var books = BookInfoDbHelper.LoadBookInfo();
                BooksInfoListView.ItemsSource = books;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
