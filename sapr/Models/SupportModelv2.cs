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
    public class SupportModelv2
    {
        private int uid;

        public int UID
        {
            get { return uid; }
            set { uid = value; }
        }

        private int prPower;
        private Rect model;

        public int PrPower { get => prPower; set => prPower = value; }
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
