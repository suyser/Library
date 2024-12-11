using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Library.Models;

namespace Library.Data
{
    public static class BookInfoDbHelper
    {
        // Load book information from the database
        public static List<BookInfo> LoadBookInfo()
        {
            var query = "SELECT * FROM \"bookinfo\";";
            var table = DbHelper.ExecuteQuery(query);
            var books = new List<BookInfo>();
            

            foreach (DataRow row in table.Rows)
            {
                books.Add(new BookInfo
                {
                    Book_Info_ID = Convert.ToInt32(row["bookInfoID"]),
                    Name = row["namess"].ToString(),
                    Author = row["author"].ToString()
                });
            }

            return books;
        }

    }
}
