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
            viewModel = new PreProcessorViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
            viewModel.RequestScrollBarUpdate += () =>
            {
                // Предположим, что ScrollViewer находится на вашей странице
                scrollbar.UpdateLayout();
            };
            viewModel = this.DataContext as PreProcessorViewModel;
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
    }

}
