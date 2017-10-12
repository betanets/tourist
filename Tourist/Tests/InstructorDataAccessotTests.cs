using NUnit.Framework;
using System;

namespace Tourist.Tests
{
    class InstructorDataAccessotTests
    {
        [Test]
        public void InstructorReadEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Чтение в датасет и удаление оттуда всех записей
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            for (int i = 0; i < ds.Instructor.Count; i++)
            {
                ds.Instructor[i].Delete();
            }

            //Сохранение в БД
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();

            //Чтение в датасет из пустой таблицы
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Instructor.Count;

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void InstructorReadNotEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if(ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
                
            }
            if(ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Петров", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();

            //Читаем снова и смотрим на последнюю строку
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Петров", ds.Instructor[countElements].surname);
            Assert.AreEqual("Алексей", ds.Instructor[countElements].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements].id_tour_type);
        }

        [Test]
        public void InstructorDelete()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
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
                ds.TourType.AddTourTypeRow("Первая строка списка туров от делита");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Делитов", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Instructor.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            ds.Instructor[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Instructor.Count, countElements);
        }

        [Test]
        public void InstructorInsert()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
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
                ds.TourType.AddTourTypeRow("Первая строка списка туров от инсёрта");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Инсёртов", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();

            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual("Инсёртов", ds.Instructor[countElements].surname);
            Assert.AreEqual("Алексей", ds.Instructor[countElements].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements].id_tour_type);
        }

        [Test]
        public void InstructorUpdate()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            TourTypeDataAccessor tourTypeDataAccessor = new TourTypeDataAccessor();
            InstructorDataAccessor instructorDataAccessor = new InstructorDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
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
                ds.TourType.AddTourTypeRow("Первая строка списка туров от апдейта");
                tourTypeDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            tourTypeDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Изменяйло", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();

            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            countElements = ds.Instructor.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElements, 1);

            //Меняем поле и пишем в БД
            ds.Instructor[countElements - 1].forename = "Hello from Update";
            instructorDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Instructor.Clear();

            //Читаем снова и сравниваем
            instructorDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Изменяйло", ds.Instructor[countElements - 1].surname);
            Assert.AreEqual("Hello from Update", ds.Instructor[countElements - 1].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements - 1].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements - 1].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements - 1].id_tour_type);
        }
    }
}
