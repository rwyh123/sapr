using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sapr.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        private ObservableCollection<DataGrid> dataGrids;

        public ObservableCollection<DataGrid> DataGrids
        {
            get { return dataGrids; }
            set
            {
                dataGrids = value;
                OnPropertyChanged(nameof(DataGrids));
            }
        }
        public TablesViewModel()
        {
            DataGrids = new ObservableCollection<DataGrid>();
        }
    }
}
