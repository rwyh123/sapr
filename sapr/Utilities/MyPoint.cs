using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Utilities
{
    public class MyPoint
    {
		private int x;

		public int X
		{
			get { return x; }
			set { x = value; }
		}

		private int y;

		public int Y
		{
			get { return y; }
			set { y = value; }
		}
		public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public MyPoint(MyPoint point)
        {
			X=point.X;
			Y=point.Y;
        }
        public MyPoint()
		{
		}
    }
}
