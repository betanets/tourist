using System;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class TourForm : Form
    {
        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        public TourForm()
        {
            InitializeComponent();
        }

        void ReloadTable()
        {
            //Очистка колонок нужна для удаления кастомных колонок: "Дата тура" и "Название тура"
            //После переустановки DataSource остальные колонки подтянутся автоматически, а кастомные - заново сгенерируются
            dataGridView_tour.Columns.Clear();

            touristDataSet = touristServiceExporter.ReadTour();
            dataGridView_tour.DataSource = touristDataSet;
            dataGridView_tour.DataMember = "Tour";
            dataGridView_tour.Columns.Add("sight_name", "Главная достопримечательность");
            dataGridView_tour.Columns.Add("tour_type_name", "Тип тура");
            dataGridView_tour.Columns.Add("tour_date", "Дата тура");
            foreach (DataGridViewRow row in dataGridView_tour.Rows)
            {
                row.Cells["sight_name"].Value = touristDataSet.Sight.Rows.Find(row.Cells["id_sight"].Value)["sight_name"];
                row.Cells["tour_type_name"].Value = touristDataSet.TourType.Rows.Find(row.Cells["id_tour_type"].Value)["tour_type_name"];
                row.Cells["tour_date"].Value = touristDataSet.Schedule.Rows.Find(row.Cells["id_schedule"].Value)["tour_date"];
            }
            dataGridView_tour.Columns["tour_name"].HeaderText = "Название тура";
            dataGridView_tour.Columns["tour_name"].Width = 150;
            dataGridView_tour.Columns["tour_descr"].HeaderText = "Описание тура";
            dataGridView_tour.Columns["tour_descr"].Width = 200;
            dataGridView_tour.Columns["sight_name"].Width = 200;

            dataGridView_tour.Columns["id"].Visible = false;
            dataGridView_tour.Columns["id_sight"].Visible = false;
            dataGridView_tour.Columns["id_schedule"].Visible = false;
            dataGridView_tour.Columns["id_tour_type"].Visible = false;
        }

        private void TourForm_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            AddTour addTour = new AddTour(touristDataSet.Tour, touristDataSet.Sight, touristDataSet.TourType, touristDataSet.Schedule, null);
            addTour.ShowDialog();
            if (addTour.DialogResult == DialogResult.OK)
            {
                touristServiceExporter.WriteTour(touristDataSet);
                //Перезагрузка таблицы для подтягивания ID новой записи
                ReloadTable();
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            //Получение 1й выбранной строки и отправка соответствующей строки датасета в форму редактирования
            AddTour addTour = new AddTour(touristDataSet.Tour, touristDataSet.Sight, touristDataSet.TourType,
                touristDataSet.Schedule, touristDataSet.Tour.Rows.Find(dataGridView_tour.SelectedRows[0].Cells["id"].Value));
            addTour.Text = "Редактирование тура";
            addTour.ShowDialog();
            if (addTour.DialogResult == DialogResult.OK)
            {
                touristDataSet = touristServiceExporter.WriteTour(touristDataSet);
                ReloadTable();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            try
            {
                if (result == DialogResult.Yes)
                {
                    touristDataSet.Tour.Rows.Find(dataGridView_tour.SelectedRows[0].Cells["id"].Value).Delete();
                    touristDataSet = touristServiceExporter.WriteTour(touristDataSet);
                    ReloadTable();
                }
            }
            catch (SoapException)
            {
                ReloadTable();
                MessageBox.Show("Не удалось удалить выбранную строку.\nСкорее всего, на данную строку имеются ссылки из других таблиц", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
