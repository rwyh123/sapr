using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Utilities
{
    public class MyPoint
    {
		private Nullable<int> x;

		public Nullable<int> X
		{
			get { return x; }
			set { x = value; }
		}

		private Nullable<int> y;

		public Nullable<int> Y
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
