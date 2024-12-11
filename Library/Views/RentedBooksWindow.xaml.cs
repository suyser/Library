using System;
using System.Collections.Generic;
using System.Windows;
using Library.Models;
using Library.Data;

namespace Library.Views
{
    public partial class RentedBooksWindow : Window
    {
        public RentedBooksWindow()
        {
            InitializeComponent();
            LoadRentedBooks();
        }

        // Загрузка данных аренды книг
        private void LoadRentedBooks()
        {
            try
            {
                var rentedBooks = RentedBookDbHelper.LoadRentedBooks();
                RentedBooksListView.ItemsSource = rentedBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных аренды книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
