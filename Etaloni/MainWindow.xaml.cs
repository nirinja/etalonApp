using Etaloni.DB;
using Etaloni.Model;
using Etaloni.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Etaloni
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            EtalonVM = new MainViewModel();
            this.EtalonCollection = new ObservableCollection<EtalonModel>();
            this.EtalonCollection = EtalonVM.EtalonCollection;
        }
        public ObservableCollection<EtalonModel> EtalonCollection { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender; // cast sender to Button type
            if (clickedButton.Name.ToString() == "GetCalDate")
            {
                GetNextCalDate();
            }
            else if (clickedButton.Name.ToString() == "Add")
            {
                GetEtalonMValues();
            }
            else if (clickedButton.Name.ToString() == "Save")
            {
                SaveUpdateDB();
            }
        }

        private MainViewModel _EtalonVM;
        public MainViewModel EtalonVM
        {
            get
            { return this._EtalonVM; }
            set
            {
                this._EtalonVM = value;
            }
        }

        private void GetEtalonMValues()
        {
            try
            {
                int etalonId;
                if (int.TryParse(EtalonId.Text, out etalonId))
                {
                    EtalonVM.EtalonM.EtalonId = etalonId;
                }
                EtalonVM.EtalonM.Indication = Indication.Text;
                EtalonVM.EtalonM.Manufacturer = Manufacturer.Text;
                EtalonVM.EtalonM.Designation = Designation.Text;
                EtalonVM.EtalonM.Model = Model.Text;
                EtalonVM.EtalonM.Proider = Proider.Text;
                EtalonVM.EtalonM.SerialNumber = SerialNumber.Text;
                int interval;
                if (int.TryParse(Interval.Text, out interval))
                {
                    EtalonVM.EtalonM.Interval = interval;
                }
                if (interval != 0 && LastCalibration.SelectedDate != null)
                {
                    EtalonVM.EtalonM.LastCalibration = LastCalibration.SelectedDate.Value;
                    if (NextCalibration.SelectedDate == null)
                    {
                        GetNextCalDate();
                    }
                    else
                    {
                        EtalonVM.EtalonM.NextCalibration = NextCalibration.SelectedDate.Value;
                    }
                }
                EtalonId.Text = EtalonVM.AddEtalon();
                UpdateCollection();
                ClearEtalon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetNextCalDate()
        {
            int interval;
            if (int.TryParse(Interval.Text, out interval))
            {
                EtalonVM.EtalonM.Interval = interval;
            }
            if (LastCalibration.SelectedDate != null && interval > 0)
            {
                EtalonVM.EtalonM.LastCalibration = LastCalibration.SelectedDate.Value;
                DateTime lastCal = EtalonVM.EtalonM.LastCalibration;
                bool whilein = true;
                while (whilein)
                {
                    NextCalibration.SelectedDate = EtalonVM.GetNextCal(lastCal, interval);
                    EtalonVM.EtalonM.NextCalibration = NextCalibration.SelectedDate.Value;

                    if (NextCalibration.SelectedDate < DateTime.Now)
                    {
                        MessageBox.Show("The supposed next calibration has already passed (" + NextCalibration.SelectedDate.Value + ")");
                        lastCal = NextCalibration.SelectedDate.Value;
                        whilein = true;
                    }
                    else
                    {
                        whilein = false;
                    }
                }
                NextCalibration.SelectedDate = EtalonVM.GetNextCal(LastCalibration.SelectedDate.Value, interval);
                EtalonVM.EtalonM.NextCalibration = NextCalibration.SelectedDate.Value;
                if (NextCalibration.SelectedDate < DateTime.Now)
                {

                }
                EtalonVM.EtalonM.NextCalibration = NextCalibration.SelectedDate.Value;
            }
        }

        private void SaveUpdateDB()
        {
            EtalonVM.SaveUpdateDB();
            UpdateCollection();
        }
        private void UpdateCollection()
        {
            EtalonVM.ShowEtalonsDB();
            this.EtalonCollection = EtalonVM.EtalonCollection;
        }
        private void ClearEtalon()
        {
            EtalonId.Text = null;
            Indication.Text = null;
            Manufacturer.Text = null;
            Designation.Text = null;
            Model.Text = null;
            Proider.Text = null;
            SerialNumber.Text = null;
            Interval.Text = null;
            LastCalibration.SelectedDate = null;
            NextCalibration.SelectedDate = null;
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            GetNextCalDate();
        }
        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetNextCalDate();
        }
    }
}