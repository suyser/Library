using System;
using System.Data;
using Npgsql;

namespace Library.Data
{
    public static class DbHelper
    {
        private static readonly string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=89513861312;Database=Library;Encoding=UTF-8";

        // Execute a query that does not return results (e.g., INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Execute a SELECT query and return the result as a DataTable
        public static DataTable ExecuteQuery(string query, params NpgsqlParameter[] parameters)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        // Create a parameter for a query
        public static NpgsqlParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value ?? DBNull.Value);
        }
    }
}
