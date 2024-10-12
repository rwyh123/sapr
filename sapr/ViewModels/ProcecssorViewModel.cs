using sapr.Command;
using sapr.Models;
using sapr.Stores;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace sapr.ViewModels
{
    public class ProcecssorViewModel : ViewModelBase
    {
        public List<List<double>> MatrixA;
        public List<double> MatrixB;
        public List<double> VactorQ;
        private string myTextA;
        private string myTextB;
        private string myTextC;
        private string myTextD;
        private SuportStore myStore = SuportStore.Instance;

        public string MyTextD
        {
            get { return myTextD; }
            set
            {
                myTextD = value;
                OnPropertyChanged(nameof(MyTextD));
            }
        }
        public string MyTextA
        {
            get { return myTextA; }
            set
            {
                myTextA = value;
                OnPropertyChanged(nameof(MyTextA));
            }
        }
        public string MyTextB
        {
            get { return myTextB; }
            set
            {
                myTextB = value;
                OnPropertyChanged(nameof(MyTextB));
            }
        }
        public string MyTextC
        {
            get { return myTextC; }
            set
            {
                myTextC = value;
                OnPropertyChanged(nameof(MyTextC));
            }
        }

        public ICommand setText { get; }
        public ProcecssorViewModel()
        {
            MatrixA = new List<List<double>>();
            MatrixB = new List<double>();
            VactorQ = new List<double>();
            setText = new SetTextCommand(this);
        }


    }
    
}
