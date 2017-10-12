using NUnit.Framework;

namespace Tourist.Tests
{
    class TourTypeDataAccessorTests
    {
        [Test]
        public void TourTypeReadEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Чтение в датасет и удаление оттуда всех записей
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            for (int i = 0; i < ds.Instructor.Count; i++)
            {
                ds.Instructor[i].Delete();
            }
            for (int i = 0; i < ds.Tour.Count; i++)
            {
                ds.Tour[i].Delete();
            }
            for (int i = 0; i < ds.TourType.Count; i++)
            {
                ds.TourType[i].Delete();
            }

            //Сохранение в БД
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            ds.Instructor.Clear();
            ds.Tour.Clear();
            ds.TourType.Clear();

            //Чтение в датасет из пустой таблицы
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.TourType.Count;

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void TourTypeReadNotEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.TourType.Count;

            //Добавляем строку и пишем в базу
            ds.TourType.AddTourTypeRow("Поход в горы");
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.TourType.Clear();

            //Читаем снова и смотрим на последнюю строку
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Поход в горы", ds.TourType[countElements].tour_type_name);
        }

        [Test]
        public void TourTypeDelete()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД в датасет и смотрим число строк
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.TourType.Count;
            ds.TourType.Clear();

            //Добавляем строку в датасет и записываем в БД
            ds.TourType.AddTourTypeRow("DelMe");
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.TourType.Clear();
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            ds.TourType[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.TourType.Clear();
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.TourType.Count, countElements);
        }

        [Test]
        public void TourTypeInsert()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД, считаем число записей в датасете
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.TourType.Count;

            //Добавляем строку в датасет, сохраняем в БД, снова читаем в датасет
            ds.TourType.AddTourTypeRow("InsertStr");
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.TourType.Clear();
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual("InsertStr", ds.TourType[countElement].tour_type_name);
        }

        [Test]
        public void TourTypeUpdate()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем и проверяем, что в таблице хоть что-то есть
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.TourType.Count;
            if (countElement == 0)
            {
                //Если ничего нет, добавляем 1 строку
                ds.TourType.AddTourTypeRow("InsertAgain");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Читаем снова, пересчитываем число строк
            ds.TourType.Clear();
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            countElement = ds.TourType.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElement, 1);

            //Меняем поле и пишем в БД
            ds.TourType[countElement - 1].tour_type_name = "Hello from Update TT";
            tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.TourType.Clear();

            //Читаем снова и сравниваем
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Hello from Update TT", ds.TourType[countElement - 1].tour_type_name);
        }
    }
}
