using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using sapr.Models;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _preProcessorViewModel.Shapes.CollectionChanged -= _preProcessorViewModel.Draw;
            _preProcessorViewModel.Nodes.CollectionChanged -= _preProcessorViewModel.Draw;
            _preProcessorViewModel.Nodes.Clear();
            _preProcessorViewModel.Shapes.Clear();
            _preProcessorViewModel.SupportCount = 0;
            _preProcessorViewModel.Shapes.CollectionChanged += _preProcessorViewModel.Draw;
            _preProcessorViewModel.Nodes.CollectionChanged += _preProcessorViewModel.Draw;

            List<string> list = new List<string>();
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json documents (.json)|*.json";
            NodeModel node = new NodeModel();
            SupportModelv2 support = new SupportModelv2();

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                list = File.ReadAllLines(dialog.FileName).ToList();
            }
            foreach (var elm in list)
            {
                if (TryParseClass.TryParseJson(elm, out node))
                {
                    _preProcessorViewModel.Nodes.Add(node);
                }
                else if (TryParseClass.TryParseJson(elm, out support))
                {
                    _preProcessorViewModel.Shapes.Add(support);
                    ResizeCanvas((int)support.Model.Height, (int)support.Model.Width);
                    _preProcessorViewModel.SupportCount++;
                }
                else
                {
                    MyPoint myPoint = new MyPoint();
                    myPoint = System.Text.Json.JsonSerializer.Deserialize<MyPoint>(elm);
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
            _preProcessorViewModel.OnPropertyChanged(nameof(_preProcessorViewModel.CeckBoxIsEnabled));
            _preProcessorViewModel.OnPropertyChanged(nameof(_preProcessorViewModel.LeftSmth));
            _preProcessorViewModel.OnPropertyChanged(nameof(_preProcessorViewModel.RightSmth));

        }
    }
}
