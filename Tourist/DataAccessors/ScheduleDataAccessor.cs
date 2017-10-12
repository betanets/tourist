using Npgsql;

namespace Tourist
{
    class ScheduleDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = new NpgsqlCommand("select * from schedule")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "schedule");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                UpdateCommand = new NpgsqlCommand("update schedule set tour_date=:tour_date where id=:id"),
                InsertCommand = new NpgsqlCommand("insert into schedule (tour_date) values (:tour_date)"),
                DeleteCommand = new NpgsqlCommand("delete from schedule where id=:id")
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

            NpgsqlParameter paramTourDateU = new NpgsqlParameter
            {
                SourceColumn = "tour_date",
                ParameterName = ":tour_date"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramTourDateU);

            NpgsqlParameter paramTourDateI = new NpgsqlParameter
            {
                SourceColumn = "tour_date",
                ParameterName = ":tour_date"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramTourDateI);

            dataAdapter.Update(dataSet, "schedule");
        }
    }
}
