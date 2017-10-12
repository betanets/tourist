using NUnit.Framework;
using System;

namespace Tourist.Tests
{
    class TourDataAccessorTests
    {
        [Test]
        public void TourReadEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Чтение в датасет и удаление оттуда всех записей
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            for (int i = 0; i < ds.Tour.Count; i++)
            {
                ds.Tour[i].Delete();
            }

            //Сохранение в БД
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();

            //Чтение в датасет из пустой таблицы
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Tour.Count;

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void TourReadNotEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Tour.Count;

            //Проверка, есть ли значения в таблицах расписания, экскурсии и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Sight.Count == 0)
            {
                ds.Sight.AddSightRow("Первое название экскурсии", "Первое описание экскурсии");
                sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Tour.Clear();
            ds.Sight.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedSightId = ds.Sight[0].id;
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Tour.AddTourRow("Тур городской", "Тур по городу", ds.Sight[0], ds.Schedule[0], ds.TourType[0]);
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();

            //Читаем снова и смотрим на последнюю строку
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Тур городской", ds.Tour[countElements].tour_name);
            Assert.AreEqual("Тур по городу", ds.Tour[countElements].tour_descr);
            Assert.AreEqual(savedSightId, ds.Tour[countElements].id_sight);
            Assert.AreEqual(savedScheduleId, ds.Tour[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Tour[countElements].id_tour_type);
        }

        [Test]
        public void TourDelete()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Tour.Count;

            //Проверка, есть ли значения в таблицах расписания, экскурсии и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Sight.Count == 0)
            {
                ds.Sight.AddSightRow("Первое название экскурсии", "Первое описание экскурсии");
                sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Tour.Clear();
            ds.Sight.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Добавляем строку и пишем в базу
            ds.Tour.AddTourRow("Тур для удаления", "Тур по удалению", ds.Sight[0], ds.Schedule[0], ds.TourType[0]);
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Tour.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            ds.Tour[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Tour.Count, countElements);
        }

        [Test]
        public void TourInsert()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Tour.Count;

            //Проверка, есть ли значения в таблицах расписания, экскурсии и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Sight.Count == 0)
            {
                ds.Sight.AddSightRow("Первое название экскурсии", "Первое описание экскурсии");
                sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Tour.Clear();
            ds.Sight.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedSightId = ds.Sight[0].id;
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Tour.AddTourRow("Тур для инсёрта", "Тур инсёртов", ds.Sight[0], ds.Schedule[0], ds.TourType[0]);
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();

            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual("Тур для инсёрта", ds.Tour[countElements].tour_name);
            Assert.AreEqual("Тур инсёртов", ds.Tour[countElements].tour_descr);
            Assert.AreEqual(savedSightId, ds.Tour[countElements].id_sight);
            Assert.AreEqual(savedScheduleId, ds.Tour[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Tour[countElements].id_tour_type);
        }

        [Test]
        public void TourUpdate()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            TourDataAccessor tourDataAccessor = new TourDataAccessor();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Tour.Count;

            //Проверка, есть ли значения в таблицах расписания, экскурсии и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Sight.Count == 0)
            {
                ds.Sight.AddSightRow("Первое название экскурсии", "Первое описание экскурсии");
                sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Tour.Clear();
            ds.Sight.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedSightId = ds.Sight[0].id;
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Tour.AddTourRow("Тур для апдейта", "Тур апдейтов", ds.Sight[0], ds.Schedule[0], ds.TourType[0]);
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();

            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            countElements = ds.Tour.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElements, 1);

            //Меняем поле и пишем в БД
            ds.Tour[countElements - 1].tour_name = "Hello from Update";
            tourDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Tour.Clear();

            //Читаем снова и сравниваем
            tourDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Hello from Update", ds.Tour[countElements - 1].tour_name);
            Assert.AreEqual("Тур апдейтов", ds.Tour[countElements - 1].tour_descr);
            Assert.AreEqual(savedSightId, ds.Tour[countElements - 1].id_sight);
            Assert.AreEqual(savedScheduleId, ds.Tour[countElements - 1].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Tour[countElements - 1].id_tour_type);
        }
    }
}
