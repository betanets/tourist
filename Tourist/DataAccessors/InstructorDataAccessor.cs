using Npgsql;

namespace Tourist
{
    class InstructorDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = new NpgsqlCommand("select * from instructor")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "instructor");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter
            {
                UpdateCommand = new NpgsqlCommand("update instructor set surname=:surname, forename=:forename, patronymic=:patronymic, id_schedule=:id_schedule, id_tour_type=:id_tour_type where id=:id"),
                InsertCommand = new NpgsqlCommand("insert into instructor (surname, forename, patronymic, id_schedule, id_tour_type) values (:surname, :forename, :patronymic, :id_schedule, :id_tour_type)"),
                DeleteCommand = new NpgsqlCommand("delete from instructor where id=:id")
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

            NpgsqlParameter paramSurnameU = new NpgsqlParameter
            {
                SourceColumn = "surname",
                ParameterName = ":surname"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramSurnameU);

            NpgsqlParameter paramSurnameI = new NpgsqlParameter
            {
                SourceColumn = "surname",
                ParameterName = ":surname"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramSurnameI);

            NpgsqlParameter paramForenameU = new NpgsqlParameter
            {
                SourceColumn = "forename",
                ParameterName = ":forename"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramForenameU);

            NpgsqlParameter paramForenameI = new NpgsqlParameter
            {
                SourceColumn = "forename",
                ParameterName = ":forename"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramForenameI);

            NpgsqlParameter paramPatronymicU = new NpgsqlParameter
            {
                SourceColumn = "patronymic",
                ParameterName = ":patronymic"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramPatronymicU);

            NpgsqlParameter paramPatronymicI = new NpgsqlParameter
            {
                SourceColumn = "patronymic",
                ParameterName = ":patronymic"
            };
            dataAdapter.InsertCommand.Parameters.Add(paramPatronymicI);

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

            dataAdapter.Update(dataSet, "instructor");
        }
    }
}
