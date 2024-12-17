using sapr.Models;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace sapr.Command.PreProcessorCommands
{
    public class AddSupportCommand : PreProcessorComandBase
    {
        public event EventHandler ChangeState;
        public AddSupportCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            Window1 window = new Window1();
            if (window.ShowDialog() == true)
            {
                //vars
                var node0 = new NodeModel(0, _preProcessorViewModel.SupportCount + 1);
                var node = new NodeModel(0, _preProcessorViewModel.SupportCount + 2);
                var supp = new SupportModelv2(0, new Rectangle());

                //create node
                if (_preProcessorViewModel.SupportCount == 0)
                { 
                    _preProcessorViewModel.Nodes.Add(node0);
                }
                _preProcessorViewModel.Nodes.Add(node);

                //create shape
                supp.Model.Height = int.Parse(window.Radius);
                supp.Model.Width = int.Parse(window.Lenght);

                if (int.Parse(window.Lenght) < 10 && int.Parse(window.Lenght) >= 1)
                    supp.Multiplayer = 1;
                else
                    supp.Multiplayer = 0;
                supp.Model.Stroke = Brushes.Black;
                supp.Model.StrokeThickness = 1;
                supp.Model.Uid = _preProcessorViewModel.SupportCount.ToString();
                ChangeState?.Invoke(false, EventArgs.Empty);
                _preProcessorViewModel.IsProcessorCalculated = false;
                _preProcessorViewModel.SupportCount++;

                _preProcessorViewModel.Shapes.Add(supp);
                node0.PropertyChanged += _preProcessorViewModel.Draw;
                node.PropertyChanged += _preProcessorViewModel.Draw;


                ResizeCanvas(int.Parse(window.Radius), int.Parse(window.Lenght));

            }
        }
    }
}
