using Etaloni.DB;
using Etaloni.Model;
using Etaloni.Tools;
using Etaloni.Tools.Observable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Etaloni.ViewModel
{
    public class EtalonViewModel : ObservableObject
    {
        public EtalonViewModel()
        {
            EtalonM = new EtalonModel();
            this.EtalonCollection = new ObservableCollection<EtalonModel>();
            ShowEtalonsDB();
            RelayCommands();
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
        public void ContentCollectionChanged()
        {
            RaisePropertyChangedEvent("EtalonCollection");
        }
        #endregion
        #region Declering Commands

        public ICommand AddEtalonICommand { get; private set; }
        public ICommand SaveUpdateDBICommand { get; private set; }

        public void RelayCommands()
        {
            AddEtalonICommand = new RelayCommand(AddEtalonCommand);
            SaveUpdateDBICommand = new RelayCommand(SaveUpdateDBCommand);

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
                        Provider = x.Provider,
                        RemoveE = false,
                    };
                    EtalonCollection.Add(etalonModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        public void AddEtalonCommand(object obj)
        {//NalogaView - Dodaj novo nalogo btn -doda in nastavi za novo nalogo
            int id = 0;
            try
            {
                GetNextCal();
                if (EtalonM.EtalonId == 0 && EtalonM.Indication != null && EtalonM.Manufacturer != null && EtalonM.Model != null && EtalonM.Designation != null &&
                                    EtalonM.SerialNumber != null && EtalonM.Interval != 0 &&
                                    EtalonM.LastCalibration.ToString() != null && EtalonM.NextCalibration.ToString() != null && EtalonM.Provider != null)
                {
                    id = DBHelper.CreateCal(EtalonM.EtalonId, EtalonM.Indication, EtalonM.Manufacturer, EtalonM.Model, EtalonM.Designation,
                                    EtalonM.SerialNumber, EtalonM.Interval, EtalonM.LastCalibration, EtalonM.NextCalibration, EtalonM.Provider);
                    this.EtalonCollection.Add(new EtalonModel());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        public void SaveUpdateDBCommand(object obj)
        {
            try
            {
                foreach (var x in EtalonCollection)
                {
                    if (x.EtalonId != 0 && x.Indication != null && x.Manufacturer != null && x.Model != null && x.Designation != null &&
                                        x.SerialNumber != null && x.Interval != 0 &&
                                        x.LastCalibration.ToString() != null && x.NextCalibration.ToString() != null && x.Provider != null)
                    {
                        if (x.RemoveE == false)
                        {
                            DBHelper.UpdateEtalon(x.EtalonId, x.Indication, x.Manufacturer, x.Model, x.Designation,
                                            x.SerialNumber, x.Interval, x.LastCalibration, x.NextCalibration, x.Provider);
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

        public void GetNextCal()
        {
            DateTime lastCal = EtalonM.LastCalibration;
            int interval = EtalonM.Interval;
            if (interval != 0)
            {
                DateTime newDateTime = lastCal.AddDays(interval);
                EtalonM.NextCalibration = newDateTime;
            }
        }

        List<int> deleteList = new List<int>();
        public void DeleteEtalon()
        {
            foreach (var x in deleteList)
            {
                DBHelper.DeleteEtalon(x);
            }
        }
        #endregion
    }
}
