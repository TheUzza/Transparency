using System;
using MySqlConnector;

namespace Transparency
{
    public class MysqlHandler
    {

        public static void TestMySQL()
        {
            MySqlDataBase sql = new MySqlDataBase();

            if (!sql.Connect("server=gsw.dnsalias.net;uid=brandtransparency;" +
                              "pwd=db23/M3rm2EA;database=brandtransparency"))
            {
                Console.WriteLine("Failed to connect to the database!");
                return;
            }
            Console.WriteLine("Connect to the database was successful.\n");

            string tableName = "hayday";

            CreateTable(sql, tableName);
            DeleteAllRows(sql, tableName);
            InsertRows(sql, tableName);
            UpdateRows(sql, tableName);
            ListAllRows(sql, tableName);
            FilteredRows(sql, tableName);

            Console.WriteLine("\nTest done...");
        }

        private static void CreateTable(MySqlDataBase sql, string tableName)
        {
            sql.ExecuteQuery($"CREATE TABLE IF NOT EXISTS `brandtransparency`.`{tableName}` " +
                             "(`index` INT NOT NULL, `name` VARCHAR(64) NULL, PRIMARY KEY (`index`)) ENGINE = InnoDB");
        }

        private static void DeleteAllRows(MySqlDataBase sql, string tableName)
        {
            sql.ExecuteQuery($"DELETE FROM {tableName}");
        }

        private static void InsertRows(MySqlDataBase sql, string tableName)
        {
            string[] animals = { "Chicken", "Cow", "Pig", "Sheep", "Goat" };

            for (int i = 0; i < animals.Length; i++)
            {
                sql.ExecuteQuery($"INSERT INTO {tableName} VALUES ({i + 1}, '{animals[i]}')");
            }
        }

        private static void UpdateRows(MySqlDataBase sql, string tableName)
        {
            sql.ExecuteQuery($"UPDATE {tableName} SET name = 'Hugo' WHERE name = 'Pelle'");
        }

        private static void ListAllRows(MySqlDataBase sql, string tableName)
        {
            Console.WriteLine("1: list all\n");

            try
            {
                MySqlCommand cmd = sql.GetSqlCommand($"SELECT * FROM {tableName}");
                sql.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }

        private static void FilteredRows(MySqlDataBase sql, string tableName)
        {
            Console.WriteLine("\n2: filtered list on the name 'Hugo'\n");

            try
            {
                MySqlCommand cmd = sql.GetSqlCommand($"SELECT * FROM {tableName} WHERE name = 'Hugo'");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}
