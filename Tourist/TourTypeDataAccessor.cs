using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class TourTypeDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand("select * from tour_type")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "tour_type");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update tour_type set tour_type_name=:tour_type_name where id=:id"),
                InsertCommand = new SqlCommand("insert into tour_type (tour_type_name) values (:tour_type_name) where id=:id"),
                DeleteCommand = new SqlCommand("delete from tour_type where id=:id")
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

            SqlParameter paramTourTypeName = new SqlParameter
            {
                SourceColumn = "tour_type_name",
                ParameterName = ":tour_type_name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramTourTypeName);
            dataAdapter.InsertCommand.Parameters.Add(paramTourTypeName);

            dataAdapter.Update(dataSet, "tour_type");
        }
    }
}
