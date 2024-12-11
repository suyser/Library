using System;
using System.Windows;
using Library.Data;
using Library.Models;

namespace Library.Views
{
    public partial class AvailableBooksWindow : Window
    {
        private int currentUserId;

        public AvailableBooksWindow(int userId)
        {
            InitializeComponent();
            currentUserId = userId; // Получаем ID пользователя
            LoadAvailableBooks(); // Загружаем доступные книги при открытии окна
        }

        // Метод для загрузки доступных книг
        private void LoadAvailableBooks()
        {
            var availableBooks = AvailableBookDbHelper.GetAvailableBooks();
            AvailableBooksListView.ItemsSource = availableBooks;
        }

        // Обработчик для аренды книги
        private void RentBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = AvailableBooksListView.SelectedItem as AvailableBook;
            if (selectedBook != null)
            {
                // Логика аренды книги
                DateTime startRent = DateTime.Now;
                DateTime endRent = startRent.AddDays(14); // Аренда на 14 дней

                // Арендуем книгу
                RentedBookDbHelper.RentBook(selectedBook.Book_ID, currentUserId, startRent, endRent);

                // Удаляем книгу из доступных книг
                AvailableBookDbHelper.RemoveAvailableBook(selectedBook.Available_Books_ID);

                // Обновляем список доступных книг
                LoadAvailableBooks();

                MessageBox.Show("Книга успешно арендована!", "Аренда", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для аренды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
