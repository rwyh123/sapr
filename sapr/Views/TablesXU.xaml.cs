using sapr.Command;
using sapr.Stores;
using sapr.Stores.ProcessorStores;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sapr.Views
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    
    public partial class UserControl1 : UserControl
    {
        private static ProcecssorViewModel viewModel = new ProcecssorViewModel();
        public UserControl1()
        {
            InitializeComponent();
            viewModel = new ProcecssorViewModel();
            this.DataContext = viewModel ;
            viewModel = this.DataContext as ProcecssorViewModel;
            PreProcessorCalculationsCommand.calculated += () =>
            {
                CreateTables();
            };
        }

        private void CreateTables()
        {


            for (int i = 0; i < SuportStore.Instance.GetUserData().Count; i++)
            {
                List<ProcrssorTables> list = new List<ProcrssorTables>();

                for (int j = 0; j <= SuportStore.Instance.GetUserData()[i].Model.Width * 100; j += StepStore.Instance.GetUserData())
                {
                    ProcrssorTables table = new ProcrssorTables(i, j, PreProcessorCalculationsCommand.CalculateNX(i, j),
                                            PreProcessorCalculationsCommand.CalculateUX(i, j), SuportStore.Instance.GetUserData()[i].AdmissibleStress);
                    list.Add(table);
                }
                DataGrid dataGrid = new DataGrid();
                dataGrid.CanUserAddRows = false;
                dataGrid.ItemsSource = list;
                dataGrid.IsReadOnly = true;
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Support",
                    Binding = new Binding("Support")
                });
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Step",
                    Binding = new Binding("Step")
                });
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Nx",
                    Binding = new Binding("Nx")
                });
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Ux",
                    Binding = new Binding("Ux")
                });
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Stress",
                    Binding = new Binding("Stress")
                });
                viewModel.DataGrids.Add(dataGrid);
            }
        }
    }
}
