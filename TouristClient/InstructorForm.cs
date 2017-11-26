using System;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class InstructorForm : Form
    {
        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        public InstructorForm()
        {
            InitializeComponent();
        }

        void ReloadTable()
        {
            //Очистка колонок нужна для удаления кастомных колонок: "Дата тура" и "Название тура"
            //После переустановки DataSource остальные колонки подтянутся автоматически, а кастомные - заново сгенерируются
            dataGridView_instructor.Columns.Clear();

            touristDataSet = touristServiceExporter.ReadInstructor();
            dataGridView_instructor.DataSource = touristDataSet;
            dataGridView_instructor.DataMember = "Instructor";
            dataGridView_instructor.Columns.Add("tour_type_name", "Тип тура");
            dataGridView_instructor.Columns.Add("tour_date", "Дата выхода");
            foreach (DataGridViewRow row in dataGridView_instructor.Rows)
            {
                row.Cells["tour_type_name"].Value = touristDataSet.TourType.Rows.Find(row.Cells["id_tour_type"].Value)["tour_type_name"];
                row.Cells["tour_date"].Value = touristDataSet.Schedule.Rows.Find(row.Cells["id_schedule"].Value)["tour_date"];
            }
            dataGridView_instructor.Columns["surname"].HeaderText = "Фамилия";
            dataGridView_instructor.Columns["surname"].Width = 150;
            dataGridView_instructor.Columns["forename"].HeaderText = "Имя";
            dataGridView_instructor.Columns["forename"].Width = 150;
            dataGridView_instructor.Columns["patronymic"].HeaderText = "Отчество";
            dataGridView_instructor.Columns["patronymic"].Width = 150;
            dataGridView_instructor.Columns["id"].Visible = false;
            dataGridView_instructor.Columns["id_schedule"].Visible = false;
            dataGridView_instructor.Columns["id_tour_type"].Visible = false;
        }

        private void InstructorForm_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            AddInstructor addInstructor = new AddInstructor(touristDataSet.Instructor, touristDataSet.TourType, touristDataSet.Schedule, null);
            addInstructor.ShowDialog();
            if (addInstructor.DialogResult == DialogResult.OK)
            {
                touristServiceExporter.WriteInstructor(touristDataSet);
                //Перезагрузка таблицы для подтягивания ID новой записи
                ReloadTable();
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            //Получение 1й выбранной строки и отправка соответствующей строки датасета в форму редактирования
            AddInstructor addInstructor = new AddInstructor(touristDataSet.Instructor, touristDataSet.TourType, 
                touristDataSet.Schedule, touristDataSet.Instructor.Rows.Find(dataGridView_instructor.SelectedRows[0].Cells["id"].Value));
            addInstructor.Text = "Редактирование инструктора";
            addInstructor.ShowDialog();
            if (addInstructor.DialogResult == DialogResult.OK)
            {
                touristDataSet = touristServiceExporter.WriteInstructor(touristDataSet);
                ReloadTable();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    touristDataSet.Instructor.Rows.Find(dataGridView_instructor.SelectedRows[0].Cells["id"].Value).Delete();
                    touristDataSet = touristServiceExporter.WriteInstructor(touristDataSet);
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
