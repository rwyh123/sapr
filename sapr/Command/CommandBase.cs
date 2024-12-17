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
        public static event EventHandler NeedToDraw;
        protected static void NeedToDrawExecute()
        {
            NeedToDraw?.Invoke(new object(), EventArgs.Empty);
        }
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
        }
    }
}
