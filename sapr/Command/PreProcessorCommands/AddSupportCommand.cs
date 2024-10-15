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

namespace sapr.Command.PreProcessorCommands
{
    public class AddSupportCommand : PreProcessorComandBase
    {
        public AddSupportCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            Window1 window = new Window1();
            if (window.ShowDialog() == true)
            {
                if (_preProcessorViewModel.SupportCount == 0)
                { 
                    var node0 = new NodeModel(0, _preProcessorViewModel.SupportCount + 1);
                    node0.PropertyChanged += _preProcessorViewModel.Draw;
                    _preProcessorViewModel.Nodes.Add(node0);
                }
                _preProcessorViewModel.SupportCount++;

                var node = new NodeModel(0, _preProcessorViewModel.SupportCount + 1);
                node.PropertyChanged += _preProcessorViewModel.Draw;
                _preProcessorViewModel.Nodes.Add(node);

                var supp = new SupportModelv2(0, 0, new Rect(0, 0, int.Parse(window.Lenght), int.Parse(window.Radius)), _preProcessorViewModel.SupportCount);
                supp.PropertyChanged += _preProcessorViewModel.Draw;
                _preProcessorViewModel.Shapes.Add(supp);

                _preProcessorViewModel.OnPropertyChanged(nameof(_preProcessorViewModel.CeckBoxIsEnabled));

                ResizeCanvas(int.Parse(window.Radius), int.Parse(window.Lenght));
                //scrollbar.ScrollToVerticalOffset(scrollbar.ScrollableHeight / 2);
                //scrollbar.ScrollToHorizontalOffset(scrollbar.ScrollableWidth / 2);

            }
        }

    }
}
