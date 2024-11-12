using sapr.Models;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sapr.Command.PreProcessorCommands
{
    public  class PreProcessorComandBase : CommandBase
    {
        //сделать вычитание как то хз, 
        private static int plussesH = 120;
        protected static int plussesW = 0;
        static public PreProcessorViewModel _preProcessorViewModel { get; set; }
        public PreProcessorComandBase(PreProcessorViewModel preProcessorViewModel)
        {
            _preProcessorViewModel = preProcessorViewModel;
            
        }
        public static void ResizeCanvas(int radius, int lenght)
        {
            if (radius * 100 + plussesH > _preProcessorViewModel.CanvasActualHenght)
            {
                _preProcessorViewModel.CanvasHenght = radius * 100 + plussesH;
                _preProcessorViewModel.VscrolVisible = ScrollBarVisibility.Visible;


            }
            if (_preProcessorViewModel.CalculateAllWidth() * 100 + plussesW > _preProcessorViewModel.CanvasActualLenhgt)
            {
                if (plussesW == 0)
                {
                    plussesW = 100;
                    _preProcessorViewModel.CanvasLenhgt = (_preProcessorViewModel.CalculateAllWidth() * 100 + plussesW);
                    _preProcessorViewModel.HscrolVisible = ScrollBarVisibility.Visible;
                }else
                _preProcessorViewModel.CanvasLenhgt += lenght * 100;
                _preProcessorViewModel.HscrolVisible = ScrollBarVisibility.Visible;
            }

        }
        protected void ReFillNodesTable(string id)
        {
            //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
            _preProcessorViewModel.Nodes.Remove(_preProcessorViewModel.Nodes[_preProcessorViewModel.Nodes.Count - 1]);
            for (int i = int.Parse(id); i <= _preProcessorViewModel.SupportCount; i++)
            {
                _preProcessorViewModel.Nodes[i - 1] = (new NodeModel(0, i));
            }
        }
        protected void ReFillShapesUID()
        {
            int i = 1;
            //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
            foreach (SupportModelv2 supportModelv2 in _preProcessorViewModel.Shapes)
            {
                supportModelv2.Model.Uid = i++.ToString();
            }

        }
        protected static void Clear()
        {
            _preProcessorViewModel.Shapes.CollectionChanged -= _preProcessorViewModel.Draw;
            _preProcessorViewModel.SupportCount = 0;
            _preProcessorViewModel.Shapes.Clear();
            _preProcessorViewModel.Nodes.Clear();
            _preProcessorViewModel.LeftSmth = false;
            _preProcessorViewModel.RightSmth = false;
            _preProcessorViewModel.CanvasLenhgt = 0;
            _preProcessorViewModel.CanvasHenght = 0;
            _preProcessorViewModel.CanvasChildrens.Clear();
            _preProcessorViewModel.Shapes.CollectionChanged += _preProcessorViewModel.Draw;
            _preProcessorViewModel.IsProcessorCalculated = false;
        }

    }
}
