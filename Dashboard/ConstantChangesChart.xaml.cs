using CodeClinic;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for ConstantChangesChart.xaml
    /// </summary>
    public partial class ConstantChangesChart : UserControl
    {
        public ConstantChangesChart()
        {
            InitializeComponent();

            lsEfficiency.Configuration = Mappers.Xy<FactoryTelemetry>().X(ft => ft.TimeStamp.Ticks).Y(ft => ft.Efficiency);

            DataContext = this;
        }

        private bool readingData = false;
        private ChartValues<FactoryTelemetry> chartValues { get; set; } = new ChartValues<FactoryTelemetry>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!readingData)
            {
                Task.Factory.StartNew(ReadData);
            }
            readingData = !readingData;
        }

        private void ReadData()
        {
            // TODO: Populate the collection chartValues

            string fileName = @"";

            foreach (var ft in FactoryTelemetry.Load(fileName))
            {
                chartValues.Add(ft);

                if (chartValues.Count > 30)
                    chartValues.Remove(0);

                Thread.Sleep(30);
            }
        }
    }
}
