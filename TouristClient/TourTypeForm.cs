using System;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class TourTypeForm : Form
    {
        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        public TourTypeForm()
        {
            InitializeComponent();
        }

        void ReloadTable()
        {
            touristDataSet = touristServiceExporter.ReadTourType();
            dataGridView_tourType.DataSource = touristDataSet;
            dataGridView_tourType.DataMember = "TourType";
            dataGridView_tourType.Columns["tour_type_name"].HeaderText = "Название";
            dataGridView_tourType.Columns["tour_type_name"].Width = 700;
            dataGridView_tourType.Columns["id"].Visible = false;
        }

        private void TourTypeForm_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            AddTourType addTourType = new AddTourType(touristDataSet.TourType, null);
            addTourType.ShowDialog();
            if (addTourType.DialogResult == DialogResult.OK)
            {
                touristServiceExporter.WriteTourType(touristDataSet);
                //Перезагрузка таблицы для подтягивания ID новой записи
                ReloadTable();
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            //Получение 1й выбранной строки и отправка соответствующей строки датасета в форму редактирования
            AddTourType addTourType = new AddTourType(touristDataSet.TourType, touristDataSet.TourType.Rows.Find(dataGridView_tourType.SelectedRows[0].Cells["id"].Value));
            addTourType.Text = "Редактирование типа туров";
            addTourType.ShowDialog();
            if (addTourType.DialogResult == DialogResult.OK)
            {
                touristDataSet = touristServiceExporter.WriteTourType(touristDataSet);
                ReloadTable();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить выбранную строку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                touristDataSet.TourType.Rows.Find(dataGridView_tourType.SelectedRows[0].Cells["id"].Value).Delete();
                touristDataSet = touristServiceExporter.WriteTourType(touristDataSet);
                ReloadTable();
            }
        }
    }
}
