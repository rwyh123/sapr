using sapr.Command;
using sapr.Command.PreProcessorCommands;
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
    /// Логика взаимодействия для PreProcessorView.xaml
    /// </summary>
    public partial class PreProcessorView : UserControl
    {
        private static PreProcessorViewModel viewModel = new PreProcessorViewModel();
        public PreProcessorView()
        {
            InitializeComponent();
            viewModel = new PreProcessorViewModel();
            this.DataContext = viewModel;
            viewModel = this.DataContext as PreProcessorViewModel;
            
            viewModel.RequestScrollBarUpdate += () =>
            {
                scrollbar.UpdateLayout();
            };
            SupportTable.CurrentCellChanged += viewModel.Draw;
            WorkSpase.SizeChanged += viewModel.Draw;

        }
        private void Resize(object sender, EventArgs e)
        {
            UpdateCanvasSize();
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCanvasSize();
            viewModel.CanvasHenght = WorkSpase.ActualHeight;
            viewModel.CanvasLenhgt = WorkSpase.ActualWidth;
        }
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCanvasSize();
        }
        private void UpdateCanvasSize()
        {
            if (viewModel != null)
            {
                viewModel.CanvasActualLenhgt = WorkSpase.ActualWidth;
                viewModel.CanvasActualHenght = WorkSpase.ActualHeight;
            }
        }

        private void WorkSpase_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rect = new Rectangle();
            
            if(e.OriginalSource is Rectangle && viewModel.IsProcessorCalculated)
            {
                rect = (Rectangle)e.OriginalSource;
                Point pos = e.GetPosition(e.OriginalSource as Rectangle);
                int id = 0;
                if(int.TryParse(rect.Uid, out id))
                {
                    viewModel.CurentSup = id;
                    viewModel.NX = PreProcessorCalculationsCommand.CalculateNX(id - 1, pos.X / 100);
                    viewModel.UX = PreProcessorCalculationsCommand.CalculateUX(id - 1, pos.X / 100);

                }

            }
            
            



        }
    }

}
