using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace sapr.Models
{
    public class NodeModel : ModelBase
    {
        private double _poPower;

		public double PoPower
		{
			get { return _poPower; }
			set
			{
				_poPower = value;
				OnPropertyChanged(nameof(PoPower));

            }
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
