using System;
using System.Collections.Generic;
using System.Data;
using Library.Models;
using Npgsql;

namespace Library.Data
{
    public static class UserDbHelper
    {

        public static void AddUser(string fullName, string role, string password)
        {
            var query = "INSERT INTO Users (fullName, roles, \"passwords\") VALUES (@FullName, @Role, @Password);"; // Исправлено на "password"

            var parameters = new[]
            {
                new NpgsqlParameter("@FullName", fullName),
                new NpgsqlParameter("@Role", role),
                new NpgsqlParameter("@Password", password) // Пароль будет добавлен в правильное поле
            };

            DbHelper.ExecuteNonQuery(query, parameters);
        }

        // Загрузка пользователей из базы данных
        public static List<User> LoadUsers()
        {
            var query = "SELECT * FROM \"users\";";
            var table = DbHelper.ExecuteQuery(query);
            var users = new List<User>();

            foreach (DataRow row in table.Rows)
            {
                users.Add(new User
                {
                    User_ID = Convert.ToInt32(row["userID"]),
                    Full_Name = row["fullName"].ToString(),
                    Role = row["roles"].ToString(),
                    Password = row["passwords"].ToString() // Добавили поле Password
                });
            }

            return users;
        }

        // Добавление нового пользователя в базу данных
        public static void AddUser(User user)
        {
            var query = $"INSERT INTO \"Users\" (\"fullName\", \"roles\", \"passwords\") VALUES ('{user.Full_Name}', '{user.Role}', '{user.Password}');";
            DbHelper.ExecuteNonQuery(query);
        }

        // Обновление информации о пользователе
        public static void UpdateUser(User user)
        {
            var query = $"UPDATE \"Users\" SET \"fullName\" = '{user.Full_Name}', \"roles\" = '{user.Role}', \"passwords\" = '{user.Password}' WHERE \"userID\" = {user.User_ID};";
            DbHelper.ExecuteNonQuery(query);
        }

        // Удаление пользователя из базы данных
        public static void DeleteUser(int userId)
        {
            var query = $"DELETE FROM \"Users\" WHERE \"userID\" = {userId};";
            DbHelper.ExecuteNonQuery(query);
        }

        public static User ValidateUser(string username, string password)
        {
            var query = $@"
                SELECT * 
                FROM ""users"" 
                WHERE ""fullname"" = '{username}' AND ""passwords"" = '{password}'";

            var table = DbHelper.ExecuteQuery(query);

            if (table.Rows.Count == 0)
                return null;

            var row = table.Rows[0];

            return new User
            {
                User_ID = Convert.ToInt32(row["userID"]),
                Full_Name = row["fullName"].ToString(),
                Role = row["roles"].ToString()
            };
        }
    }
}
