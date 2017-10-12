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
                InsertCommand = new NpgsqlCommand("insert into tour (tour_name, tour_descr, id_sight, id_schedule, id_tour_type) values (:name, :descr, :id_sight, :id_schedule, :id_tour_type)"),
                DeleteCommand = new NpgsqlCommand("delete from tour where id=:id")
            };
            dataAdapter.UpdateCommand.Connection = aConnection.connection;
            dataAdapter.UpdateCommand.Transaction = aTransaction.transaction;
            dataAdapter.InsertCommand.Connection = aConnection.connection;
            dataAdapter.InsertCommand.Transaction = aTransaction.transaction;
            dataAdapter.DeleteCommand.Connection = aConnection.connection;
            dataAdapter.DeleteCommand.Transaction = aTransaction.transaction;

            NpgsqlParameter paramIdU = new NpgsqlParameter
            {
                SourceColumn = "id",
                ParameterName = ":id"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdU);

            NpgsqlParameter paramIdD = new NpgsqlParameter
            {
                SourceColumn = "id",
                ParameterName = ":id"
            };
            dataAdapter.DeleteCommand.Parameters.Add(paramIdD);

            NpgsqlParameter paramNameU = new NpgsqlParameter
            {
                SourceColumn = "tour_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramNameU);

            NpgsqlParameter paramNameI = new NpgsqlParameter
            {
                SourceColumn = "tour_name",
                ParameterName = ":name"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramNameI);

            NpgsqlParameter paramDescrU = new NpgsqlParameter
            {
                SourceColumn = "tour_descr",
                ParameterName = ":descr"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramDescrU);

            NpgsqlParameter paramDescrI = new NpgsqlParameter
            {
                SourceColumn = "tour_descr",
                ParameterName = ":descr"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramDescrI);

            NpgsqlParameter paramIdSightU = new NpgsqlParameter
            {
                SourceColumn = "id_sight",
                ParameterName = ":id_sight"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdSightU);

            NpgsqlParameter paramIdSightI = new NpgsqlParameter
            {
                SourceColumn = "id_sight",
                ParameterName = ":id_sight"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramIdSightI);

            NpgsqlParameter paramIdScheduleU = new NpgsqlParameter
            {
                SourceColumn = "id_schedule",
                ParameterName = ":id_schedule"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdScheduleU);

            NpgsqlParameter paramIdScheduleI = new NpgsqlParameter
            {
                SourceColumn = "id_schedule",
                ParameterName = ":id_schedule"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramIdScheduleI);

            NpgsqlParameter paramIdTourTypeU = new NpgsqlParameter
            {
                SourceColumn = "id_tour_type",
                ParameterName = ":id_tour_type"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramIdTourTypeU);

            NpgsqlParameter paramIdTourTypeI = new NpgsqlParameter
            {
                SourceColumn = "id_tour_type",
                ParameterName = ":id_tour_type"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramIdTourTypeI);

            dataAdapter.Update(dataSet, "tour");
        }
    }
}
