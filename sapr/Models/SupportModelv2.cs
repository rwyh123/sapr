using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sapr.Models
{
    public class SupportModelv2 : ModelBase
    {
        private int uid;

        public int UID
        {
            get { return uid; }
            set { uid = value; }
        }

        private double prPower;
        private Rect model;

        public double PrPower
        { 
            get => prPower;
            set
            {
                prPower = value;
                OnPropertyChanged(nameof(PrPower));
            }
        }
        public Rect Model { get => model; set => model = value; }

        

        public SupportModelv2(int prp, int pop, Rect rectangle,int uid)
        {
            UID = uid;
            PrPower = prp;
            Model = rectangle;
        }
        public SupportModelv2()
        {
        }
    }
}
