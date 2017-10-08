using NUnit.Framework;

namespace Tourist
{
    class NUnitTests
    {
        [Test]
        public void SightReadEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Чтение в датасет и удаление оттуда всех записей
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            for (int i = 0; i < ds.Sight.Count; i++)
            {
                ds.Sight[i].Delete();
            }

            //Сохранение в БД
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Sight.Clear();

            //Чтение в датасет из пустой таблицы
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Sight.Count;

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual(0, countElement);
        }

        [Test]
        public void SightReadNotEmptyTable()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем строки с БД и смотрим их число
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Sight.Count;

            //Добавляем строку и пишем в базу
            ds.Sight.AddSightRow("Памятник Ленину", "Обычный памятник в городе");
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Sight.Clear();

            //Читаем снова и смотрим на последнюю строку
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Памятник Ленину", ds.Sight[countElements].sight_name);
            Assert.AreEqual("Обычный памятник в городе", ds.Sight[countElements].sight_descr);
        }

        [Test]
        public void SightDelete()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД в датасет и смотрим число строк
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElements = ds.Sight.Count;
            ds.Sight.Clear();

            //Добавляем строку в датасет и записываем в БД
            ds.Sight.AddSightRow("String", "ToDelete");
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);

            //Чистим датасет, записываем в него ещё раз и удаляем из него последнюю запись
            ds.Sight.Clear();
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            ds.Sight[countElements].Delete();

            //Пишем в БД и снова читаем из неё в датасет
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Sight.Clear();
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            //Смотрим число строк до всех манипуляций и после
            Assert.AreEqual(ds.Sight.Count, countElements);
        }

        [Test]
        public void SightInsert()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем данные с БД, считаем число записей в датасете
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Sight.Count;

            //Добавляем строку в датасет, сохраняем в БД, снова читаем в датасет
            ds.Sight.AddSightRow("Insert", "Str.");
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Sight.Clear();
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();

            Assert.AreEqual("Insert", ds.Sight[countElement].sight_name);
            Assert.AreEqual("Str.", ds.Sight[countElement].sight_descr);
        }

        [Test]
        public void SightUpdate()
        {
            AbstractConnection abstractConnection = ConnectionFactory.CreateConnection();
            abstractConnection.Open();
            TouristDataSet ds = new TouristDataSet();
            SightDataAccessor sightDataAccessor = new SightDataAccessor();
            AbstractTransaction abstractTransaction = abstractConnection.BeginTransaction();

            //Читаем и проверяем, что в таблице хоть что-то есть
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            int countElement = ds.Sight.Count;
            if(countElement == 0)
            {
                //Если ничего нет, добавляем 1 строку
                ds.Sight.AddSightRow("Insert", "Str.");
                sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            }
            //Читаем снова, пересчитываем число строк
            ds.Sight.Clear();
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);
            countElement = ds.Sight.Count;

            //Проверяем, что число строк >= 1
            Assert.GreaterOrEqual(1, countElement);

            //Меняем поле и пишем в БД
            ds.Sight[countElement - 1].sight_descr = "Hello from Update";
            sightDataAccessor.WriteData(abstractTransaction, abstractConnection, ds);
            ds.Sight.Clear();

            //Читаем снова и сравниваем
            sightDataAccessor.ReadData(abstractTransaction, abstractConnection, ds);

            abstractTransaction.Commit();
            abstractConnection.Close();
            Assert.AreEqual("Hello from Update", ds.Sight[countElement - 1].sight_descr);
        }
    }
}
