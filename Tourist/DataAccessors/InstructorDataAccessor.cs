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
                InsertCommand = new NpgsqlCommand("insert into instructor (surname, forename, patronymic, id_schedule, id_tour_type) values (:surname, :forename, :patronymic, :id_schedule, :id_tour_type) where id=:id"),
                DeleteCommand = new NpgsqlCommand("delete from instructor where id=:id")
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

            NpgsqlParameter paramSurname = new NpgsqlParameter
            {
                SourceColumn = "surname",
                ParameterName = ":surname"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramSurname);
            dataAdapter.InsertCommand.Parameters.Add(paramSurname);

            NpgsqlParameter paramForename = new NpgsqlParameter
            {
                SourceColumn = "forename",
                ParameterName = ":forename"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramForename);
            dataAdapter.InsertCommand.Parameters.Add(paramForename);

            NpgsqlParameter paramPatronymic = new NpgsqlParameter
            {
                SourceColumn = "patronymic",
                ParameterName = ":patronymic"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramPatronymic);
            dataAdapter.InsertCommand.Parameters.Add(paramPatronymic);

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

            dataAdapter.Update(dataSet, "instructor");
        }
    }
}
