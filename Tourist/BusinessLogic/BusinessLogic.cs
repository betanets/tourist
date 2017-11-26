using System;

namespace Tourist.BusinessLogic
{
    class BusinessLogic : IBusinessLogic
    {
        private SightDataAccessor sightDataAccessor;
        private TourDataAccessor tourDataAccessor;
        private InstructorDataAccessor instructorDataAccessor;
        private TourTypeDataAccessor tourTypeDataAccessor;
        private ScheduleDataAccessor scheduleDataAccessor;

        public BusinessLogic()
        {
            sightDataAccessor = new SightDataAccessor();
            tourDataAccessor = new TourDataAccessor();
            instructorDataAccessor = new InstructorDataAccessor();
            tourTypeDataAccessor = new TourTypeDataAccessor();
            scheduleDataAccessor = new ScheduleDataAccessor();
        }

        //Конструкторы для DataAccessor'ов
        public SightDataAccessor SightDataAccessor
        {
            set
            {
                sightDataAccessor = value;
            }
        }
        public TourDataAccessor TourDataAccessor
        {
            set
            {
                tourDataAccessor = value;
            }
        }
        public InstructorDataAccessor InstructorDataAccessor
        {
            set
            {
                instructorDataAccessor = value;
            }
        }
        public TourTypeDataAccessor TourTypeDataAccessor
        {
            set
            {
                tourTypeDataAccessor = value;
            }
        }
        public ScheduleDataAccessor ScheduleDataAccessor
        {
            set
            {
                scheduleDataAccessor = value;
            }
        }

        /**
         * Методы для класса Sight
         */
        public TouristDataSet ReadSight()
        {
            TouristDataSet dataSet = new TouristDataSet();
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    tourDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    sightDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
        public TouristDataSet WriteSight(TouristDataSet dataSet)
        {
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    tourDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    sightDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }

        /**
         * Методы для класса Tour
         */
        public TouristDataSet ReadTour()
        {
            TouristDataSet dataSet = new TouristDataSet();
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    sightDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
        public TouristDataSet WriteTour(TouristDataSet dataSet)
        {
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    sightDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }

        /**
         * Методы для класса Instructor
         */
        public TouristDataSet ReadInstructor()
        {
            TouristDataSet dataSet = new TouristDataSet();
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
        public TouristDataSet WriteInstructor(TouristDataSet dataSet)
        {
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }

        /**
         * Методы для класса TourType
         */
        public TouristDataSet ReadTourType()
        {
            TouristDataSet dataSet = new TouristDataSet();
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
        public TouristDataSet WriteTourType(TouristDataSet dataSet)
        {
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }

        /**
         * Методы для класса Schedule
         */
        public TouristDataSet ReadSchedule()
        {
            TouristDataSet dataSet = new TouristDataSet();
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
        public TouristDataSet WriteSchedule(TouristDataSet dataSet)
        {
            using (AbstractConnection abstractConnection = ConnectionFactory.CreateConnection())
            {
                abstractConnection.Open();
                AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();
                try
                {
                    instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    tourDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, dataSet);
                    abstractTransaction.Commit();
                }
                catch (Exception e)
                {
                    abstractTransaction.Rollback();
                    throw e;
                }
            }
            return dataSet;
        }
    }
}
