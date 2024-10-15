using Microsoft.Win32;
using sapr.Models;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.IO;

namespace sapr.Command.PreProcessorCommands
{
    public class SaveCommand : PreProcessorComandBase
    {
        public SaveCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            List<string> JsonShapes = new List<string>();
            List<string> JsonNodes = new List<string>();
            List<string> JsonSupport = new List<string>();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            foreach (var node in _preProcessorViewModel.Nodes)
            {
                JsonNodes.Add(System.Text.Json.JsonSerializer.Serialize<NodeModel>(node));
            }
            foreach (var shape in _preProcessorViewModel.Shapes)
            {
                JsonShapes.Add(System.Text.Json.JsonSerializer.Serialize(shape, options));
            }
            int X = 0;
            int Y = 0;
            if (_preProcessorViewModel.LeftSmth)
                X = 1;
            if (_preProcessorViewModel.RightSmth)
                Y = 1;
            MyPoint sth = new MyPoint(X, Y);
            JsonSupport.Add(System.Text.Json.JsonSerializer.Serialize(sth, options));

            var dialog = new SaveFileDialog();
            dialog.FileName = "Shapes";
            dialog.DefaultExt = ".json";
            dialog.Filter = "Json documents (.json)|*.json";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllLines(dialog.FileName, JsonNodes);
                File.AppendAllLines(dialog.FileName, JsonShapes);
                File.AppendAllLines(dialog.FileName, JsonSupport);
            }
        }
    }
}
