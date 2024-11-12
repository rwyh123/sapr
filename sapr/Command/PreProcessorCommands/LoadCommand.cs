using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sapr.Models;
using sapr.Properties;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sapr.Command.PreProcessorCommands
{
    public class LoadCommand : PreProcessorComandBase
    {
        public LoadCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            Clear();
            List<string> list = new List<string>();
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json documents (.json)|*.json";
            NodeModel node = new NodeModel();
            SupportModelv2 support = new SupportModelv2();

            bool? result = dialog.ShowDialog();

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new RectangleConverter());
            _preProcessorViewModel.IsProcessorCalculated = false;


            if (result == true)
            {
                list = File.ReadAllLines(dialog.FileName).ToList();
            }
            foreach (var elm in list)
            {
                SupportModelv2 sp = new SupportModelv2();
                sp = JsonConvert.DeserializeObject<SupportModelv2>(elm, settings);
                if (JsonConvert.DeserializeObject<NodeModel>(elm).NodeNumber != 0)
                {
                    _preProcessorViewModel.Nodes.Add(JsonConvert.DeserializeObject<NodeModel>(elm));
                }
                else if (sp.Model != null)
                {
                    _preProcessorViewModel.Shapes.CollectionChanged -= _preProcessorViewModel.Draw;
                    _preProcessorViewModel.IsProcessorCalculated = false;
                    _preProcessorViewModel.Shapes.Add(sp);
                    _preProcessorViewModel.Shapes.CollectionChanged += _preProcessorViewModel.Draw;
                    ResizeCanvas((int)sp.Model.Height, (int)sp.Model.Width);
                    _preProcessorViewModel.SupportCount++;
                }
                else
                {
                    MyPoint myPoint = new MyPoint();
                    myPoint = JsonConvert.DeserializeObject<MyPoint>(elm);
                    if (myPoint.X == 1)
                        _preProcessorViewModel.LeftSmth = true;
                    else
                        _preProcessorViewModel.LeftSmth = false;
                    if (myPoint.Y == 1)
                        _preProcessorViewModel.RightSmth = true;
                    else
                        _preProcessorViewModel.RightSmth = false;
                }
            }
            _preProcessorViewModel.OnPropertyChanged(nameof(_preProcessorViewModel.IsSupportCountNotull));
        }

       
    }
}
