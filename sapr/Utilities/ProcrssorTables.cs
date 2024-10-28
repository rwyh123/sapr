using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Utilities
{
    public class ProcrssorTables
    {
        public int Support;
        public int Step;
        public double Nx;
        public double Ux;
        public double Stress;
        public ProcrssorTables(int support, int step, double nx, double ux, double stress)
        {
            Support = support;
            Step = step;
            Nx = nx;
            Ux = ux;
            Stress = stress;
        }
        public ProcrssorTables()
        { }
    }
}
