using Npgsql;

namespace Tourist
{
    class TourDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = new NpgsqlCommand("select * from tour")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "tour");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                UpdateCommand = new NpgsqlCommand("update tour set tour_name=:name, tour_descr=:descr, id_sight=:id_sight, id_schedule=:id_schedule, id_tour_type=:id_tour_type where id=:id"),
                InsertCommand = new NpgsqlCommand("insert into tour (tour_name, tour_descr, id_sight, id_schedule, id_tour_type) values (:name, :descr, :id_sight, :id_schedule, :id_tour_type) where id=:id"),
                DeleteCommand = new NpgsqlCommand("delete from tour where id=:id")
            };
            dataAdapter.UpdateCommand.Connection = aConnection.connection;
            dataAdapter.UpdateCommand.Transaction = aTransaction.transaction;
            dataAdapter.InsertCommand.Connection = aConnection.connection;
            dataAdapter.InsertCommand.Transaction = aTransaction.transaction;
            dataAdapter.DeleteCommand.Connection = aConnection.connection;
            dataAdapter.DeleteCommand.Transaction = aTransaction.transaction;

            NpgsqlParameter paramId = new NpgsqlParameter
            {
                SourceColumn = "id",
                ParameterName = ":id"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramId);
            dataAdapter.InsertCommand.Parameters.Add(paramId);
            dataAdapter.DeleteCommand.Parameters.Add(paramId);

            NpgsqlParameter paramName = new NpgsqlParameter
            {
                SourceColumn = "tour_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramName);
            dataAdapter.InsertCommand.Parameters.Add(paramName);

            NpgsqlParameter paramDescr = new NpgsqlParameter
            {
                SourceColumn = "tour_descr",
                ParameterName = ":descr"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramDescr);
            dataAdapter.InsertCommand.Parameters.Add(paramDescr);

            NpgsqlParameter paramIdSight = new NpgsqlParameter
            {
                SourceColumn = "id_sight",
                ParameterName = ":id_sight"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdSight);
            dataAdapter.InsertCommand.Parameters.Add(paramIdSight);

            NpgsqlParameter paramIdSchedule = new NpgsqlParameter
            {
                SourceColumn = "id_schedule",
                ParameterName = ":id_schedule"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdSchedule);
            dataAdapter.InsertCommand.Parameters.Add(paramIdSchedule);

            NpgsqlParameter paramIdTourType = new NpgsqlParameter
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
