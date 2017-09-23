using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist
{
    class Init
    {
        private static SightDataAccessor sightDataAccessor = new SightDataAccessor();
        public static void Main()
        {
            TouristDataSet touristDataSet = new TouristDataSet();
            AbstractConnection connection = ConnectionFactory.CreateConection();
            connection.Open();
            try
            {
                AbstractTransaction transaction = connection.BeginTransaction();
                try
                {
                    sightDataAccessor.ReadData(transaction, connection, touristDataSet);
                    //sightDataAccessor.Update(connection, transaction, result);

                    //docAccessor.Update(connection, transaction, result);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            } finally {
                connection.Close();
            }
            Console.WriteLine(touristDataSet.Sight.Count);
            Console.ReadKey();
            return;
        }
    }
}
