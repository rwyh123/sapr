using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace sapr.Command
{
    public class CommandBase : ICommand
    {
        public virtual event EventHandler CanExecuteChanged;
        
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
        }
    }
}
