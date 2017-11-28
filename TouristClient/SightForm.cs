using System;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class SightForm : Form
    {
        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        public SightForm()
        {
            InitializeComponent();
        }

        void ReloadTable()
        {
            touristDataSet = touristServiceExporter.ReadSight();
            dataGridView_sight.DataSource = touristDataSet;
            dataGridView_sight.DataMember = "Sight";
            dataGridView_sight.Columns["sight_name"].HeaderText = "Название";
            dataGridView_sight.Columns["sight_name"].Width = 300;
            dataGridView_sight.Columns["sight_descr"].HeaderText = "Описание";
            dataGridView_sight.Columns["sight_descr"].Width = 400;
            dataGridView_sight.Columns["id"].Visible = false;
        }

        private void SightForm_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            AddSight addSight = new AddSight(touristDataSet.Sight, null);
            addSight.ShowDialog();
            if(addSight.DialogResult == DialogResult.OK)
            {
                touristServiceExporter.WriteSight(touristDataSet);
                //Перезагрузка таблицы для подтягивания ID новой записи
                ReloadTable();
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (dataGridView_sight.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Не выбрана ни одна строка для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Получение 1й выбранной строки и отправка соответствующей строки датасета в форму редактирования
            AddSight addSight = new AddSight(touristDataSet.Sight, touristDataSet.Sight.Rows.Find(dataGridView_sight.SelectedRows[0].Cells["id"].Value));
            addSight.Text = "Редактирование достопримечательности";
            addSight.ShowDialog();
            if (addSight.DialogResult == DialogResult.OK)
            {
                touristDataSet = touristServiceExporter.WriteSight(touristDataSet);
                ReloadTable();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if(dataGridView_sight.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Не выбрана ни одна строка для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                try
                {
                    touristDataSet.Sight.Rows.Find(dataGridView_sight.SelectedRows[0].Cells["id"].Value).Delete();
                    touristDataSet = touristServiceExporter.WriteSight(touristDataSet);
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
