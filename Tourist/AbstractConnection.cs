using Npgsql;
using System;

namespace Tourist
{
    public class AbstractConnection : IDisposable
    {
        public NpgsqlConnection connection { get; set; }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public void Dispose()
        {
            Close();
        }

        public AbstractTransaction BeginTransaction()
        {
            NpgsqlTransaction transaction = connection.BeginTransaction();

            AbstractTransaction result = new AbstractTransaction();

            result.transaction = transaction;
            return result;
        }
    }
}
