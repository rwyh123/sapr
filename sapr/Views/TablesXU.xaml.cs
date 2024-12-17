using sapr.Command;
using sapr.Stores;
using sapr.Stores.ProcessorStores;
using sapr.Utilities;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    
    public partial class TablesXU : UserControl 
    {
        private static TablesViewModel viewModel = new TablesViewModel();
        public TablesXU()
        {
            InitializeComponent();
            viewModel = new TablesViewModel();
            this.DataContext = viewModel ;
            viewModel = this.DataContext as TablesViewModel;
            ProcessorCalculationsCommand.calculated += () =>
            {
                CreateTables();
            };
        }

        private void CreateTables()
        {
            List<DataGrid> tables = new List<DataGrid>();
            viewModel.DataGrids.Clear();
            for (int i = 0; i < SuportStore.Instance.GetUserData().Count; i++)
            {
                List<ProcrssorTables> list = new List<ProcrssorTables>();

                for (double j = 0; j <= SuportStore.Instance.GetUserData()[i].Model.Width * 100; j += StepStore.Instance.GetUserData() * 100 )
                {
                    ProcrssorTables table = new ProcrssorTables(i+1, j / 100, ProcessorCalculationsCommand.CalculateNX(i, j / 100),
                                            ProcessorCalculationsCommand.CalculateUX(i, j / 100), ProcessorCalculationsCommand.CalculateDX(i, ProcessorCalculationsCommand.CalculateNX(i, j / 100)), SuportStore.Instance.GetUserData()[i].AdmissibleStress);
                    list.Add(table);
                }

                DataGrid dataGrid = new DataGrid
                {
                    CanUserAddRows = false,
                    ItemsSource = list,
                    IsReadOnly = true,
                    AutoGenerateColumns = false
                };
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Номер Стержня",
                    Binding = new Binding("Support")
                });
                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "x",
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

                DataGridTextColumn dxColumn = new DataGridTextColumn()
                {
                    Header = "Dx",
                    Binding = new Binding("Dx")
                };

                Style cellStyle = new Style(typeof(DataGridCell));
                DataTrigger trigger = new DataTrigger
                {
                    Binding = new Binding("Dx")
                    {
                        Converter = new ComparisonConverter(),
                        ConverterParameter = list[i].Stress 
                    },
                    Value = true
                };

                trigger.Setters.Add(new Setter(DataGridCell.ForegroundProperty, Brushes.Red));
                cellStyle.Triggers.Add(trigger);

                dxColumn.CellStyle = cellStyle;

                dataGrid.Columns.Add(dxColumn);

                dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Напряжение",
                    Binding = new Binding("Stress"),
                });


                viewModel.DataGrids.Add(dataGrid);
                tables.Add(dataGrid);
            }
            ReportTableStore.Instance.SetUserData(tables);
            
        }
    }
}
