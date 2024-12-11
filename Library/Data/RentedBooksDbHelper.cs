using System;
using System.Collections.Generic;
using System.Data;
using Library.Models;
using Npgsql;

namespace Library.Data
{
    public static class RentedBooksDbHelper
    {
        // Получение арендованных книг по ID пользователя
        public static List<BookRentUser> GetRentedBooksByUser(int userId)
        {
            var query = @"
                SELECT 
                    rb.bookID,
                    bi.bookInfoID,
                    bi.namess,
                    bi.author,
                    rb.timeStartRent,
                    rb.timeRent
                FROM RentedBooks rb
                JOIN Books b ON rb.bookID = b.bookID
                JOIN BookInfo bi ON b.bookInfoID = bi.bookInfoID
                WHERE rb.userID = @UserId;
            ";

            var table = DbHelper.ExecuteQuery(query, DbHelper.CreateParameter("@UserId", userId));
            var rentedBooks = new List<BookRentUser>();

            foreach (DataRow row in table.Rows)
            {
                rentedBooks.Add(new BookRentUser
                {
                    Book_ID = Convert.ToInt32(row["bookID"]),
                    Book_Info_ID = Convert.ToInt32(row["bookInfoID"]),
                    Name = row["namess"].ToString(),
                    Author = row["author"].ToString(),
                    TimeStartRent = Convert.ToDateTime(row["timeStartRent"]),
                    TimeRent = Convert.ToDateTime(row["timeRent"])
                });
            }

            return rentedBooks;
        }

        // Возврат книги
        public static AvailableBook ReturnBook(int bookId, int userId)
        {
            // Получаем информацию о книге для возврата
            var selectQuery = @"
                SELECT rb.bookID, bi.bookInfoID, bi.namess, bi.author
                FROM RentedBooks rb
                JOIN Books b ON rb.bookID = b.bookID
                JOIN BookInfo bi ON b.bookInfoID = bi.bookInfoID
                WHERE rb.bookID = @BookId AND rb.userID = @UserId;
            ";

            var table = DbHelper.ExecuteQuery(selectQuery,
                DbHelper.CreateParameter("@BookId", bookId),
                DbHelper.CreateParameter("@UserId", userId));

            if (table.Rows.Count == 0)
            {
                throw new Exception("Книга для возврата не найдена.");
            }

            var row = table.Rows[0];
            var returnedBook = new AvailableBook
            {
                Book_ID = Convert.ToInt32(row["bookID"]),
                Book_Info_ID = Convert.ToInt32(row["bookInfoID"]),
                Name = row["namess"].ToString(),
                Author = row["author"].ToString()
            };

            // Удаляем книгу из таблицы арендованных
            var deleteQuery = "DELETE FROM RentedBooks WHERE Book_ID = @BookId AND User_ID = @UserId;";
            DbHelper.ExecuteNonQuery(deleteQuery,
                DbHelper.CreateParameter("@BookId", bookId),
                DbHelper.CreateParameter("@UserId", userId));

            // Добавляем книгу обратно в таблицу доступных книг
            AddAvailableBook(returnedBook);

            return returnedBook;
        }

        // Метод для добавления книги в таблицу доступных книг
        private static void AddAvailableBook(AvailableBook book)
        {
            var insertQuery = @"
                INSERT INTO AvailableBooks (bookID)
                VALUES (@BookID);
            ";

            DbHelper.ExecuteNonQuery(insertQuery, DbHelper.CreateParameter("@BookID", book.Book_ID));
        }
    }
}
