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
        private List<List<double>> matrixA;
        public List<double> MatrixB;
        public List<double> VactorQ;
        public Dictionary<String,double> NX;
        public Dictionary<String,double> DX;
        public Dictionary<String,double> UX;
        private string myTextA;
        private string myTextB;
        private string myTextC;
        private string myTextD;
        private string myTextNX;
        private string myTextDX;
        private string myTextUX;
       
        private SuportStore myStore = SuportStore.Instance;

        public string MyTextNX
        {
            get { return myTextNX; }
            set 
            {
                myTextNX = value;
                OnPropertyChanged(nameof(MyTextNX));
            }
        }
        public string MyTextDX
        {
            get { return myTextDX; }
            set
            {
                myTextDX = value;
                OnPropertyChanged(nameof(MyTextDX));
            }
        }
        public string MyTextUX
        {
            get { return myTextUX; }
            set
            {
                myTextUX = value;
                OnPropertyChanged(nameof(MyTextUX));
            }
        }
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
        public List<List<double>> MatrixA
        {
            get => matrixA; 
            set => matrixA = value; 
        }

        public ProcecssorViewModel()
        {
            MatrixA = new List<List<double>>();
            MatrixB = new List<double>();
            VactorQ = new List<double>();
            NX = new Dictionary<string, double>();
            DX = new Dictionary<string, double>();
            UX = new Dictionary<string, double>();
            setText = new SetTextCommand(this);
        }


    }
    
}
