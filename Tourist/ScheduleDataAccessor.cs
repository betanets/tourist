using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class ScheduleDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand("select * from schedule")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "schedule");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update schedule set tour_date=:tour_date where id=:id"),
                InsertCommand = new SqlCommand("insert into schedule (tour_date) values (:tour_date) where id=:id"),
                DeleteCommand = new SqlCommand("delete from schedule where id=:id")
            };
            dataAdapter.UpdateCommand.Connection = aConnection.connection;
            dataAdapter.UpdateCommand.Transaction = aTransaction.transaction;
            dataAdapter.InsertCommand.Connection = aConnection.connection;
            dataAdapter.InsertCommand.Transaction = aTransaction.transaction;
            dataAdapter.DeleteCommand.Connection = aConnection.connection;
            dataAdapter.DeleteCommand.Transaction = aTransaction.transaction;

            SqlParameter paramId = new SqlParameter
            {
                SourceColumn = "id",
                ParameterName = ":id"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramId);
            dataAdapter.InsertCommand.Parameters.Add(paramId);
            dataAdapter.DeleteCommand.Parameters.Add(paramId);

            SqlParameter paramTourDate = new SqlParameter
            {
                SourceColumn = "sight_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramTourDate);
            dataAdapter.InsertCommand.Parameters.Add(paramTourDate);

            dataAdapter.Update(dataSet, "schedule");
        }
    }
}
