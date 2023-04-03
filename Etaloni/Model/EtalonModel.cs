using Etaloni.Tools.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etaloni.Model
{
    public class EtalonModel : ObservableObject
    {
        public EtalonModel() { }

        private int _EtalonId;
        public int EtalonId
        {
            get
            {
                return this._EtalonId;
            }
            set
            {
                this._EtalonId = value;
                RaisePropertyChangedEvent("EtalonId");
            }
        }
        private string _Indication;
        public string Indication
        {
            get
            {
                return this._Indication;
            }
            set
            {
                this._Indication = value;
                RaisePropertyChangedEvent("Indication");
            }
        }
        private string _Manufacturer;
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                this._Manufacturer = value;
                RaisePropertyChangedEvent("Manufacturer");
            }
        }

        private string _Model;
        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                this._Model = value;
                RaisePropertyChangedEvent("Model");
            }
        }
        private string _Designation;
        public string Designation
        {
            get
            {
                return this._Designation;
            }
            set
            {
                this._Designation = value;
                RaisePropertyChangedEvent("Designation");
            }
        }
        private string _SerialNumber;
        public string SerialNumber
        {
            get
            {
                return this._SerialNumber;
            }
            set
            {
                this._SerialNumber = value;
                RaisePropertyChangedEvent("SerialNumber");
            }
        }

        private string _Proider;
        public string Proider
        {
            get
            {
                return this._Proider;
            }
            set
            {
                this._Proider = value;
                RaisePropertyChangedEvent("Proider");
            }
        }

        private int _Interval;
        public int Interval
        {
            get
            {
                return this._Interval;
            }
            set
            {
                this._Interval = value;
                RaisePropertyChangedEvent("Interval");
            }
        }
        private DateTime _LastCalibration;
        public DateTime LastCalibration
        {
            get
            {
                return this._LastCalibration;
            }
            set
            {
                this._LastCalibration = value;
                RaisePropertyChangedEvent("LastCalibration");
            }
        }
        private DateTime _NextCalibration;
        public DateTime NextCalibration
        {
            get
            {
                return this._NextCalibration;
            }
            set
            {
                this._NextCalibration = value;
                RaisePropertyChangedEvent("NextCalibration");
            }
        }

        private bool _RemoveE;
        public bool RemoveE
        {
            get
            {
                return this._RemoveE;
            }
            set
            {
                this._RemoveE = value;
                RaisePropertyChangedEvent("RemoveE");
            }
        }
    }
}
