using System;
using System.Windows.Forms;
using TouristClient.localhost;

namespace TouristClient
{
    public partial class SightForm : Form
    {
        public SightForm()
        {
            InitializeComponent();
        }

        private TouristServiceExporter touristServiceExporter = new TouristServiceExporter();
        private TouristDataSet touristDataSet;

        private void SightForm_Load(object sender, EventArgs e)
        {
            touristDataSet = touristServiceExporter.ReadSight();
            dataGridView_sight.DataSource = touristDataSet;
            dataGridView_sight.DataMember = "Sight";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            touristServiceExporter.WriteSight(touristDataSet);
        }
    }
}
