using Npgsql;

namespace Tourist
{
    class SightDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = new NpgsqlCommand("select * from sight")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "sight");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                UpdateCommand = new NpgsqlCommand("update sight set sight_name=:name, sight_descr=:descr where id=:id"),
                InsertCommand = new NpgsqlCommand("insert into sight (sight_name, sight_descr) values (:name, :descr) where id=:id"),
                DeleteCommand = new NpgsqlCommand("delete from sight where id=:id")
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
                SourceColumn = "sight_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramName);
            dataAdapter.InsertCommand.Parameters.Add(paramName);

            NpgsqlParameter paramDescr = new NpgsqlParameter
            {
                SourceColumn = "sight_descr",
                ParameterName = ":descr"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramDescr);
            dataAdapter.InsertCommand.Parameters.Add(paramDescr);

            dataAdapter.Update(dataSet, "sight");
        }
    }
}
