using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class AbstractConnection : IDisposable
    {
        public SqlConnection connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

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
            SqlTransaction transaction = connection.BeginTransaction();

            AbstractTransaction result = new AbstractTransaction();

            result.transaction = transaction;
            return result;
        }
    }
}
