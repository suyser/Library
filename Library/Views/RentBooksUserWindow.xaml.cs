using System;
using System.Windows;
using Library.Data;
using Library.Models;

namespace Library.Views
{
    public partial class RentBooksUserWindow : Window
    {
        private readonly int currentUserId;

        public RentBooksUserWindow(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            LoadRentedBooks();
        }

        // Метод для загрузки арендованных книг
        private void LoadRentedBooks()
        {
            try
            {
                var rentedBooks = RentedBooksDbHelper.GetRentedBooksByUser(currentUserId);
                RentedBooksListView.ItemsSource = rentedBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке арендованных книг: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для возврата книги
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (RentedBooksListView.SelectedItem is BookRentUser selectedBook)
            {
                try
                {
                    // Возвращаем книгу, передавая оба параметра: Book_ID и currentUserId (userId)
                    var returnedBook = RentedBooksDbHelper.ReturnBook(selectedBook.Book_ID, currentUserId);
                    if (returnedBook != null)
                    {

                        MessageBox.Show("Книга успешно возвращена!",
                            "Возврат книги", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Обновляем список арендованных книг
                        LoadRentedBooks();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Книга не найдена для возврата.",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при возврате книги: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для возврата.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // Обработчик для открытия окна с доступными книгами
        private void AvailableBooksButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var availableBooksWindow = new AvailableBooksWindow(currentUserId);
                availableBooksWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии окна доступных книг: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
