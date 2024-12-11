using Library.Models;
using System.Data;

namespace Library.Data
{
    public static class AvailableBookDbHelper
    {
        public static List<AvailableBook> GetAvailableBooks()
        {
            // Исправленный запрос для получения доступных книг
            var query = @"
                SELECT 
                    ab.availableBooksID, 
                    b.bookID, 
                    bi.bookInfoID, 
                    bi.namess, 
                    bi.author
                FROM AvailableBooks ab
                JOIN Books b ON ab.bookID = b.bookID
                JOIN BookInfo bi ON b.bookInfoID = bi.bookInfoID;
            ";

            var table = DbHelper.ExecuteQuery(query);

            var availableBooks = new List<AvailableBook>();
            foreach (DataRow row in table.Rows)
            {
                availableBooks.Add(new AvailableBook
                {
                    Available_Books_ID = Convert.ToInt32(row["availableBooksID"]),
                    Book_ID = Convert.ToInt32(row["bookID"]),
                    Book_Info_ID = Convert.ToInt32(row["bookInfoID"]),
                    Name = row["namess"].ToString(),
                    Author = row["author"].ToString()
                });
            }

            return availableBooks;
        }
        // Метод для удаления книги из доступных книг
        public static void RemoveAvailableBook(int availableBookId)
        {
            var query = "DELETE FROM availablebooks WHERE availableBooksID = @Available_Books_ID";

            var parameters = new[]
            {
            DbHelper.CreateParameter("@Available_Books_ID", availableBookId)
        };

            DbHelper.ExecuteNonQuery(query, parameters);
        }


        // Добавление книги в таблицу доступных книг
        public static void AddAvailableBook(AvailableBook book)
        {
            var insertQuery = @"
                INSERT INTO AvailableBooks (bookID) 
                VALUES (@BookId);
            ";

            DbHelper.ExecuteNonQuery(insertQuery, DbHelper.CreateParameter("@BookId", book.Book_ID));
        }

    }
}
