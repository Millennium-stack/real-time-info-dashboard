using CodeClinic;
using LiveCharts;
using LiveCharts.Configurations;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for ConstantChangesChart.xaml
    /// </summary>
    public partial class ConstantChangesChart : UserControl, INotifyPropertyChanged
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

            string fileName = @"C:\Users\Millennium Singha\Downloads\Ex_Files_Code_Clinic_C_Sharp\Ex_Files_Code_Clinic_C_Sharp\Exercise Files\Ch06\dashBoardData.csv";

            foreach (var ft in FactoryTelemetry.Load(fileName))
            {
                chartValues.Add(ft);

                this.EngineEfficiency = ft.Efficiency;

                if (chartValues.Count > 30)
                    chartValues.RemoveAt(0);

                Thread.Sleep(30);
            }
        }

        private double _EngineEfficiency = 65;
        public double EngineEfficiency {
            get
            {
                return _EngineEfficiency;
            }
            set
            {
                _EngineEfficiency = value;
                OnPropertyChanged(nameof(EngineEfficiency));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
