using NUnit.Framework;

namespace Tourist.Tests.BusinessLogic
{
    class SightBusinessLogicTests
    {
        [Test]
        public void BLSightReadEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Чтение в датасет и удаление оттуда всех записей
            ds = bl.ReadSight();

            for (int i = 0; i < ds.Tour.Count; i++)
            {
                ds.Tour[i].Delete();
            }
            for (int i = 0; i < ds.Sight.Count; i++)
            {
                ds.Sight[i].Delete();
            }

            //Сохранение в БД
            ds = bl.WriteSight(ds);

            //Очистка таблиц
            ds.Tour.Clear();
            ds.Sight.Clear();

            //Чтение в датасет из пустой таблицы
            ds = bl.ReadSight();
            int countElement = ds.Sight.Count;

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void BLSightReadNotEmptyTable()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем строки с БД и смотрим их число
            ds = bl.ReadSight();

            int countElements = ds.Sight.Count;

            //Добавляем строку и пишем в базу
            ds.Sight.AddSightRow("Памятник Ленину", "Обычный памятник в городе");
            ds = bl.WriteSight(ds);
            ds.Sight.Clear();

            //Читаем снова и смотрим на последнюю строку
            ds = bl.ReadSight();

            Assert.AreEqual("Памятник Ленину", ds.Sight[countElements].sight_name);
            Assert.AreEqual("Обычный памятник в городе", ds.Sight[countElements].sight_descr);
        }

        [Test]
        public void BLSightDelete()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем данные с БД в датасет и смотрим число строк
            ds = bl.ReadSight();
            int countElements = ds.Sight.Count;
            ds.Sight.Clear();

            //Добавляем строку в датасет и записываем в БД
            ds.Sight.AddSightRow("String", "ToDelete");
            ds = bl.WriteSight(ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Sight.Clear();
            ds = bl.ReadSight();
            ds.Sight[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            ds = bl.WriteSight(ds);
            ds.Sight.Clear();
            ds = bl.ReadSight();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Sight.Count, countElements);
        }

        [Test]
        public void BLSightInsert()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем данные с БД, считаем число записей в датасете
            ds = bl.ReadSight();
            int countElement = ds.Sight.Count;

            //Добавляем строку в датасет, сохраняем в БД, снова читаем в датасет
            ds.Sight.AddSightRow("Insert", "Str.");
            ds = bl.WriteSight(ds);
            ds.Sight.Clear();
            ds = bl.ReadSight();

            Assert.AreEqual("Insert", ds.Sight[countElement].sight_name);
            Assert.AreEqual("Str.", ds.Sight[countElement].sight_descr);
        }

        [Test]
        public void BLSightUpdate()
        {
            Tourist.BusinessLogic.BusinessLogic bl = new Tourist.BusinessLogic.BusinessLogic();
            TouristDataSet ds = new TouristDataSet();

            //Читаем и проверяем, что в таблице хоть что-то есть
            ds = bl.ReadSight();
            int countElement = ds.Sight.Count;
            if (countElement == 0)
            {
                //Если ничего нет, добавляем 1 строку
                ds.Sight.AddSightRow("Insert", "Str.");
                ds = bl.WriteSight(ds);
            }
            //Читаем снова, пересчитываем число строк
            ds.Sight.Clear();
            ds = bl.ReadSight();
            countElement = ds.Sight.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(countElement, 1);

            //Меняем поле и пишем в БД
            ds.Sight[countElement - 1].sight_descr = "Hello from Update";
            ds = bl.WriteSight(ds);
            ds.Sight.Clear();

            //Читаем снова и сравниваем
            ds = bl.ReadSight();

            Assert.AreEqual("Hello from Update", ds.Sight[countElement - 1].sight_descr);
        }
    }
}
