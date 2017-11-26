using System;
using System.Data;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class AddInstructor : Form
    {
        private TouristDataSet.InstructorDataTable instructorDataTable;
        private TouristDataSet.TourTypeDataTable tourTypeDataTable;
        private TouristDataSet.ScheduleDataTable scheduleDataTable;
        private DataRow instructorSelectedRow;

        //Значение параметра row == null при добавлении
        public AddInstructor(TouristDataSet.InstructorDataTable instructorTable, 
            TouristDataSet.TourTypeDataTable tourTypeTable, TouristDataSet.ScheduleDataTable scheduleTable, DataRow row)
        {
            InitializeComponent();
            this.instructorDataTable = instructorTable;
            this.tourTypeDataTable = tourTypeTable;
            this.scheduleDataTable = scheduleTable;
            this.instructorSelectedRow = row;
        }

        private void AddInstructor_Load(object sender, EventArgs e)
        {
            this.comboBox_tourType.DataSource = tourTypeDataTable;
            this.comboBox_tourType.DisplayMember = "tour_type_name";
            this.comboBox_tourType.ValueMember = "id";

            this.comboBox_tourDate.DataSource = scheduleDataTable;
            this.comboBox_tourDate.DisplayMember = "tour_date";
            this.comboBox_tourDate.ValueMember = "id";

            if (instructorSelectedRow != null)
            {
                this.textBox_surname.Text = instructorSelectedRow["surname"].ToString();
                this.textBox_forename.Text = instructorSelectedRow["forename"].ToString();
                this.textBox_patronymic.Text = instructorSelectedRow["patronymic"].ToString();
                this.comboBox_tourType.SelectedValue = instructorSelectedRow["id_tour_type"];
                this.comboBox_tourDate.SelectedValue = instructorSelectedRow["id_schedule"];
            }
            else
            {
                //Убираем выбранные значения с комбобоксов при добавлении
                this.comboBox_tourType.SelectedIndex = -1;
                this.comboBox_tourDate.SelectedIndex = -1;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if(this.comboBox_tourType.SelectedValue == null || Convert.ToInt32(this.comboBox_tourType.SelectedValue) < 0)
            {
                MessageBox.Show("Необходимо выбрать тип тура", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.comboBox_tourDate.SelectedValue == null || Convert.ToInt32(this.comboBox_tourDate.SelectedValue) < 0)
            {
                MessageBox.Show("Необходимо выбрать дату выхода", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Редактирование
            if (instructorSelectedRow != null)
            {
                instructorSelectedRow["surname"] = this.textBox_surname.Text;
                instructorSelectedRow["forename"] = this.textBox_forename.Text;
                instructorSelectedRow["patronymic"] = this.textBox_patronymic.Text;
                instructorSelectedRow["id_tour_type"] = this.comboBox_tourType.SelectedValue;
                instructorSelectedRow["id_schedule"] = this.comboBox_tourDate.SelectedValue;
            }
            //Или добавление
            else
            {
                instructorDataTable.Rows.Add(null, textBox_surname.Text, textBox_forename.Text, 
                    textBox_patronymic.Text, comboBox_tourDate.SelectedValue, comboBox_tourType.SelectedValue);
            }
            //Ручной возврат DialogResult из-за изначальной проверки значений
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
