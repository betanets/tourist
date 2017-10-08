using System;
using Npgsql;

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
            //return ConfigurationManager.AppSettings["connectionString"];
            return "Server=127.0.0.1;Port=5432;Database=tourist;User Id=postgres;Password=DNR;";
        }
    }
}
