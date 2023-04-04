using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace Transparency
{
	public class MySqlDataBase
	{
        private string connectionString =
         "server=gsw.dnsalias.net;uid=brandtransparency;pwd=db23/M3rm2EA;database=brandtransparency";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }


        public async Task CreateTableAsync()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();

                    string createTableQuery = @"CREATE TABLE IF NOT EXISTS test (
                                            id INT AUTO_INCREMENT PRIMARY KEY,
                                            qr_code_data TEXT NOT NULL
                                        );";

                    using (var command = new MySqlCommand(createTableQuery, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}

