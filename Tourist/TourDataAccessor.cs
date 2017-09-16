using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class TourDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand("select * from tour")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "tour");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update tour set tour_name=:name, tour_descr=:descr, id_sight=:id_sight, id_schedule=:id_schedule, id_tour_type=:id_tour_type where id=:id"),
                InsertCommand = new SqlCommand("insert into tour (tour_name, tour_descr, id_sight, id_schedule, id_tour_type) values (:name, :descr, :id_sight, :id_schedule, :id_tour_type) where id=:id"),
                DeleteCommand = new SqlCommand("delete from tour where id=:id")
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

            SqlParameter paramName = new SqlParameter
            {
                SourceColumn = "tour_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramName);
            dataAdapter.InsertCommand.Parameters.Add(paramName);

            SqlParameter paramDescr = new SqlParameter
            {
                SourceColumn = "tour_descr",
                ParameterName = ":descr"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramDescr);
            dataAdapter.InsertCommand.Parameters.Add(paramDescr);

            SqlParameter paramIdSight = new SqlParameter
            {
                SourceColumn = "id_sight",
                ParameterName = ":id_sight"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdSight);
            dataAdapter.InsertCommand.Parameters.Add(paramIdSight);

            SqlParameter paramIdSchedule = new SqlParameter
            {
                SourceColumn = "id_schedule",
                ParameterName = ":id_schedule"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdSchedule);
            dataAdapter.InsertCommand.Parameters.Add(paramIdSchedule);

            SqlParameter paramIdTourType = new SqlParameter
            {
                SourceColumn = "id_tour_type",
                ParameterName = ":id_tour_type"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdTourType);
            dataAdapter.InsertCommand.Parameters.Add(paramIdTourType);

            dataAdapter.Update(dataSet, "tour");
        }
    }
}
