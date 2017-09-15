using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Tourist
{
    class SightDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update sight set sight_name=:name, sight_descr=:descr where id=:id")
            };
            dataAdapter.UpdateCommand.Connection = aConnection.connection;
            dataAdapter.UpdateCommand.Transaction = aTransaction.transaction;

            SqlParameter paramId = new SqlParameter
            {
                SourceColumn = "id",
                ParameterName = ":id",
                SqlDbType = SqlDbType.Int
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramId);
            
            SqlParameter paramName = new SqlParameter
            {
                SourceColumn = "sight_name",
                ParameterName = ":name",
                SqlDbType = SqlDbType.VarChar
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramName);

            SqlParameter paramDescr = new SqlParameter
            {
                SourceColumn = "sight_descr",
                ParameterName = ":descr",
                SqlDbType = SqlDbType.VarChar
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramDescr);
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            //dataAdapter.Update(dataSet, "sight");
        }
    }
}
