using sapr.Models;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public  event EventHandler resize;
        private static List<EventHandler> subscribers = new List<EventHandler>();
        public static void Subscribe(EventHandler eventHandler)
        {
            subscribers.Add(eventHandler);
        }
        public static void UnSubscribe(EventHandler eventHandler)
        {
            subscribers.Remove(eventHandler);
        }
        private static void RiseEvent()
        {
            for(int i = subscribers.Count()-1; i >= 0;i--)
            {
                subscribers[i]?.Invoke(new Object(), EventArgs.Empty);
            }
        }
        
        private static int plussesH = 0;
        private static int plussesW = 0;
        static public PreProcessorViewModel _preProcessorViewModel { get; set; }
        public PreProcessorComandBase(PreProcessorViewModel preProcessorViewModel)
        {
            _preProcessorViewModel = preProcessorViewModel;
            
        }
        protected static void ResizeCanvas(int radius, int lenght)
        {
            if (radius + plussesH > _preProcessorViewModel.CanvasActualHenght)
            {
                if (double.IsNaN(_preProcessorViewModel.CanvasHenght))
                {
                    plussesH = 120;
                    _preProcessorViewModel.CanvasHenght = radius + plussesH;//вроде норм но проблемы с размерностью искать тут
                }
                else
                    _preProcessorViewModel.CanvasHenght = radius + plussesH;
                //RiseEvent();
            }
            if (_preProcessorViewModel.PlussesWihth + plussesW > _preProcessorViewModel.CanvasActualLenhgt)
            {
                if (double.IsNaN(_preProcessorViewModel.CanvasLenhgt))
                {
                    plussesW = 100;
                    _preProcessorViewModel.CanvasLenhgt = -(_preProcessorViewModel.CanvasActualLenhgt - (_preProcessorViewModel.PlussesWihth)) + plussesW + _preProcessorViewModel.CanvasActualLenhgt;
                
                }
                else
                    _preProcessorViewModel.CanvasLenhgt += lenght;
                RiseEvent();
            }

        }
        protected void ReFillNodesTable(int id)
        {
            //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
            _preProcessorViewModel.Nodes.Remove(_preProcessorViewModel.Nodes[_preProcessorViewModel.Nodes.Count - 1]);
            for (int i = id; i <= _preProcessorViewModel.SupportCount; i++)
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
                supportModelv2.UID = i++;
            }

        }
    
    }
}
