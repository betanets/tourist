using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class AbstractTransaction
    {
        public SqlTransaction transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
