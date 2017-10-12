using NUnit.Framework;
using System;

namespace Tourist.Tests
{
    class ScheduleDataAccessorTests
    {
        [Test]
        public void ScheduleReadEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Чтение в датасет и удаление оттуда всех записей
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            for (int i = 0; i < ds.Schedule.Count; i++)
            {
                ds.Schedule[i].Delete();
            }

            //Сохранение в БД
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Schedule.Clear();

            //Чтение в датасет из пустой таблицы
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Schedule.Count;

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void ScheduleReadNotEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Schedule.Count;

            //Добавляем строку и пишем в базу
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Schedule.Clear();

            //Читаем снова и смотрим на последнюю строку
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual(currentDateTime, ds.Schedule[countElements].tour_date);
        }

        [Test]
        public void ScheduleDelete()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД в датасет и смотрим число строк
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Schedule.Count;
            ds.Schedule.Clear();

            //Добавляем строку в датасет и записываем в БД
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Schedule.Clear();
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            ds.Schedule[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Schedule.Clear();
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Schedule.Count, countElements);
        }

        [Test]
        public void ScheduleInsert()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД, считаем число записей в датасете
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Schedule.Count;

            //Добавляем строку в датасет, сохраняем в БД, снова читаем в датасет
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Schedule.Clear();
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(currentDateTime, ds.Schedule[countElement].tour_date);
        }

        [Test]
        public void ScheduleUpdate()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            ScheduleDataAccessor scheduleDataAccessor = new ScheduleDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);

            //Читаем и проверяем, что в таблице хоть что-то есть
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Sight.Count;
            if (countElement == 0)
            {
                //Если ничего нет, добавляем 1 строку
                
                ds.Schedule.AddScheduleRow(currentDateTime);
                scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Читаем снова, пересчитываем число строк
            ds.Schedule.Clear();
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            countElement = ds.Schedule.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElement, 1);

            //Меняем поле и пишем в БД
            ds.Schedule[countElement - 1].tour_date = currentDateTime.AddDays(3);
            scheduleDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Schedule.Clear();

            //Читаем снова и сравниваем
            scheduleDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual(currentDateTime.AddDays(3), ds.Schedule[countElement - 1].tour_date);
        }
    }
}
