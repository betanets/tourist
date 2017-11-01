using NUnit.Framework;
using System;

namespace Tourist.Tests.BusinessLogic
{
    class InstructorBusinessLogicTests
    {
        [Test]
        public void BLInstructorReadEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Чтение в датасет и удаление оттуда всех записей
            ds = bl.ReadInstructor();
            for (int i = 0; i < ds.Instructor.Count; i++)
            {
                ds.Instructor[i].Delete();
            }

            //Сохранение в БД
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();

            //Чтение в датасет из пустой таблицы
            ds = bl.ReadInstructor();
            int countElement = ds.Instructor.Count;

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void BLInstructorReadNotEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadInstructor();
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if(ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                ds = bl.WriteSchedule(ds);
            }
            if(ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров");
                ds = bl.WriteTourType(ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            ds = bl.ReadInstructor();

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Петров", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();

            //Читаем снова и смотрим на последнюю строку
            ds = bl.ReadInstructor();

            Assert.AreEqual("Петров", ds.Instructor[countElements].surname);
            Assert.AreEqual("Алексей", ds.Instructor[countElements].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements].id_tour_type);
        }

        [Test]
        public void BLInstructorDelete()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadInstructor();
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                ds = bl.WriteSchedule(ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров от делита");
                ds = bl.WriteTourType(ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            ds = bl.ReadInstructor();

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Делитов", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            ds = bl.WriteInstructor(ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Instructor.Clear();
            ds = bl.ReadInstructor();
            ds.Instructor[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();
            ds = bl.ReadInstructor();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Instructor.Count, countElements);
        }

        
        [Test]
        public void BLInstructorInsert()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadInstructor();
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                ds = bl.WriteSchedule(ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров от инсёрта");
                ds = bl.WriteTourType(ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            ds = bl.ReadInstructor();

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Инсёртов", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();

            ds = bl.ReadInstructor();

            Assert.AreEqual("Инсёртов", ds.Instructor[countElements].surname);
            Assert.AreEqual("Алексей", ds.Instructor[countElements].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements].id_tour_type);
        }

        
        [Test]
        public void BLInstructorUpdate()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadInstructor();
            int countElements = ds.Instructor.Count;

            //Проверка, есть ли значения в таблицах расписания и типа экскурсии. Если значения нет, записываем его и считываем еще раз
            if (ds.Schedule.Count == 0)
            {
                DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                ds.Schedule.AddScheduleRow(currentDateTime);
                ds = bl.WriteSchedule(ds);
            }
            if (ds.TourType.Count == 0)
            {
                ds.TourType.AddTourTypeRow("Первая строка списка туров от апдейта");
                ds = bl.WriteTourType(ds);
            }
            //Чистим таблицы датасета и читаем заново данные с базы
            ds.Instructor.Clear();
            ds.Schedule.Clear();
            ds.TourType.Clear();
            ds = bl.ReadInstructor();

            //Сохраняем ID первой записи таблиц
            Int32 savedScheduleId = ds.Schedule[0].id;
            Int32 savedTourTypeId = ds.TourType[0].id;

            //Добавляем строку и пишем в базу
            ds.Instructor.AddInstructorRow("Изменяйло", "Алексей", "Петрович", ds.Schedule[0], ds.TourType[0]);
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();

            ds = bl.ReadInstructor();
            countElements = ds.Instructor.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElements, 1);

            //Меняем поле и пишем в БД
            ds.Instructor[countElements - 1].forename = "Hello from Update";
            ds = bl.WriteInstructor(ds);
            ds.Instructor.Clear();

            //Читаем снова и сравниваем
            ds = bl.ReadInstructor();

            Assert.AreEqual("Изменяйло", ds.Instructor[countElements - 1].surname);
            Assert.AreEqual("Hello from Update", ds.Instructor[countElements - 1].forename);
            Assert.AreEqual("Петрович", ds.Instructor[countElements - 1].patronymic);
            Assert.AreEqual(savedScheduleId, ds.Instructor[countElements - 1].id_schedule);
            Assert.AreEqual(savedTourTypeId, ds.Instructor[countElements - 1].id_tour_type);
        }
    }
}
