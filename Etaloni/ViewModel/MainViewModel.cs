using Etaloni.DB;
using Etaloni.Model;
using Etaloni.Tools.Observable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace Etaloni.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            EtalonM = new EtalonModel();
            this.EtalonCollection = new ObservableCollection<EtalonModel>();
            //var list = DBHelper.ShowEtalon();
            ShowEtalonsDB();
        }

        #region Properties & Events

        private EtalonModel _EtalonM;
        public EtalonModel EtalonM
        {
            get
            { return this._EtalonM; }
            set
            {
                this._EtalonM = value;
                RaisePropertyChangedEvent("EtalonM");
            }
        }
        public ObservableCollection<EtalonModel> EtalonCollection { get; set; }
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChangedEvent("EtalonCollection");
        }
        #endregion

        #region Commands
        public void ShowEtalonsDB()
        {
            try
            {
                EtalonCollection.Clear();
                foreach (var x in DBHelper.ShowEtalon())
                {
                    var etalonModel = new EtalonModel()
                    {
                        EtalonId = x.EtalonId,
                        Indication = x.Indication,
                        Manufacturer = x.Manufacturer,
                        Model = x.Model,
                        Designation = x.Designation,
                        SerialNumber = x.SerialNumber,
                        Interval = x.Interval,
                        LastCalibration = x.LastCalibration,
                        NextCalibration = x.NextCalibration,
                        Proider = x.Proider,
                        RemoveE = false,
                    };
                    EtalonCollection.Add(etalonModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string AddEtalon()
        {//NalogaView - Dodaj novo nalogo btn -doda in nastavi za novo nalogo
            int id = 0;
            try
            {
                if (EtalonM.EtalonId == 0 && EtalonM.Indication != null && EtalonM.Manufacturer != null && EtalonM.Model != null && EtalonM.Designation != null &&
                                    EtalonM.SerialNumber != null && EtalonM.Interval != 0 &&
                                    EtalonM.LastCalibration.ToString() != null && EtalonM.NextCalibration.ToString() != null && EtalonM.Proider != null)
                {
                    id = DBHelper.CreateCal(EtalonM.EtalonId, EtalonM.Indication, EtalonM.Manufacturer, EtalonM.Model, EtalonM.Designation,
                                    EtalonM.SerialNumber, EtalonM.Interval, EtalonM.LastCalibration, EtalonM.NextCalibration, EtalonM.Proider);
                    this.EtalonCollection.Add(new EtalonModel());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id.ToString();
        }

        public DateTime GetNextCal(DateTime lastCal, int interval)
        {
            DateTime newDateTime = lastCal.AddDays(interval);
            return newDateTime;
        }

        public void SaveUpdateDB()
        {
            try
            {
                foreach (var x in EtalonCollection)
                {
                    if (x.EtalonId != 0 && x.Indication != null && x.Manufacturer != null && x.Model != null && x.Designation != null &&
                                        x.SerialNumber != null && x.Interval != 0 &&
                                        x.LastCalibration.ToString() != null && x.NextCalibration.ToString() != null && x.Proider != null)
                    {
                        if (x.RemoveE == false)
                        {
                            DBHelper.UpdateEtalon(x.EtalonId, x.Indication, x.Manufacturer, x.Model, x.Designation,
                                            x.SerialNumber, x.Interval, x.LastCalibration, x.NextCalibration, x.Proider);
                        }
                        else if (x.RemoveE == true)
                        {
                            deleteList.Add(x.EtalonId);
                        }
                    }
                }
                DeleteEtalon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        List<int> deleteList = new List<int>();
        public void DeleteEtalon()
        {
            foreach(var x in deleteList)
            {
                DBHelper.DeleteEtalon(x);
            }
        }
        #endregion
    }
}
