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
using System.Windows.Shapes;

namespace sapr.Utilities
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public string Lenght
        {
            get { return this.lenght.Text; }
        }
        public string Radius
        {
            get { return this.radius.Text; }
        }
        private void Chancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Длина")
            {
                if (double.TryParse(((TextBox)e.EditingElement).Text, out double newValue))
                {
                    if (newValue < 0)
                    {
                        MessageBox.Show("Длина доложна быть не меньше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        e.Cancel = true; // Отменяет изменение
                    }
                }
            }
            else if (e.Column.Header.ToString() == "Сечение")
            {
                if (double.TryParse(((TextBox)e.EditingElement).Text, out double newValue))
                {
                    if (newValue < 0)
                    {
                        MessageBox.Show("Сечение доложно быть не меньше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
