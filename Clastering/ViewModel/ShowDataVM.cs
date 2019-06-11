using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using LiveCharts;
using LiveCharts.Defaults;
using System.Windows.Media;
using Clastering.Model;

namespace Clastering.ViewModel
{
    class ShowDataVM
    {
        private static double[][] arr_point { get; set; }
        private static List<double[]> arr_vector { get; set; }

        public static void Show(DataGridView dgv, LiveCharts.WinForms.CartesianChart cc)
        {
            cc.Series.Clear();
            arr_point = new double[dgv.Rows.Count - 1][];

            ScatterSeries ss = new ScatterSeries();
            ss.Title = "Основные данные";
            ss.PointGeometry = DefaultGeometries.Triangle;
            ss.StrokeThickness = 2;
            ss.Fill = Brushes.Transparent;

            ChartValues <ObservablePoint> pointvalue = new ChartValues<ObservablePoint>();
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                arr_point[i] = new double[] { Convert.ToDouble(dgv.Rows[i].Cells[1].Value), Convert.ToDouble(dgv.Rows[i].Cells[2].Value) };
                pointvalue.Add(new ObservablePoint( Convert.ToDouble(dgv.Rows[i].Cells[1].Value), Convert.ToDouble(dgv.Rows[i].Cells[2].Value)));
            }
            ss.Values = pointvalue;
            cc.Series.Add(ss);
        }

        public static List<double[]> ClasteringShow(LiveCharts.WinForms.CartesianChart cc, DataGridView dgv_out, int numberclass)
        {
            ClasteringM do_clastering = new ClasteringM();
            List<double[]> output_arr = new List<double[]>();

            do_clastering.SetValueClaster(numberclass);

            int[] outVector = do_clastering.Cluster(arr_point, numberclass);
            
            for (int i = 0; i < outVector.Length; i++)
            {
                double[] buffer = new double[4];
                buffer[0] = i;
                buffer[1] = arr_point[i][0];
                buffer[2] = arr_point[i][1];
                buffer[3] = outVector[i];
                output_arr.Add(buffer);
            }
            arr_vector = output_arr;
            return output_arr;
        }

        public static void CreateChartsClaster(List<double[]> arr_point, LiveCharts.WinForms.CartesianChart cc, int clastervalue)
        {
            ChartValues<ObservablePoint> pointvalue = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue4 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue5 = new ChartValues<ObservablePoint>();

            for (int i = 0; i < arr_point.Count; i++)
            {
                switch (Convert.ToInt32(arr_point[i][3]))
                {
                    case 0:
                        pointvalue.Add(new ObservablePoint(arr_point[i][1], arr_point[i][2]));
                        break;
                    case 1:
                        pointvalue2.Add(new ObservablePoint(arr_point[i][1], arr_point[i][2]));
                        break;
                    case 2:
                        pointvalue3.Add(new ObservablePoint(arr_point[i][1], arr_point[i][2]));
                        break;
                    case 3:
                        pointvalue4.Add(new ObservablePoint(arr_point[i][1], arr_point[i][2]));
                        break;
                    case 4:
                        pointvalue5.Add(new ObservablePoint(arr_point[i][1], arr_point[i][2]));
                        break;
                }
            }

            ScatterSeries pointvalues = new ScatterSeries();
            pointvalues.Title = "Series 1";
            pointvalues.Values = pointvalue;
            pointvalues.PointGeometry = DefaultGeometries.Triangle;
            pointvalues.StrokeThickness = 2;
            pointvalues.Fill = Brushes.Transparent;

            ScatterSeries pointvalue2s = new ScatterSeries();
            pointvalue2s.Title = "Series 2";
            pointvalue2s.Values = pointvalue2;
            pointvalue2s.PointGeometry = DefaultGeometries.Circle;
            pointvalue2s.StrokeThickness = 2;
            pointvalue2s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue3s = new ScatterSeries();
            pointvalue3s.Title = "Series 3";
            pointvalue3s.Values = pointvalue3;
            pointvalue3s.PointGeometry = DefaultGeometries.Cross;
            pointvalue3s.StrokeThickness = 2;
            pointvalue3s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue4s = new ScatterSeries();
            pointvalue4s.Title = "Series 4";
            pointvalue4s.Values = pointvalue4;
            pointvalue4s.PointGeometry = DefaultGeometries.Diamond;
            pointvalue4s.StrokeThickness = 2;
            pointvalue4s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue5s = new ScatterSeries();
            pointvalue5s.Title = "Series 5";
            pointvalue5s.Values = pointvalue5;
            pointvalue5s.PointGeometry = DefaultGeometries.Square;
            pointvalue5s.StrokeThickness = 2;
            pointvalue5s.Fill = Brushes.Transparent;

            SeriesCollection sc = new SeriesCollection();
            sc.Add(pointvalues);
            sc.Add(pointvalue2s);
            sc.Add(pointvalue3s);
            sc.Add(pointvalue4s);
            sc.Add(pointvalue5s);
            cc.Series = sc;
        }
        public static void RebuildClastering(LiveCharts.WinForms.CartesianChart cRebuild, DataGridView dg, int x, int y, int numberclass)
        {
            cRebuild.Series.Clear();
            List<double[]> new_vector = new List<double[]>();
            for (int i = 0; i < arr_vector.Count; i++)
            {
                if (x != arr_vector[i][1] && y != arr_vector[i][2])
                {
                    new_vector.Add(arr_vector[i]);
                }
                
            }

            double[][] arr_point_new = new double[new_vector.Count][];
            for (int i = 0; i < new_vector.Count; i++)
            {
                arr_point_new[i] = new double[] { new_vector[i][1], new_vector[i][2]};
            }

            ClasteringM do_clastering1 = new ClasteringM();
            do_clastering1.SetValueClaster(numberclass);

            int[] outVector = do_clastering1.Cluster(arr_point_new, numberclass);
            List<double[]> new_vector_claster = new List<double[]>();
            for (int i = 0; i < outVector.Length; i++)
            {
                double[] buffer = new double[4];
                buffer[0] = i;
                buffer[1] = arr_point_new[i][0];
                buffer[2] = arr_point_new[i][1];
                buffer[3] = outVector[i];
                new_vector_claster.Add(buffer);
            }


            ChartValues<ObservablePoint> pointvalue = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue4 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> pointvalue5 = new ChartValues<ObservablePoint>();

            for (int i = 0; i < new_vector_claster.Count; i++)
            {
                switch (Convert.ToInt32(new_vector_claster[i][3]))
                {
                    case 0:
                        pointvalue.Add(new ObservablePoint(new_vector_claster[i][1], new_vector_claster[i][2]));
                        break;
                    case 1:
                        pointvalue2.Add(new ObservablePoint(new_vector_claster[i][1], new_vector_claster[i][2]));
                        break;
                    case 2:
                        pointvalue3.Add(new ObservablePoint(new_vector_claster[i][1], new_vector_claster[i][2]));
                        break;
                    case 3:
                        pointvalue4.Add(new ObservablePoint(new_vector_claster[i][1], new_vector_claster[i][2]));
                        break;
                    case 4:
                        pointvalue5.Add(new ObservablePoint(new_vector_claster[i][1], new_vector_claster[i][2]));
                        break;
                }
            }

            ScatterSeries pointvalues = new ScatterSeries();
            pointvalues.Title = "Series 1";
            pointvalues.Values = pointvalue;
            pointvalues.PointGeometry = DefaultGeometries.Triangle;
            pointvalues.StrokeThickness = 2;
            pointvalues.Fill = Brushes.Transparent;

            ScatterSeries pointvalue2s = new ScatterSeries();
            pointvalue2s.Title = "Series 2";
            pointvalue2s.Values = pointvalue2;
            pointvalue2s.PointGeometry = DefaultGeometries.Circle;
            pointvalue2s.StrokeThickness = 2;
            pointvalue2s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue3s = new ScatterSeries();
            pointvalue3s.Title = "Series 3";
            pointvalue3s.Values = pointvalue3;
            pointvalue3s.PointGeometry = DefaultGeometries.Cross;
            pointvalue3s.StrokeThickness = 2;
            pointvalue3s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue4s = new ScatterSeries();
            pointvalue4s.Title = "Series 4";
            pointvalue4s.Values = pointvalue4;
            pointvalue4s.PointGeometry = DefaultGeometries.Diamond;
            pointvalue4s.StrokeThickness = 2;
            pointvalue4s.Fill = Brushes.Transparent;

            ScatterSeries pointvalue5s = new ScatterSeries();
            pointvalue5s.Title = "Series 5";
            pointvalue5s.Values = pointvalue5;
            pointvalue5s.PointGeometry = DefaultGeometries.Square;
            pointvalue5s.StrokeThickness = 2;
            pointvalue5s.Fill = Brushes.Transparent;

            SeriesCollection sc = new SeriesCollection();
            sc.Add(pointvalues);
            sc.Add(pointvalue2s);
            sc.Add(pointvalue3s);
            sc.Add(pointvalue4s);
            sc.Add(pointvalue5s);
            cRebuild.Series = sc;
           
        }
    }

    
}
