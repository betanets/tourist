using System;
using System.Data;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class AddSight : Form
    {
        private TouristDataSet.SightDataTable sightDataTable;
        private DataRow sightSelectedRow;

        //Значение параметра row == null при добавлении
        public AddSight(TouristDataSet.SightDataTable table, DataRow row)
        {
            InitializeComponent();
            this.sightDataTable = table;
            this.sightSelectedRow = row;
        }

        //Подгрузка значений при редактировании
        private void AddSight_Load(object sender, EventArgs e)
        {
            if(sightSelectedRow != null)
            {
                this.textBox_name.Text = sightSelectedRow["sight_name"].ToString();
                this.textBox_description.Text = sightSelectedRow["sight_descr"].ToString();
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //Редактирование
            if(sightSelectedRow != null)
            {
                sightSelectedRow["sight_name"] = this.textBox_name.Text;
                sightSelectedRow["sight_descr"] = this.textBox_description.Text;
            }
            //Или добавление
            else
            {
                sightDataTable.Rows.Add(null, textBox_name.Text, textBox_description.Text);
            }
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
