using System;
using System.Data;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class AddTourType : Form
    {
        private TouristDataSet.TourTypeDataTable tourTypeDataTable;
        private DataRow tourTypeSelectedRow;

        public AddTourType(TouristDataSet.TourTypeDataTable table, DataRow row)
        {
            InitializeComponent();
            this.tourTypeDataTable = table;
            this.tourTypeSelectedRow = row;
        }

        private void AddTourType_Load(object sender, EventArgs e)
        {
            if (tourTypeSelectedRow != null)
            {
                this.textBox_name.Text = tourTypeSelectedRow["tour_type_name"].ToString();
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //Редактирование
            if (tourTypeSelectedRow != null)
            {
                tourTypeSelectedRow["tour_type_name"] = this.textBox_name.Text;
            }
            //Или добавление
            else
            {
                tourTypeDataTable.Rows.Add(null, textBox_name.Text);
            }
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
