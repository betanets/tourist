﻿using System;
using System.Windows.Forms;

namespace TouristClient
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void button_sight_Click(object sender, EventArgs e)
        {
            SightForm sightForm = new SightForm();
            sightForm.ShowDialog();
        }

        private void button_tour_type_Click(object sender, EventArgs e)
        {
            TourTypeForm tourTypeForm = new TourTypeForm();
            tourTypeForm.ShowDialog();
        }
    }
}