using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using sapr.Command;
using sapr.Command.PreProcessorCommands;
using sapr.Stores.ProcessorStores;
using sapr.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
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
                if(int.TryParse(rect.Uid, out id) && viewModel.IsProcessorCalculated)
                {
                    viewModel.CurentSup = id;
                    viewModel.NX = ProcessorCalculationsCommand.CalculateNX(id - 1, pos.X / 100);
                    viewModel.UX = ProcessorCalculationsCommand.CalculateUX(id - 1, pos.X / 100);
                    viewModel.DX = ProcessorCalculationsCommand.CalculateDX(id - 1, ProcessorCalculationsCommand.CalculateNX(id - 1, pos.X / 100));

                }

            }
            
            



        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Report";
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "Pdf documents (.pdf)|*.pdf";

            bool? result = dialog.ShowDialog();
            var Tables = new List<DataGrid>();
            Tables.Add(SupportTable);
            Tables.Add(NodeTable);

            if (result == true)
            {
                using (var document = new PdfDocument())
                {
                    var page1 = document.AddPage();
                    var graphics1 = XGraphics.FromPdfPage(page1);
                    var font = new XFont("Verdana", 10);
                    double yOffset = 20;
                    const double xOffset = 20;
                    const double rowHeight = 20;
                    const double columnWidth = 100;
                    const double gridSpacing = 30;

                    foreach (var dataGrid in Tables)
                    {
                        if (yOffset + rowHeight > page1.Height.Point) // Проверка места на странице
                        {
                            page1 = document.AddPage();
                            graphics1 = XGraphics.FromPdfPage(page1);
                            yOffset = 20;
                        }

                        double headerXOffset = xOffset;

                        // Рисуем заголовки столбцов с рамками
                        foreach (var column in dataGrid.Columns)
                        {
                            var rect = new XRect(headerXOffset, yOffset, columnWidth, rowHeight);
                            graphics1.DrawRectangle(XPens.Black, rect); // Рамка
                            graphics1.DrawString(column.Header.ToString(), font, XBrushes.Black, rect, XStringFormats.Center);
                            headerXOffset += columnWidth;
                        }
                        yOffset += rowHeight;

                        // Отображаем строки данных
                        foreach (var item in dataGrid.Items)
                        {
                            var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                            if (row != null)
                            {
                                double cellOffsetX = xOffset;
                                foreach (var column in dataGrid.Columns)
                                {
                                    var cellContent = column.GetCellContent(row) as TextBlock;
                                    var cellRect = new XRect(cellOffsetX, yOffset, columnWidth, rowHeight);

                                    // Получаем цвет текста ячейки
                                    var textColor = cellContent?.Foreground as SolidColorBrush;
                                    var xColor = textColor != null ? XColor.FromArgb(textColor.Color.A, textColor.Color.R, textColor.Color.G, textColor.Color.B) : XColors.Black;
                                    var xBrush = new XSolidBrush(xColor);

                                    // Рисуем рамку и текст ячейки с цветом
                                    graphics1.DrawRectangle(XPens.Black, cellRect);
                                    graphics1.DrawString(cellContent?.Text ?? "", font, xBrush, cellRect, XStringFormats.Center);

                                    cellOffsetX += columnWidth;
                                }
                                yOffset += rowHeight;
                            }
                        }

                        yOffset += gridSpacing;
                    }

                    var page2 = document.AddPage();
                    var graphics2 = XGraphics.FromPdfPage(page2);

                    double canvasWidth = WorkSpase.Width;
                    double canvasHeight = WorkSpase.Height;

                    // Рендерим Canvas в RenderTargetBitmap с размерами страницы PDF
                    var renderBitmap = new RenderTargetBitmap((int)canvasWidth, (int)canvasHeight, 96, 96, PixelFormats.Pbgra32);
                    renderBitmap.Render(WorkSpase);

                    // Конвертируем RenderTargetBitmap в изображение и вставляем его в PDF
                    var pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    using (var stream = new MemoryStream())
                    {
                        pngEncoder.Save(stream);
                        var image = XImage.FromStream(stream);
                        graphics2.DrawImage(image, 0, 0, page2.Width, page2.Height);
                    }


                    var page3 = document.AddPage();
                    var graphics3 = XGraphics.FromPdfPage(page3);
                    var font3 = new XFont("Verdana", 10);
                    double yOffset3 = 20;
                    const double xOffset3 = 0;
                    const double rowHeight3 = 20;
                    const double columnWidth3 = 100;
                    const double gridSpacing3 = 5;

                    foreach (var dataGrid in ReportTableStore.Instance.GetUserData())
                    {
                        if (yOffset3 + rowHeight3 > page3.Height.Point) // Проверка места на странице
                        {
                            page3 = document.AddPage();
                            graphics3 = XGraphics.FromPdfPage(page3);
                            yOffset3 = 20;
                        }

                        double headerxOffset3 = xOffset3;

                        // Рисуем заголовки столбцов с рамками
                        foreach (var column in dataGrid.Columns)
                        {
                            var rect = new XRect(headerxOffset3, yOffset3, columnWidth3, rowHeight3);
                            graphics3.DrawRectangle(XPens.Black, rect); // Рамка
                            graphics3.DrawString(column.Header.ToString(), font3, XBrushes.Black, rect, XStringFormats.Center);
                            headerxOffset3 += columnWidth3;
                        }
                        yOffset3 += rowHeight3;

                        // Отображаем строки данных
                        foreach (var item in dataGrid.Items)
                        {
                            var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                            if (row != null)
                            {
                                double cellOffsetX = xOffset3;
                                foreach (var column in dataGrid.Columns)
                                {
                                    var cellContent = column.GetCellContent(row) as TextBlock;
                                    var cellRect = new XRect(cellOffsetX, yOffset3, columnWidth3, rowHeight3);

                                    // Получаем цвет текста ячейки
                                    var textColor = cellContent?.Foreground as SolidColorBrush;
                                    var xColor = textColor != null ? XColor.FromArgb(textColor.Color.A, textColor.Color.R, textColor.Color.G, textColor.Color.B) : XColors.Black;
                                    var xBrush = new XSolidBrush(xColor);

                                    // Рисуем рамку и текст ячейки с цветом
                                    graphics3.DrawRectangle(XPens.Black, cellRect);
                                    graphics3.DrawString(cellContent?.Text ?? "", font3, xBrush, cellRect, XStringFormats.Center);

                                    cellOffsetX += columnWidth3;
                                }
                                yOffset3 += rowHeight3;
                            }
                        }

                        yOffset3 += gridSpacing3;
                    }
                    document.Save(dialog.FileName);
                }
            }
        }
    }


}
