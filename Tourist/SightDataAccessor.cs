﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class SightDataAccessor
    {
        public void ReadData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand("select * from sight")
            };
            dataAdapter.SelectCommand.Connection = aConnection.connection;
            dataAdapter.SelectCommand.Transaction = aTransaction.transaction;
            dataAdapter.Fill(dataSet, "sight");
        }

        public void WriteData(AbstractTransaction aTransaction, AbstractConnection aConnection, TouristDataSet dataSet)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("update sight set sight_name=:name, sight_descr=:descr where id=:id"),
                InsertCommand = new SqlCommand("insert into sight (sight_name, sight_descr) values (:name, :descr) where id=:id"),
                DeleteCommand = new SqlCommand("delete from sight where id=:id")
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
                SourceColumn = "sight_name",
                ParameterName = ":name"
            };
            dataAdapter.UpdateCommand.Parameters.Add(paramName);
            dataAdapter.InsertCommand.Parameters.Add(paramName);
            
            SqlParameter paramDescr = new SqlParameter
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
