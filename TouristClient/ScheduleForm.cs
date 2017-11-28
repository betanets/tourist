using System;
using System.Windows.Forms;
using System.Web.Services.Protocols;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class ScheduleForm : Form
    {
        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        public ScheduleForm()
        {
            InitializeComponent();
        }

        void ReloadTable()
        {
            touristDataSet = touristServiceExporter.ReadSchedule();
            dataGridView_schedule.DataSource = touristDataSet;
            dataGridView_schedule.DataMember = "Schedule";
            dataGridView_schedule.Columns["tour_date"].HeaderText = "Дата";
            dataGridView_schedule.Columns["tour_date"].Width = 700;
            dataGridView_schedule.Columns["id"].Visible = false;
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            AddSchedule addSchedule = new AddSchedule(touristDataSet.Schedule, null);
            addSchedule.ShowDialog();
            if (addSchedule.DialogResult == DialogResult.OK)
            {
                touristServiceExporter.WriteSchedule(touristDataSet);
                //Перезагрузка таблицы для подтягивания ID новой записи
                ReloadTable();
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (dataGridView_schedule.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Не выбрана ни одна строка для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Получение 1й выбранной строки и отправка соответствующей строки датасета в форму редактирования
            AddSchedule addSchedule = new AddSchedule(touristDataSet.Schedule, touristDataSet.Schedule.Rows.Find(dataGridView_schedule.SelectedRows[0].Cells["id"].Value));
            addSchedule.Text = "Редактирование даты тура";
            addSchedule.ShowDialog();
            if (addSchedule.DialogResult == DialogResult.OK)
            {
                touristDataSet = touristServiceExporter.WriteSchedule(touristDataSet);
                ReloadTable();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView_schedule.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Не выбрана ни одна строка для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    touristDataSet.Schedule.Rows.Find(dataGridView_schedule.SelectedRows[0].Cells["id"].Value).Delete();
                    touristDataSet = touristServiceExporter.WriteSchedule(touristDataSet);
                    ReloadTable();
                }
                catch (SoapException)
                {
                    ReloadTable();
                    MessageBox.Show("Не удалось удалить выбранную строку.\nСкорее всего, на данную строку имеются ссылки из других таблиц", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
