using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using Clastering.ViewModel;

namespace Clastering
{
    public partial class EntryPoint : MetroForm
    {
        public EntryPoint()
        {
            InitializeComponent();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            ShowDataVM.Show(dataGridView1,cartesianChart2);
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            List<double[]> arrpoint = ShowDataVM.ClasteringShow(cartesianChart1, dataGridView2, Convert.ToInt32(numericUpDown1.Value));

            for (int i = 0; i < arrpoint.Count; i++)
            {
                dataGridView2.Rows.Add(i, arrpoint[i][1], arrpoint[i][2], arrpoint[i][3]);
            }

            ShowDataVM.CreateChartsClaster(arrpoint,cartesianChart1, Convert.ToInt32(numericUpDown1.Value));
        }

        private void EntryPoint_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(0, 65.0, 220.0);
            dataGridView1.Rows.Add(0, 73.0, 160.0);
            dataGridView1.Rows.Add(0, 59.0, 110.0);
            dataGridView1.Rows.Add(0, 61.0, 120.0);
            dataGridView1.Rows.Add(0, 75.0, 150.0);
            dataGridView1.Rows.Add(0, 67.0, 240.0);
            dataGridView1.Rows.Add(0, 68.0, 230.0);
            dataGridView1.Rows.Add(0, 70.0, 220.0);
            dataGridView1.Rows.Add(0, 62.0, 130.0);
            dataGridView1.Rows.Add(0, 66.0, 210.0);
            dataGridView1.Rows.Add(0, 77.0, 190.0);
            dataGridView1.Rows.Add(0, 75.0, 180.0);
            dataGridView1.Rows.Add(0,  74.0, 170.0);
            dataGridView1.Rows.Add(0,  70.0, 210.0);
            dataGridView1.Rows.Add(0, 61.0, 110.0);
            dataGridView1.Rows.Add(0,  58.0, 100.0);
            dataGridView1.Rows.Add(0, 66.0, 230.0);
            dataGridView1.Rows.Add(0,  59.0, 120.0);
            dataGridView1.Rows.Add(0, 68.0, 210.0);
            dataGridView1.Rows.Add(0, 61.0, 130.0);
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
           
        }

        private void cartesianChart1_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            ShowDataVM.RebuildClastering(cartesianChart1, dataGridView2, (int)chartPoint.X, (int)chartPoint.Y, Convert.ToInt32(numericUpDown1.Value));
        }
    }
}
