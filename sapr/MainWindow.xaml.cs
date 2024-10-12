using Microsoft.Win32;
using sapr.Models;
using sapr.Stores;
using sapr.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace sapr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// ПОПРОБОВАТЬ ЦЕНТРИРОВАТЬ ЗА СЧЕТ ТОГО ЧТО Я УВЕЛИЧУ КАНВАЧ В ПОЛОРА РАЗА 
    /// че то там с опопрами храить в полях мб
    public partial class MainWindow : Window
    {
        public ObservableCollection<SupportModelv2> shapes = new ObservableCollection<SupportModelv2>();
        private ObservableCollection<NodeModel> Nodes = new ObservableCollection<NodeModel>();
        private int SupportCount = 0;
        private int PlussesWihth = 0;
        private int e;
        private SuportStore store = SuportStore.Instance;
        private SmthStore smthStore = SmthStore.Instance;
        private NodesStore nodesStore = NodesStore.Instance;
        private EPowerStore ePowerStore = EPowerStore.Instance;
        private bool leftSmth;
        private bool rightSmth;

        public int E
        {
            get { return e; }
            set { e = value; }
        }

        public bool LeftSmth { get => leftSmth; set => leftSmth = value; }
        public bool RightSmth { get => rightSmth; set => rightSmth = value; }

        public MainWindow()
        {
            InitializeComponent();
            this.SupportTable.ItemsSource = shapes; 
            this.SupportTable.CanUserAddRows = false;
            this.NodeTable.ItemsSource = Nodes;
            this.NodeTable.CanUserAddRows = false;
            shapes.CollectionChanged += Draw;
            Nodes.CollectionChanged += Draw;
            this.WorkSpase.SizeChanged += Draw;
            DataContext = this;
            E = 1;

        }
        private Path GenerateLeftorder(Rect rect)
        {
            //  !!1.Переделать создание опоры, не ноая фунция а переоворот получившейся первой 
            //сдесь, карочче разобратсья чтобы длиина и высота штучек завичсела там от размеров ректайнгла.
            int ParticulWigth = 10;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(ParticulWigth, 0);
            line.EndPoint = new Point(ParticulWigth, rect.Height);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);
            for (int i = 0;i < rect.Height;)
            {
                line = new LineGeometry();
                line.StartPoint = new Point(0, i);
                line.EndPoint = new Point(ParticulWigth, i + ParticulWigth);
                group.Children.Add(line);
                i += ParticulWigth;
            }
            path.Data = group;
            path.MaxHeight = rect.Height;
            path.MaxWidth = ParticulWigth;
            return path;
        }
        private Path GenerateRighttorder(Rect rect)
        {
            int ParticulWigth = 10;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(0, 0);
            line.EndPoint = new Point(0, rect.Height);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);
            for (int i = 0; i < rect.Height;)
            {
                line = new LineGeometry();
                line.StartPoint = new Point(ParticulWigth, i);
                line.EndPoint = new Point(0, i + ParticulWigth);
                group.Children.Add(line);
                i += ParticulWigth;
            }
            path.Data = group;
            path.MaxHeight = rect.Height;
            path.MaxWidth = ParticulWigth;
            return path;
        }

        private Path GenerateArrow(Rect rect)
        {
            int ParticulWigth = 10;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(0, ParticulWigth);
            line.EndPoint = new Point(rect.Width, ParticulWigth);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);
            for (int i = 0; i < rect.Width;)
            {
                line = new LineGeometry();
                line.StartPoint = new Point(i, ParticulWigth);
                line.EndPoint = new Point(ParticulWigth + i, 0);
                group.Children.Add(line);
                line = new LineGeometry();
                line.StartPoint = new Point(i, ParticulWigth);
                line.EndPoint = new Point(ParticulWigth + i, ParticulWigth * 2);
                group.Children.Add(line);
                i += ParticulWigth;
            }
            path.Data = group;
            path.MaxWidth = rect.Width;
            path.MaxHeight = ParticulWigth * 2;
            return path;
        }

        private Path GenereteShortArrow(Rect rect)
        {
            int ParticulWigth = 10;
            Path path = new Path();
            path.StrokeThickness = 2;
            RectangleGeometry recta = new RectangleGeometry();
            recta.Rect = new Rect();
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(0, ParticulWigth);
            line.EndPoint = new Point(rect.Width / 4, ParticulWigth);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);

            line = new LineGeometry();
            line.StartPoint = new Point(rect.Width / 4, ParticulWigth);
            line.EndPoint = new Point(rect.Width / 4 - ParticulWigth, 0);
            group.Children.Add(line);

            line = new LineGeometry();
            line.StartPoint = new Point(rect.Width / 4, ParticulWigth);
            line.EndPoint = new Point(rect.Width / 4 - ParticulWigth, ParticulWigth * 2);
            group.Children.Add(line);

            path.Data = group;
            path.MaxWidth = rect.Width;
            path.MaxHeight = ParticulWigth * 2;
            return path;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            if(window.ShowDialog() == true)
            {
                if (SupportCount == 0)
                    Nodes.Add(new NodeModel(0, SupportCount + 1));
                SupportCount++;
                Nodes.Add(new NodeModel(0, SupportCount + 1));
                shapes.Add(new SupportModelv2(0, 0, new Rect(0, 0, int.Parse(window.Lenght), int.Parse(window.Radius)), SupportCount));
                ResizeCanvas(int.Parse(window.Radius), int.Parse(window.Lenght));
                scrollbar.UpdateLayout();
                scrollbar.ScrollToVerticalOffset(scrollbar.ScrollableHeight / 2);
                scrollbar.ScrollToHorizontalOffset(scrollbar.ScrollableWidth / 2);

            }

        }

        private void ResizeCanvas(int radius, int lenght)
        {
            if (radius > WorkSpase.ActualHeight)
            { 
                if(double.IsNaN(WorkSpase.Height))
                WorkSpase.Height = radius + 120;//вроде норм но проблемы с размерностью искать тут
                else
                WorkSpase.Height += radius - WorkSpase.ActualHeight;

            }
            if (lenght + WorkSpase.ActualWidth > WorkSpase.ActualWidth)
            {
                if (double.IsNaN(WorkSpase.Width))
                WorkSpase.Width = lenght + 100;
                else
                WorkSpase.Width += lenght;

            }
        }

        private void ReFillNodesTable(int id)
        {
            //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
            Nodes.Remove(Nodes[Nodes.Count - 1]);
            for (int i = id; i <= SupportCount; i++)
            {
                Nodes[i - 1] = (new NodeModel(0, i));
            }
        }
        private void ReFillShapesUID()
        {
            int i = 1;
            //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
            foreach (SupportModelv2 supportModelv2 in shapes)
            {
                supportModelv2.UID = i++;
            }
                
        }




        private void Draw(object sender, EventArgs e)
        {
            
            //!!Сделать квадратики и кружочки на макс хигхт
            int Index = 1;
            int MaxHeight = 0;
            PlussesWihth = 0;
            WorkSpase.Children.Clear();
            foreach (SupportModelv2 sp in shapes)
            {
                if (sp.Model.Height > MaxHeight)
                    MaxHeight = (int)sp.Model.Height;

                Rectangle Lellipse = new Rectangle();
                Rectangle Rellipse = new Rectangle();
                Ellipse Nrect = new Ellipse();
                TextBlock Lnode = new TextBlock();
                TextBlock Rnode = new TextBlock();
                TextBlock Snumb = new TextBlock();
                Path Arrow = new Path();
                Path ShortArrow = new Path();
                
                Arrow = GenerateArrow(sp.Model);

                ShortArrow = GenereteShortArrow(sp.Model);

                //Размещенеи стрелки продольной нагрузки
                if (sp.PrPower < 0)
                {
                    Canvas.SetTop(Arrow, WorkSpase.ActualHeight / 2 - Arrow.MaxHeight / 2);
                    Canvas.SetLeft(Arrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                    WorkSpase.Children.Add(Arrow);
                }
                else if (sp.PrPower > 0)
                {
                    Arrow.LayoutTransform = new ScaleTransform(-1, 1);
                    Canvas.SetTop(Arrow, WorkSpase.ActualHeight / 2 - Arrow.MaxHeight / 2);
                    Canvas.SetLeft(Arrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                    WorkSpase.Children.Add(Arrow);
                }

                Nrect.Width = 20;
                Nrect.Height = 20;
                Nrect.Uid = "Nrect" + Index;
                Nrect.Stroke = Brushes.Black;
                Nrect.StrokeThickness = 1;

                Lellipse.Width = 20;
                Lellipse.Height = 20;
                Lellipse.Uid = "LElipse" + Index;
                Lellipse.Stroke = Brushes.Black;
                Lellipse.StrokeThickness = 1;

                Rellipse.Width = 20;
                Rellipse.Height = 20;
                Rellipse.Uid = "RElipse" + Index;
                Rellipse.Stroke = Brushes.Black;
                Rellipse.StrokeThickness = 1;

                Lnode.Text = (Index).ToString();
                Canvas.SetTop(Lellipse, WorkSpase.ActualHeight / 2 + MaxHeight / 2 + 30);
                Canvas.SetLeft(Lellipse, WorkSpase.ActualWidth / 2 + PlussesWihth - Lellipse.Width /2);//asdadasd
                WorkSpase.Children.Add(Lellipse);

                Canvas.SetTop(Lnode, WorkSpase.ActualHeight / 2 + MaxHeight / 2 + 30 + Lnode.FontSize / 16);
                Canvas.SetLeft(Lnode, WorkSpase.ActualWidth / 2 + PlussesWihth - Lnode.FontSize/4);
                WorkSpase.Children.Add(Lnode);

                Snumb.Text = (Index).ToString();
                Canvas.SetTop(Nrect, WorkSpase.ActualHeight / 2 - MaxHeight / 2 - 40);
                Canvas.SetLeft(Nrect, WorkSpase.ActualWidth / 2 + PlussesWihth - Nrect.Width / 2 + sp.Model.Width / 2);
                WorkSpase.Children.Add(Nrect);

                Canvas.SetTop(Snumb, WorkSpase.ActualHeight / 2 - MaxHeight / 2 - 40 + Snumb.FontSize / 16);
                Canvas.SetLeft(Snumb, WorkSpase.ActualWidth / 2 + PlussesWihth + sp.Model.Width / 2 - Snumb.FontSize / 4);
                WorkSpase.Children.Add(Snumb);

                Path path = new Path();
                GeometryGroup myGeometryGroup1 = new GeometryGroup();
                myGeometryGroup1.Children.Add(new RectangleGeometry(sp.Model));
                path.Data = myGeometryGroup1;
                Canvas.SetTop(path, WorkSpase.ActualHeight / 2 - sp.Model.Height / 2);
                Canvas.SetLeft(path, WorkSpase.ActualWidth / 2  + PlussesWihth);
                path.Stroke = Brushes.Black;
                WorkSpase.Children.Add(path);

                if (Index == 1)
                {
                    if (SupportCount == 1)
                    {
                        //стрелочка слева для первого квадратика
                        if (Nodes[Index - 1].PoPower > 0)
                        {
                            ShortArrow.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                            WorkSpase.Children.Add(ShortArrow);
                        }
                        else if (Nodes[Index - 1].PoPower < 0)
                        {
                            ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                            WorkSpase.Children.Add(ShortArrow);
                        }

                        //стрелочка справа для последнего квадратика
                        Path ShortArrow1 = new Path();
                        ShortArrow1 = GenereteShortArrow(sp.Model);
                        if (Nodes[Index].PoPower > 0)
                        {

                            ShortArrow1.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow1, WorkSpase.ActualHeight / 2 - ShortArrow1.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow1, WorkSpase.ActualWidth / 2 + sp.Model.Width - sp.Model.Width / 4);
                            WorkSpase.Children.Add(ShortArrow1);
                        }
                        else if (Nodes[Index].PoPower < 0)
                        {
                            ShortArrow1.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow1.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow1, WorkSpase.ActualHeight / 2 - ShortArrow1.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow1, WorkSpase.ActualWidth / 2 + sp.Model.Width - sp.Model.Width / 4);
                            WorkSpase.Children.Add(ShortArrow1);
                        }
                    }
                    else
                    {
                        //стрелочка слева для первого квадратика
                        if (Nodes[Index - 1].PoPower > 0)
                        {
                            ShortArrow.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                            WorkSpase.Children.Add(ShortArrow);
                        }
                        else if (Nodes[Index - 1].PoPower < 0)
                        {
                            ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                            WorkSpase.Children.Add(ShortArrow);
                        }
                    }
                }
                if (Index > 1 && Index < shapes.Count)
                {
                    //стрелочка слева для дефолт квадратика
                    if (Nodes[Index - 1].PoPower > 0)
                    {
                        ShortArrow.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                        WorkSpase.Children.Add(ShortArrow);
                    }
                    else if (Nodes[Index - 1].PoPower < 0)
                    {
                        ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth - ShortArrow.MaxWidth / 4);
                        WorkSpase.Children.Add(ShortArrow);
                    }


                }
                if (Index == shapes.Count && SupportCount > 1)
                {
                    //стрелочка слева для дефолт квадратика
                    if (Nodes[Index - 1].PoPower > 0)
                    {
                        ShortArrow.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth);
                        WorkSpase.Children.Add(ShortArrow);
                    }
                    else if (Nodes[Index - 1].PoPower < 0)
                    {
                        ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow, WorkSpase.ActualHeight / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, WorkSpase.ActualWidth / 2 + PlussesWihth - ShortArrow.MaxWidth / 4);
                        WorkSpase.Children.Add(ShortArrow);
                    }
                    //стрелочка справа для последнего квадратика
                    Path ShortArrow1 = new Path();
                    ShortArrow1 = GenereteShortArrow(sp.Model);
                    if (Nodes[Index].PoPower > 0)
                    {

                        ShortArrow1.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow1, WorkSpase.ActualHeight / 2 - ShortArrow1.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow1, WorkSpase.ActualWidth / 2 + PlussesWihth + sp.Model.Width - sp.Model.Width / 4);
                        WorkSpase.Children.Add(ShortArrow1);
                    }
                    else if (Nodes[Index].PoPower < 0)
                    {
                        ShortArrow1.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow1.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow1, WorkSpase.ActualHeight / 2 - ShortArrow1.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow1, WorkSpase.ActualWidth / 2 + PlussesWihth + sp.Model.Width - sp.Model.Width / 4);
                        WorkSpase.Children.Add(ShortArrow1);
                    }
                }

                PlussesWihth += (int)sp.Model.Width;

                if (Index == SupportCount)
                {
                    Rnode.Text = (Index + 1).ToString();
                    Canvas.SetTop(Rellipse, WorkSpase.ActualHeight / 2 + MaxHeight / 2 + 30);
                    Canvas.SetLeft(Rellipse, WorkSpase.ActualWidth / 2 + PlussesWihth - Rellipse.Width / 2);//asdadasd
                    WorkSpase.Children.Add(Rellipse);

                    Canvas.SetTop(Rnode, WorkSpase.ActualHeight / 2 + MaxHeight / 2 + 30 + Rnode.FontSize / 16);
                    Canvas.SetLeft(Rnode, WorkSpase.ActualWidth / 2 + PlussesWihth - Rnode.FontSize / 4);
                    WorkSpase.Children.Add(Rnode);
                }

                path.Uid = Index.ToString();
                Index++;
               

            }
            foreach (UIElement uIElement in WorkSpase.Children)
            {
                Canvas.SetLeft(uIElement, Canvas.GetLeft(uIElement) - PlussesWihth / 2);
            }
        }

        private void LeftCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Path myShape = new Path();
            myShape = GenerateLeftorder(shapes[0].Model);
            Canvas.SetTop(myShape, WorkSpase.ActualHeight / 2 - myShape.MaxHeight / 2);
            Canvas.SetLeft(myShape, WorkSpase.ActualWidth / 2 - 10 - PlussesWihth / 2);
            myShape.Uid = "Left";
            WorkSpase.Children.Add(myShape);
        }
        private void RightCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Path myShape = new Path();
            myShape = GenerateRighttorder(shapes[shapes.Count - 1].Model);
            Canvas.SetTop(myShape, WorkSpase.ActualHeight / 2 - myShape.MaxHeight / 2);
            Canvas.SetLeft(myShape, WorkSpase.ActualWidth / 2 + PlussesWihth / 2);
            myShape.Uid = "Right";
            WorkSpase.Children.Add(myShape);
        }

        private void RemoveElement(string RemoveUID)
        {
            //сделать что индексы опор переписываются
            List<UIElement> itemstoremove = new List<UIElement>();
            foreach (UIElement ui in WorkSpase.Children)
            {
                if (ui.Uid.StartsWith(RemoveUID))
                {
                    itemstoremove.Add(ui);
                }
            }
            foreach (UIElement ui in itemstoremove)
            {
                WorkSpase.Children.Remove(ui);
            }
        }

        private void LeftCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveElement("Left");
            Draw(new object(), new EventArgs());
        }
        private void RightCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveElement("Right");
            Draw(new object(), new EventArgs());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SupportModelv2 supportModel = new SupportModelv2();
            supportModel = this.SupportTable.SelectedItem as SupportModelv2;
            shapes.Remove(shapes.Where(x => x.UID == supportModel.UID).FirstOrDefault());
            SupportCount--;
            ReFillNodesTable(supportModel.UID);
            ReFillShapesUID();
            {
                int i = 1;
                //МБ РАБОТАТ Не ПРАВИОЬНО """!!!!!
                foreach (SupportModelv2 supportModelv2 in shapes)
                {
                    supportModelv2.UID = i++;
                }

            }
            //ЭТО КОСТЫЛЬ НАДО РАЗОБРАТСЬЯ ВРОДЕ В ФУНКЦИИ РЕВИЛ НОДЕС

            if (SupportCount == 0)
            {
                Nodes.Clear();
            }else if(SupportCount == 1)
            {
                WorkSpase.Height -= supportModel.Model.Height - 120;
                WorkSpase.Width -= supportModel.Model.Width - 100;
            }
            else
            {
                WorkSpase.Height -= supportModel.Model.Height;
                WorkSpase.Width -= supportModel.Model.Width;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Nodes.Clear();
            shapes.Clear();
            SupportCount = 0;

            List<string> list = new List<string>();
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json documents (.json)|*.json";
            NodeModel node = new NodeModel();

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                list = File.ReadAllLines(dialog.FileName).ToList();
            }
            foreach(var elm in list)
            {
                if (TryParseClass.TryParseJson(elm,out node))
                {
                    Nodes.Add(node);
                }
                else
                {
                    SupportModelv2 supportModelv2 = new SupportModelv2();
                    supportModelv2 = System.Text.Json.JsonSerializer.Deserialize<SupportModelv2>(elm);
                    shapes.Add(supportModelv2);
                    ResizeCanvas((int)supportModelv2.Model.Height, (int)supportModelv2.Model.Width);
                    SupportCount++;
                }
            }

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> JsonShapes = new List<string>();
            List<string> JsonNodes = new List<string>();
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            foreach (var node in Nodes)
            {
                JsonNodes.Add(System.Text.Json.JsonSerializer.Serialize<NodeModel>(node));
            }
            foreach (var shape in shapes)
            {
                JsonShapes.Add(System.Text.Json.JsonSerializer.Serialize(shape, options));
            }

            var dialog = new SaveFileDialog();
            dialog.FileName = "Shapes";
            dialog.DefaultExt = ".json";
            dialog.Filter = "Json documents (.json)|*.json";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllLines(dialog.FileName, JsonNodes);
                File.AppendAllLines(dialog.FileName, JsonShapes);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            store.SetUserData(shapes);
            nodesStore.SetUserData(Nodes);
            int x = 0;
            if (LeftSmth)
                x = 1;
            int y = 0;
            if (RightSmth)
                y = 1;
            smthStore.SetUserData(new Point(x, y));
            ePowerStore.SetUserData(E);
        }
    }
    
}
