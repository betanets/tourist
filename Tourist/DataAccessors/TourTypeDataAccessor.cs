using Npgsql;

namespace Tourist
{
    class TourTypeDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = new NpgsqlCommand("select * from tourtype")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "tourtype");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                UpdateCommand = new NpgsqlCommand("update tourtype set tour_type_name=:tour_type_name where id=:id"),
                InsertCommand = new NpgsqlCommand("insert into tourtype (tour_type_name) values (:tour_type_name)"),
                DeleteCommand = new NpgsqlCommand("delete from tourtype where id=:id")
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

            NpgsqlParameter paramTourTypeNameU = new NpgsqlParameter
            {
                SourceColumn = "tour_type_name",
                ParameterName = ":tour_type_name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramTourTypeNameU);

            NpgsqlParameter paramTourTypeNameI = new NpgsqlParameter
            {
                SourceColumn = "tour_type_name",
                ParameterName = ":tour_type_name"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramTourTypeNameI);

            dataAdapter.Update(dataSet, "tourtype");
        }
    }
}
