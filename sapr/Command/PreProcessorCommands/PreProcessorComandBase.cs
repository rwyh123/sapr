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
        private static int plussesH = 0;
        private static int plussesW = 0;
        static public PreProcessorViewModel _preProcessorViewModel { get; set; }
        public PreProcessorComandBase(PreProcessorViewModel preProcessorViewModel)
        {
            _preProcessorViewModel = preProcessorViewModel;
            
        }
        public static void ResizeCanvas(int radius, int lenght)
        {
            if (radius * 100 + plussesH > _preProcessorViewModel.CanvasActualHenght)
            {
                if (double.IsNaN(_preProcessorViewModel.CanvasHenght))
                {
                    plussesH = 120;
                    _preProcessorViewModel.CanvasHenght = radius * 100 + plussesH;//вроде норм но проблемы с размерностью искать тут
                }
                else
                    _preProcessorViewModel.CanvasHenght = radius * 100 + plussesH;
            }
            if (_preProcessorViewModel.PlussesWihth + plussesW > _preProcessorViewModel.CanvasActualLenhgt)
            {
                if (double.IsNaN(_preProcessorViewModel.CanvasLenhgt))
                {
                    plussesW = 100;
                    _preProcessorViewModel.CanvasLenhgt = -(_preProcessorViewModel.CanvasActualLenhgt - (_preProcessorViewModel.PlussesWihth)) + plussesW + _preProcessorViewModel.CanvasActualLenhgt;
                
                }
                else
                    _preProcessorViewModel.CanvasLenhgt += lenght * 100;
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
            _preProcessorViewModel.Nodes.CollectionChanged -= _preProcessorViewModel.Draw;
            _preProcessorViewModel.Nodes.Clear();
            _preProcessorViewModel.Shapes.Clear();
            _preProcessorViewModel.SupportCount = 0;

            _preProcessorViewModel.Shapes.CollectionChanged += _preProcessorViewModel.Draw;
            _preProcessorViewModel.Nodes.CollectionChanged += _preProcessorViewModel.Draw;
        }

    }
}
