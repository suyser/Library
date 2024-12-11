using System;
using System.Collections.Generic;
using System.Data;
using Library.Models;

namespace Library.Data
{
    public static class BooksDbHelper
    {
        // Метод для загрузки уникальных экземпляров книг с их количеством
        public static List<Book> LoadUniqueBooks()
        {
            var query = @"
                SELECT 
                    b.bookInfoID, 
                    bi.namess, 
                    bi.author, 
                    COUNT(b.bookID) AS Quantity
                FROM 
                    Books b
                JOIN 
                    BookInfo bi ON b.bookInfoID = bi.bookInfoID
                GROUP BY 
                    b.bookInfoID, bi.namess, bi.author;
            ";

            var table = DbHelper.ExecuteQuery(query);
            var books = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                books.Add(new Book
                {
                    Book_Info_ID = Convert.ToInt32(row["bookInfoID"]),
                    Name = row["namess"].ToString(),
                    Author = row["author"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"])
                });
            }

            return books;
        }
        public static byte[] GetBookImage(int bookInfoId)
        {
            var query = $@"
        SELECT 
            Image 
        FROM 
            bookImages 
        WHERE 
            bookInfoID = {bookInfoId}";

            var table = DbHelper.ExecuteQuery(query);

            if (table.Rows.Count == 0)
                return null;

            return (byte[])table.Rows[0]["Image"];
        }

    }
}
