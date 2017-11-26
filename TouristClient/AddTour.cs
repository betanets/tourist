using System;
using System.Data;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class AddTour : Form
    {
        private TouristDataSet.TourDataTable tourDataTable;
        private TouristDataSet.SightDataTable sightDataTable;
        private TouristDataSet.TourTypeDataTable tourTypeDataTable;
        private TouristDataSet.ScheduleDataTable scheduleDataTable;
        private DataRow tourSelectedRow;

        public AddTour(TouristDataSet.TourDataTable tourTable, TouristDataSet.SightDataTable sightTable,
            TouristDataSet.TourTypeDataTable tourTypeTable, TouristDataSet.ScheduleDataTable scheduleTable, DataRow row)
        {
            InitializeComponent();
            this.tourDataTable = tourTable;
            this.sightDataTable = sightTable;
            this.tourTypeDataTable = tourTypeTable;
            this.scheduleDataTable = scheduleTable;
            this.tourSelectedRow = row;
        }

        private void AddTour_Load(object sender, EventArgs e)
        {
            this.comboBox_sight.DataSource = sightDataTable;
            this.comboBox_sight.DisplayMember = "sight_name";
            this.comboBox_sight.ValueMember = "id";

            this.comboBox_tourType.DataSource = tourTypeDataTable;
            this.comboBox_tourType.DisplayMember = "tour_type_name";
            this.comboBox_tourType.ValueMember = "id";

            this.comboBox_tourDate.DataSource = scheduleDataTable;
            this.comboBox_tourDate.DisplayMember = "tour_date";
            this.comboBox_tourDate.ValueMember = "id";

            if (tourSelectedRow != null)
            {
                this.textBox_name.Text = tourSelectedRow["tour_name"].ToString();
                this.textBox_description.Text = tourSelectedRow["tour_descr"].ToString();
                this.comboBox_sight.SelectedValue = tourSelectedRow["id_sight"];
                this.comboBox_tourType.SelectedValue = tourSelectedRow["id_tour_type"];
                this.comboBox_tourDate.SelectedValue = tourSelectedRow["id_schedule"];
            }
            else
            {
                //Убираем выбранные значения с комбобоксов при добавлении
                this.comboBox_tourType.SelectedIndex = -1;
                this.comboBox_tourDate.SelectedIndex = -1;
                this.comboBox_sight.SelectedIndex = -1;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (this.comboBox_sight.SelectedValue == null || Convert.ToInt32(this.comboBox_sight.SelectedValue) < 0)
            {
                MessageBox.Show("Необходимо выбрать главную достопримечательность", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.comboBox_tourType.SelectedValue == null || Convert.ToInt32(this.comboBox_tourType.SelectedValue) < 0)
            {
                MessageBox.Show("Необходимо выбрать тип тура", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.comboBox_tourDate.SelectedValue == null || Convert.ToInt32(this.comboBox_tourDate.SelectedValue) < 0)
            {
                MessageBox.Show("Необходимо выбрать дату тура", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Редактирование
            if (tourSelectedRow != null)
            {
                tourSelectedRow["tour_name"] = this.textBox_name.Text;
                tourSelectedRow["tour_descr"] = this.textBox_description.Text;
                tourSelectedRow["id_sight"] = this.comboBox_sight.SelectedValue;
                tourSelectedRow["id_tour_type"] = this.comboBox_tourType.SelectedValue;
                tourSelectedRow["id_schedule"] = this.comboBox_tourDate.SelectedValue;
            }
            //Или добавление
            else
            {
                tourDataTable.Rows.Add(null, textBox_name.Text, textBox_description.Text,
                    comboBox_sight.SelectedValue, comboBox_tourDate.SelectedValue, comboBox_tourType.SelectedValue);
            }
            //Ручной возврат DialogResult из-за изначальной проверки значений
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
