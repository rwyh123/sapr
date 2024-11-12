using sapr.Stores;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace sapr.Command.PreProcessorCommands
{
    public class CalculateCommand : PreProcessorComandBase
    {
        public CalculateCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            _preProcessorViewModel.store.SetUserData(_preProcessorViewModel.Shapes);
            _preProcessorViewModel.nodesStore.SetUserData(_preProcessorViewModel.Nodes);
            _preProcessorViewModel.smthStore.SetUserData(_preProcessorViewModel.LeftSmth, _preProcessorViewModel.RightSmth);
            _preProcessorViewModel.ePowerStore.SetUserData(_preProcessorViewModel.E);
            _preProcessorViewModel.IsProcessorCalculated = false;
        }
    }
}
