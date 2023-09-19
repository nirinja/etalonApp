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
            EtalonVM = new EtalonViewModel();
        }
        public EtalonViewModel EtalonVM { get; set; }
    }
}
