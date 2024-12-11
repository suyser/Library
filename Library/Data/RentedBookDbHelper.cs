using System;
using System.Collections.Generic;
using System.Data;
using Library.Models;

namespace Library.Data
{
    public static class RentedBookDbHelper
    {

        public static List<RentedBook> LoadRentedBooks()
        {
            var query = "SELECT * FROM \"rentedbooks\";";
            var table = DbHelper.ExecuteQuery(query);
            var rentedBooks = new List<RentedBook>();

            foreach (DataRow row in table.Rows)
            {
                rentedBooks.Add(new RentedBook
                {
                    Rented_Books_ID = Convert.ToInt32(row["rentedBooksID"]),
                    Book_ID = Convert.ToInt32(row["bookID"]),
                    User_ID = Convert.ToInt32(row["userID"]),
                    Time_start_rent = Convert.ToDateTime(row["timeStartRent"]),
                    Time_rent = Convert.ToDateTime(row["timeRent"])
                });
            }

            return rentedBooks;
        }
        public static List<RentedBook> GetRentedBooksByUser(int userId)
        {
            var query = "SELECT * FROM rentedbooks WHERE userID = @User_ID";
            var parameters = new[] { DbHelper.CreateParameter("@User_ID", userId) };
            var table = DbHelper.ExecuteQuery(query, parameters);

            var rentedBooks = new List<RentedBook>();
            foreach (DataRow row in table.Rows)
            {
                rentedBooks.Add(new RentedBook
                {
                    Rented_Books_ID = Convert.ToInt32(row["rentedBooksID"]),
                    Book_ID = Convert.ToInt32(row["bookID"]),
                    User_ID = Convert.ToInt32(row["userID"]),
                    Time_start_rent = Convert.ToDateTime(row["timeStartRent"]),
                    Time_rent = Convert.ToDateTime(row["timeRent"])
                });
            }

            return rentedBooks;
        }

        public static void RentBook(int bookId, int userId, DateTime startRent, DateTime endRent)
        {
            var query = @"
                INSERT INTO rentedbooks (bookID, userID, timeStartRent, timeRent)
                VALUES (@Book_ID, @User_ID, @Time_start_rent, @Time_rent)";

            var parameters = new[]
            {
                DbHelper.CreateParameter("@Book_ID", bookId),
                DbHelper.CreateParameter("@User_ID", userId),
                DbHelper.CreateParameter("@Time_start_rent", startRent),
                DbHelper.CreateParameter("@Time_rent", endRent)
            };

            DbHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
