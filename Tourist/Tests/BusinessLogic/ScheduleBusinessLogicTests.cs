using NUnit.Framework;
using System;

namespace Tourist.Tests.BusinessLogic
{
    class ScheduleBusinessLogicTests
    {
        [Test]
        public void BLScheduleReadEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Чтение в датасет и удаление оттуда всех записей
            ds = bl.ReadSchedule();
            for (int i = 0; i < ds.Instructor.Count; i++)
            {
                ds.Instructor[i].Delete();
            }
            for (int i = 0; i < ds.Tour.Count; i++)
            {
                ds.Tour[i].Delete();
            }
            for (int i = 0; i < ds.Schedule.Count; i++)
            {
                ds.Schedule[i].Delete();
            }

            //Сохранение в БД
            ds = bl.WriteSchedule(ds);

            ds.Instructor.Clear();
            ds.Tour.Clear();
            ds.Schedule.Clear();

            //Чтение в датасет из пустой таблицы
            ds = bl.ReadSchedule();
            int countElement = ds.Schedule.Count;

            Assert.AreEqual(0, countElement);
        }
        
        [Test]
        public void BLScheduleReadNotEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadSchedule();
            int countElements = ds.Schedule.Count;

            //Добавляем строку и пишем в базу
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            ds = bl.WriteSchedule(ds);
            ds.Schedule.Clear();

            //Читаем снова и смотрим на последнюю строку
            ds = bl.ReadSchedule();

            Assert.AreEqual(currentDateTime, ds.Schedule[countElements].tour_date);
        }
        
        [Test]
        public void BLScheduleDelete()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем данные с БД в датасет и смотрим число строк
            ds = bl.ReadSchedule();
            int countElements = ds.Schedule.Count;
            ds.Schedule.Clear();

            //Добавляем строку в датасет и записываем в БД
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            ds = bl.WriteSchedule(ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Schedule.Clear();
            ds = bl.ReadSchedule();
            ds.Schedule[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            ds = bl.WriteSchedule(ds);
            ds.Schedule.Clear();
            ds = bl.ReadSchedule();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Schedule.Count, countElements);
        }
        
        [Test]
        public void BLScheduleInsert()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем данные с БД, считаем число записей в датасете
            ds = bl.ReadSchedule();
            int countElement = ds.Schedule.Count;

            //Добавляем строку в датасет, сохраняем в БД, снова читаем в датасет
            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            ds.Schedule.AddScheduleRow(currentDateTime);
            ds = bl.WriteSchedule(ds);
            ds.Schedule.Clear();
            ds = bl.ReadSchedule();

            Assert.AreEqual(currentDateTime, ds.Schedule[countElement].tour_date);
        }

        
        [Test]
        public void BLScheduleUpdate()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            DateTime currentDateTime = DateTime.Now;
            currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);

            //Читаем и проверяем, что в таблице хоть что-то есть
            ds = bl.ReadSchedule();
            int countElement = ds.Sight.Count;
            if (countElement == 0)
            {
                //Если ничего нет, добавляем 1 строку
                ds.Schedule.AddScheduleRow(currentDateTime);
                ds = bl.WriteSchedule(ds);
            }
            //Читаем снова, пересчитываем число строк
            ds.Schedule.Clear();
            ds = bl.ReadSchedule();
            countElement = ds.Schedule.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElement, 1);

            //Меняем поле и пишем в БД
            ds.Schedule[countElement - 1].tour_date = currentDateTime.AddDays(3);
            ds = bl.WriteSchedule(ds);
            ds.Schedule.Clear();

            //Читаем снова и сравниваем
            ds = bl.ReadSchedule();

            Assert.AreEqual(currentDateTime.AddDays(3), ds.Schedule[countElement - 1].tour_date);
        }
    }
}
