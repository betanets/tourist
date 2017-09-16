using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class InstructorDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand("select * from instructor")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "instructor");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update instructor set surname=:surname, forename=:forename, patronymic=:patronymic, id_schedule=:id_schedule, id_tour_type=:id_tour_type where id=:id"),
                InsertCommand = new SqlCommand("insert into instructor (surname, forename, patronymic, id_schedule, id_tour_type) values (:surname, :forename, :patronymic, :id_schedule, :id_tour_type) where id=:id"),
                DeleteCommand = new SqlCommand("delete from instructor where id=:id")
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

            SqlParameter paramSurname = new SqlParameter
            {
                SourceColumn = "surname",
                ParameterName = ":surname"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramSurname);
            dataAdapter.InsertCommand.Parameters.Add(paramSurname);

            SqlParameter paramForename = new SqlParameter
            {
                SourceColumn = "forename",
                ParameterName = ":forename"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramForename);
            dataAdapter.InsertCommand.Parameters.Add(paramForename);

            SqlParameter paramPatronymic = new SqlParameter
            {
                SourceColumn = "patronymic",
                ParameterName = ":patronymic"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramPatronymic);
            dataAdapter.InsertCommand.Parameters.Add(paramPatronymic);

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

            dataAdapter.Update(dataSet, "instructor");
        }
    }
}
