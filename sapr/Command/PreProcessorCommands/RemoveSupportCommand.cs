using sapr.Models;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace sapr.Command.PreProcessorCommands
{
    public class RemoveSupportCommand : PreProcessorComandBase
    {
        public RemoveSupportCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }

        public override void Execute(object parameter)
        {
            SupportModelv2 supportModel = new SupportModelv2();
            supportModel = _preProcessorViewModel.SelectedShape;
            _preProcessorViewModel.Shapes.CollectionChanged -= _preProcessorViewModel.Draw;
            _preProcessorViewModel.Shapes.Remove(_preProcessorViewModel.Shapes.Where(x => x.Model.Uid == supportModel.Model.Uid).FirstOrDefault());
            _preProcessorViewModel.Shapes.CollectionChanged += _preProcessorViewModel.Draw;
            _preProcessorViewModel.SupportCount--;
            _preProcessorViewModel.IsProcessorCalculated = false;
            ReFillNodesTable(supportModel.Model.Uid);
            ReFillShapesUID();
            {
                int i = 1;
                //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
                foreach (SupportModelv2 supportModelv2 in _preProcessorViewModel.Shapes)
                {
                    supportModelv2.Model.Uid = i++.ToString();
                }

            }
            //ЭТО КОСТЫЛЬ НАДО РАЗОБРАТСЬЯ ВРОДЕ В ФУНКЦИИ РЕВИЛ НОДЕС
            _preProcessorViewModel.IsProcessorCalculated = false;

            if (_preProcessorViewModel.SupportCount == 0)
            {
                _preProcessorViewModel.Nodes.Clear();
                plussesW = 0;
                _preProcessorViewModel.LeftSmth = false;
                _preProcessorViewModel.RightSmth = false;
                _preProcessorViewModel.CanvasLenhgt -= supportModel.Model.Width  * 100 - 100;
                _preProcessorViewModel.CanvasHenght -= supportModel.Model.Height * 100 - 120;
                plussesW = 0;

            }else
            {
                _preProcessorViewModel.CanvasLenhgt -= supportModel.Model.Width * 100;
                _preProcessorViewModel.CanvasHenght = FindHihest.FindHeight() * 100 + 120;
                if (_preProcessorViewModel.CanvasHenght < _preProcessorViewModel.CanvasActualHenght)
                {
                    _preProcessorViewModel.VscrolVisible = System.Windows.Controls.ScrollBarVisibility.Hidden;
                }
                if (_preProcessorViewModel.CanvasLenhgt < _preProcessorViewModel.CanvasActualLenhgt)
                {
                    _preProcessorViewModel.HscrolVisible = System.Windows.Controls.ScrollBarVisibility.Hidden;
                }
            }

        }
        
    }
}
