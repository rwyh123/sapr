using sapr.Stores;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Command.PreProcessorCommands
{
    public class ClearCommand : PreProcessorComandBase
    {
        public ClearCommand(PreProcessorViewModel preProcessorViewModel) : base(preProcessorViewModel)
        {
        }
        public override void Execute(object parameter)
        {
            Clear();
            _preProcessorViewModel.Draw(this, new EventArgs());
        }

    }
}
