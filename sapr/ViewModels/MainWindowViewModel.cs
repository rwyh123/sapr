using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
		private bool postProcessorEnabled = false;

		public bool PostProcessorEnabled
        {
			get { return postProcessorEnabled; }
			set 
			{
				postProcessorEnabled = value;
				OnPropertyChanged(nameof(PostProcessorEnabled));
			}
		}
		public MainWindowViewModel()
		{
			PreProcessorViewModel.RediToCalculate += ChangeState;
        }
		private void ChangeState(object sender, EventArgs e)
		{
			PostProcessorEnabled = (bool)sender;
        }

	}
}
