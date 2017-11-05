using System;
using Npgsql;
using System.Configuration;

namespace Tourist
{
    public class ConnectionFactory
    {
        public static AbstractConnection CreateConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(getConnectionString());
            AbstractConnection result = new AbstractConnection();

            result.connection = connection;
            return result;
        }

        private static String getConnectionString()
        {
            return ConfigurationManager.AppSettings["connectionString"];
        }
    }
}
