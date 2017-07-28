using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ScatterPloting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // default values
        private string chartColor = "Red";
        private int numPairs = 100;

        public MainWindow()
        {
            InitializeComponent();

            dataPoints.DataPointStyle = Resources["MyPointColor" + chartColor] as Style; // default point color
            PointViewModel md = new PointViewModel(numPairs);
            dataPoints.DataContext = md;

            pairsNo.Text = numPairs.ToString();

            ComboBoxItem cRed = new ComboBoxItem();
            cRed.Text = "Red";
            cRed.Value = "Red";
            colorComboBox.Items.Add(cRed);
            ComboBoxItem cBlue = new ComboBoxItem();
            cBlue.Text = "Blue";
            cBlue.Value = "Blue";
            colorComboBox.Items.Add(cBlue);
            ComboBoxItem cGreen = new ComboBoxItem();
            cGreen.Text = "Green";
            cGreen.Value = "Green";
            colorComboBox.Items.Add(cGreen);
            colorComboBox.Text = chartColor; 
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public class PointViewModel
        {
            public ObservableCollection<PointPlot> PointPlotCollection { get; set; }

            public PointViewModel(int num)
            {
                Random r = new Random();
                PointPlotCollection = new ObservableCollection<PointPlot>();

                PointPlotCollection.Add(new PointPlot(r, num));
            }
        }

        public class PointPlot : ObservableCollection<Point>
        {
            public PointPlot(Random r, int num)
            {
                for (int i = 0; i < num; i++)
                    Add(new Point { X = i, Y = num*r.NextDouble() });
            }
        }

        public class ComboBoxItem
        {
            private string _text = null;
            private object _value = null;
            public string Text { get { return this._text; } set { this._text = value; } }
            public object Value { get { return this._value; } set { this._value = value; } }
            public override string ToString()
            {
                return this._text;
            }
        }

        private void btn_num_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;

            if (Int32.TryParse(pairsNo.Text, out x) && (x >= 0))
            {
                PointViewModel md = new PointViewModel(x);
                dataPoints.DataContext = md;
                msgWarning.Content = " ";
            }
            else
            {
                msgWarning.Content = "Warning: please input an integer!";
            }
        }

        private void btn_color_Click(object sender, RoutedEventArgs e)
        {
            dataPoints.DataPointStyle = Resources["MyPointColor" + ((ComboBoxItem)colorComboBox.SelectedItem).Text] as Style;
        }
    }


}
