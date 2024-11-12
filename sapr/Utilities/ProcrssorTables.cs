using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace sapr.Utilities
{
    public class ProcrssorTables
    {
        public int Support { get; set; }
        public double Step { get; set; }
        public double Nx { get; set; }
        public double Ux { get; set; }
        public double Dx { get; set; }
        public double Stress { get; set; }
        public Brush Brush { get; set; }


        public ProcrssorTables(int support, double step, double nx, double ux, double dx, double stress)
        {
            Support = support;
            Step = step;
            Nx = nx;
            Ux = ux;
            Dx = dx;
            Stress = stress;
            if(dx > stress)
             Brush = Brushes.Red;
            else
             Brush = Brushes.Black;

        }
        public ProcrssorTables()
        { }
    }
}
