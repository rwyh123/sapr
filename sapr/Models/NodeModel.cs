using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace sapr.Models
{
    public class NodeModel
    {
		private double _poPower;

		public double PoPower
		{
			get { return _poPower; }
			set { _poPower = value; }
		}

		private int _nodeNumber;

		public int NodeNumber
		{
			get { return _nodeNumber; }
			set { _nodeNumber = value; }
			
		}

		public NodeModel(int poPower, int nodeNumber)
		{
			PoPower = poPower;
			NodeNumber = nodeNumber;

        }
        public NodeModel()
		{ }


	}
}
