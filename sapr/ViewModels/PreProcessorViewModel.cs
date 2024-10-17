using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using sapr.Command.PreProcessorCommands;
using sapr.Models;
using sapr.Stores;
using sapr.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace sapr.ViewModels
{
    public class PreProcessorViewModel : ViewModelBase
    {
        //worksVar
        private int supportCount = 0;
        public int PlussesWihth = 0;
        public SuportStore store = SuportStore.Instance;
        public SmthStore smthStore = SmthStore.Instance;
        public NodesStore nodesStore = NodesStore.Instance;
        public EPowerStore ePowerStore = EPowerStore.Instance;
        private static double canvasActualHeight;
        private static double canvasActualLenght;
        private double canvasHeight = double.NaN; 
        private double canvasLenght = double.NaN; 
        private NodeModel selectedNode;
        private SupportModelv2 selectedShape;
        private double scrollBarVerticalOfset;
        private double scrollBarHorizontalOfset;
        private bool isSupportCountNotull;
        private static bool isProcessorCalculated;

        //props
        private ObservableCollection<SupportModelv2> shapes = new ObservableCollection<SupportModelv2>();
        private ObservableCollection<NodeModel> nodes = new ObservableCollection<NodeModel>();
        private ObservableCollection<UIElement> canvasChildrens;
        private int e;
        private bool leftSmth;
        private bool rightSmth;

        //events
        public event Action RequestScrollBarUpdate;

        //prop enit
        public SupportModelv2 SelectedShape
        {
            get { return selectedShape; }
            set
            {
                selectedShape = value;
                OnPropertyChanged(nameof(SelectedShape));
            }
        }
        public NodeModel SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                OnPropertyChanged(nameof(SelectedNode));
            }
        }
        public  double CanvasActualLenhgt
        {
            get { return canvasActualLenght; }
            set
            {
                canvasActualLenght = value;
                OnPropertyChanged(nameof(CanvasActualLenhgt));
            }
        }
        public double CanvasActualHenght
        {
            get { return canvasActualHeight; }
            set
            {
                canvasActualHeight = value;
                OnPropertyChanged(nameof(CanvasActualHenght));
            }
        }
        public double CanvasLenhgt
        {
            get { return canvasLenght; }
            set
            {
                canvasLenght = value;
                OnPropertyChanged(nameof(CanvasLenhgt));
                RequestScrollBarUpdate?.Invoke();
                
            }
        }
        public double CanvasHenght
        {
            get { return canvasHeight; }
            set
            {
                canvasHeight = value;
                OnPropertyChanged(nameof(CanvasHenght));
                RequestScrollBarUpdate?.Invoke();
            }
        }
        public ObservableCollection<UIElement> CanvasChildrens
        {
            get { return canvasChildrens; }
            set
            {
                canvasChildrens = value;
                OnPropertyChanged(nameof(CanvasChildrens));
            }
        }
        public int E
        {
            get { return e; }
            set
            {
                e = value;
                OnPropertyChanged(nameof(E));
            }
        }
        public bool LeftSmth
        {
            get { return leftSmth; }
            set
            {
                leftSmth = value;
                OnPropertyChanged(nameof(LeftSmth));
                Draw(this, new EventArgs());
            }
        }
        public bool RightSmth
        {
            get => rightSmth;
            set
            {
                rightSmth = value;
                OnPropertyChanged(nameof(RightSmth));
                Draw(this, new EventArgs());
            }
        }
        public double ScrollBarHorizontalOfset
        {
            get { return scrollBarHorizontalOfset; }
            set
            {
                scrollBarHorizontalOfset = value;
                OnPropertyChanged(nameof(ScrollBarHorizontalOfset));
            }
        }
        public double ScrollBarVerticalOfset
        {
            get { return scrollBarVerticalOfset; }
            set
            {
                scrollBarVerticalOfset = value;
                OnPropertyChanged(nameof(ScrollBarVerticalOfset));
            }
        }
        public bool IsSupportCountNotull
        {
            get
            {
                return IsCountNotnull();
            }
            set { isSupportCountNotull = value; }
        }
        public int SupportCount
        {
            get => supportCount;
            set
            {
                supportCount = value;
                OnPropertyChanged(nameof(SupportCount));
                OnPropertyChanged(nameof(IsSupportCountNotull));
            }
        }
        public bool IsProcessorCalculated
        {
            get { return isProcessorCalculated; }
            set
            {
                isProcessorCalculated = value;
                OnPropertyChanged(nameof(IsProcessorCalculated));
            }
        }
        public ObservableCollection<SupportModelv2> Shapes
        {
            get => shapes;
            set
            {
                shapes = value;
                OnPropertyChanged(nameof(Shapes));
            }
        }
        public ObservableCollection<NodeModel> Nodes
        {
            get => nodes;
            set
            {
                nodes = value;
                OnPropertyChanged(nameof(Nodes));
            }
        }

        //cinstruct
        private Path GenerateBorder(Rectangle rect)
        {
            //  !!1.Переделать создание опоры, не ноая фунция а переоворот получившейся первой 
            //сдесь, карочче разобратсья чтобы длиина и высота штучек завичсела там от размеров ректайнгла.
            int ParticulWigth = 10;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(ParticulWigth, 0);
            line.EndPoint = new Point(ParticulWigth, rect.Height * 100);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);
            for (int i = 0; i < rect.Height * 100;)
            {
                line = new LineGeometry();
                line.StartPoint = new Point(0, i);
                line.EndPoint = new Point(ParticulWigth, i + ParticulWigth);
                group.Children.Add(line);
                i += ParticulWigth;
            }
            path.Data = group;
            path.MaxHeight = rect.Height * 100;
            path.MaxWidth = ParticulWigth;
            return path;
        }
        private Path GenerateArrow(Rectangle rect)
        {
            int ParticulWigth = 10;
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(0, ParticulWigth);
            line.EndPoint = new Point(rect.Width * 100, ParticulWigth);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);
            for (int i = 0; i < rect.Width * 100;)
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
            path.MaxWidth = rect.Width * 100;
            path.MaxHeight = ParticulWigth * 2;
            return path;
        }
        private Path GenereteShortArrow(Rectangle rect)
        {
            int ParticulWigth = 10;
            Path path = new Path();
            path.StrokeThickness = 2;
            RectangleGeometry recta = new RectangleGeometry();
            recta.Rect = new Rect();
            LineGeometry line = new LineGeometry();
            line.StartPoint = new Point(0, ParticulWigth);
            line.EndPoint = new Point(rect.Width * 100 / 4, ParticulWigth);
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(line);

            line = new LineGeometry();
            line.StartPoint = new Point(rect.Width * 100 / 4, ParticulWigth);
            line.EndPoint = new Point(rect.Width * 100 / 4 - ParticulWigth, 0);
            group.Children.Add(line);

            line = new LineGeometry();
            line.StartPoint = new Point(rect.Width * 100 / 4, ParticulWigth);
            line.EndPoint = new Point(rect.Width * 100 / 4 - ParticulWigth, ParticulWigth * 2);
            group.Children.Add(line);

            path.Data = group;
            path.MaxWidth = rect.Width * 100;
            path.MaxHeight = ParticulWigth * 2;
            return path;
        }


        public ICommand AddSupport {  get; set; }
        public ICommand RemoveSupport { get; set; } // ТУТ ЧТО ТО ВЫЧИТАТСЯ ИЗ АКТУАЛ ХИГХТ МБ НАДО ВЫЧИТАЬ ИЗ ХАЙГТ
        public ICommand Calculate {  get; set; }
        public ICommand Load { get; set; }
        public ICommand Save { get; set; }
        public ICommand Clear { get; set; }

        public PreProcessorViewModel()
        {
            EPowerStore.Instance.SetUserData(E);
            NodesStore.Instance.SetUserData(Nodes);
            SuportStore.Instance.SetUserData(Shapes);
            SmthStore.Instance.SetUserData(LeftSmth, RightSmth);
            CanvasChildrens = new ObservableCollection<UIElement>();
            Shapes.CollectionChanged += Draw;
            Nodes.CollectionChanged += Draw;
            E = 1;
            AddSupport = new AddSupportCommand(this);
            RemoveSupport = new RemoveSupportCommand(this);
            Calculate = new CalculateCommand(this);
            Load = new LoadCommand(this);
            Save = new SaveCommand(this);
            Clear = new ClearCommand(this);
        }


        public void RemoveElement(string RemoveUID)
        {
            //сделать что индексы опор переписываются
            List<UIElement> itemstoremove = new List<UIElement>();
            foreach (UIElement ui in CanvasChildrens)
            {
                if (ui.Uid.StartsWith(RemoveUID))
                {
                    itemstoremove.Add(ui);
                }
            }
            foreach (UIElement ui in itemstoremove)
            {
                UIElement uIElement = new UIElement();
                uIElement = canvasChildrens.Where(x => x.Uid == ui.Uid).First();
                CanvasChildrens.Remove(uIElement);
            }
        }
        private bool IsCountNotnull()
        {
            if (SupportCount > 0)
                return true;
            return false;
        }
        public void Draw(object sender, EventArgs e)
        {

            //!!Сделать квадратики и кружочки на макс хигхт
            int Index = 1;
            int MaxHeight = 0;
            PlussesWihth = 0;
            CanvasChildrens.Clear();



            foreach (SupportModelv2 sp in shapes)
            {
                if (sp.Model.Height * 100 > MaxHeight)
                    MaxHeight = (int)sp.Model.Height * 100;

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
                    Canvas.SetTop(Arrow, CanvasActualHenght / 2 - Arrow.MaxHeight / 2);
                    Canvas.SetLeft(Arrow, CanvasActualLenhgt / 2 + PlussesWihth);
                    CanvasChildrens.Add(Arrow);
                }
                else if (sp.PrPower > 0)
                {
                    Arrow.LayoutTransform = new ScaleTransform(-1, 1);
                    Canvas.SetTop(Arrow, CanvasActualHenght / 2 - Arrow.MaxHeight / 2);
                    Canvas.SetLeft(Arrow, CanvasActualLenhgt / 2 + PlussesWihth);
                    CanvasChildrens.Add(Arrow);
                }

                Nrect.Width= 20;
                Nrect.Height= 20;
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
                Canvas.SetTop(Lellipse, CanvasActualHenght / 2 + MaxHeight / 2 + 30);
                Canvas.SetLeft(Lellipse, CanvasActualLenhgt / 2 + PlussesWihth - Lellipse.Width / 2);//asdadasd
                CanvasChildrens.Add(Lellipse);

                Canvas.SetTop(Lnode, CanvasActualHenght / 2 + MaxHeight / 2 + 30 + Lnode.FontSize / 16);
                Canvas.SetLeft(Lnode, CanvasActualLenhgt / 2 + PlussesWihth - Lnode.FontSize / 4);
                CanvasChildrens.Add(Lnode);

                Snumb.Text = (Index).ToString();
                Canvas.SetTop(Nrect, CanvasActualHenght / 2 - MaxHeight / 2 - 40);
                Canvas.SetLeft(Nrect, CanvasActualLenhgt / 2 + PlussesWihth - Nrect.Width / 2 + sp.Model.Width * 100  / 2);
                CanvasChildrens.Add(Nrect);

                Canvas.SetTop(Snumb, CanvasActualHenght / 2 - MaxHeight / 2 - 40 + Snumb.FontSize / 16);
                Canvas.SetLeft(Snumb, CanvasActualLenhgt / 2 + PlussesWihth + sp.Model.Width * 100  / 2 - Snumb.FontSize / 4);
                CanvasChildrens.Add(Snumb);

                var newRect = new Rectangle();
                newRect.Height= sp.Model.Height * 100;
                newRect.Width= sp.Model.Width * 100;
                newRect.Stroke = Brushes.Black;
                newRect.StrokeThickness = 1;
                Canvas.SetTop(newRect, CanvasActualHenght / 2 - sp.Model.Height * 100 / 2);
                Canvas.SetLeft(newRect, CanvasActualLenhgt / 2 + PlussesWihth);
                CanvasChildrens.Add(newRect);


                if (Index == 1)
                {
                    if (SupportCount == 1)
                    {
                        //стрелочка слева для первого квадратика
                        if (Nodes[Index - 1].PoPower > 0)
                        {
                            ShortArrow.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                            CanvasChildrens.Add(ShortArrow);
                        }
                        else if (Nodes[Index - 1].PoPower < 0)
                        {
                            ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                            CanvasChildrens.Add(ShortArrow);
                        }

                        //стрелочка справа для последнего квадратика
                        Path ShortArrow1 = new Path();
                        ShortArrow1 = GenereteShortArrow(sp.Model);
                        if (Nodes[Index].PoPower > 0)
                        {

                            ShortArrow1.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow1, CanvasActualHenght / 2 - ShortArrow1.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow1, CanvasActualLenhgt / 2 + sp.Model.Width * 100  - sp.Model.Width * 100  / 4);
                            CanvasChildrens.Add(ShortArrow1);
                        }
                        else if (Nodes[Index].PoPower < 0)
                        {
                            ShortArrow1.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow1.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow1, CanvasActualHenght / 2 - ShortArrow1.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow1, CanvasActualLenhgt / 2 + sp.Model.Width * 100  - sp.Model.Width * 100  / 4);
                            CanvasChildrens.Add(ShortArrow1);
                        }
                    }
                    else
                    {
                        //стрелочка слева для первого квадратика
                        if (Nodes[Index - 1].PoPower > 0)
                        {
                            ShortArrow.Stroke = Brushes.Red;
                            Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                            CanvasChildrens.Add(ShortArrow);
                        }
                        else if (Nodes[Index - 1].PoPower < 0)
                        {
                            ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                            ShortArrow.Stroke = Brushes.Blue;
                            Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                            Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                            CanvasChildrens.Add(ShortArrow);
                        }
                    }
                }
                if (Index > 1 && Index < shapes.Count)
                {
                    //стрелочка слева для дефолт квадратика
                    if (Nodes[Index - 1].PoPower > 0)
                    {
                        ShortArrow.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                        CanvasChildrens.Add(ShortArrow);
                    }
                    else if (Nodes[Index - 1].PoPower < 0)
                    {
                        ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth - ShortArrow.MaxWidth / 4);
                        CanvasChildrens.Add(ShortArrow);
                    }


                }
                if (Index == shapes.Count && SupportCount > 1)
                {
                    //стрелочка слева для дефолт квадратика
                    if (Nodes[Index - 1].PoPower > 0)
                    {
                        ShortArrow.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth);
                        CanvasChildrens.Add(ShortArrow);
                    }
                    else if (Nodes[Index - 1].PoPower < 0)
                    {
                        ShortArrow.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow, CanvasActualHenght / 2 - ShortArrow.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow, CanvasActualLenhgt / 2 + PlussesWihth - ShortArrow.MaxWidth / 4);
                        CanvasChildrens.Add(ShortArrow);
                    }
                    //стрелочка справа для последнего квадратика
                    Path ShortArrow1 = new Path();
                    ShortArrow1 = GenereteShortArrow(sp.Model);
                    if (Nodes[Index].PoPower > 0)
                    {

                        ShortArrow1.Stroke = Brushes.Red;
                        Canvas.SetTop(ShortArrow1, CanvasActualHenght / 2 - ShortArrow1.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow1, CanvasActualLenhgt / 2 + PlussesWihth + sp.Model.Width * 100  - sp.Model.Width * 100  / 4);
                        CanvasChildrens.Add(ShortArrow1);
                    }
                    else if (Nodes[Index].PoPower < 0)
                    {
                        ShortArrow1.LayoutTransform = new ScaleTransform(-1, 1);
                        ShortArrow1.Stroke = Brushes.Blue;
                        Canvas.SetTop(ShortArrow1, CanvasActualHenght / 2 - ShortArrow1.MaxHeight / 2);
                        Canvas.SetLeft(ShortArrow1, CanvasActualLenhgt / 2 + PlussesWihth + sp.Model.Width * 100  - sp.Model.Width * 100  / 4);
                        CanvasChildrens.Add(ShortArrow1);
                    }
                }

                PlussesWihth += (int)sp.Model.Width * 100; ///////////////////////////////////////!!!11!!11

                if (Index == SupportCount)
                {
                    Rnode.Text = (Index + 1).ToString();
                    Canvas.SetTop(Rellipse, CanvasActualHenght / 2 + MaxHeight / 2 + 30);
                    Canvas.SetLeft(Rellipse, CanvasActualLenhgt / 2 + PlussesWihth - Rellipse.Width / 2);//asdadasd
                    CanvasChildrens.Add(Rellipse);

                    Canvas.SetTop(Rnode, CanvasActualHenght / 2 + MaxHeight / 2 + 30 + Rnode.FontSize / 16);
                    Canvas.SetLeft(Rnode, CanvasActualLenhgt / 2 + PlussesWihth - Rnode.FontSize / 4);
                    CanvasChildrens.Add(Rnode);
                }

                sp.Model.Uid = Index.ToString();
                Index++;


            }
            
            foreach (UIElement uIElement in CanvasChildrens)
            {
                Canvas.SetLeft(uIElement, Canvas.GetLeft(uIElement) - PlussesWihth / 2);
            }
            if (LeftSmth)
            {
                Path LeftMyShape = new Path();
                LeftMyShape = GenerateBorder(Shapes[0].Model);
                Canvas.SetTop(LeftMyShape, CanvasActualHenght / 2 - LeftMyShape.MaxHeight / 2);
                Canvas.SetLeft(LeftMyShape, CanvasActualLenhgt / 2 - 10 - PlussesWihth / 2);
                LeftMyShape.Uid = "Left";
                CanvasChildrens.Add(LeftMyShape);
            }
            if (RightSmth)
            {
                Path myShape = new Path();
                myShape = GenerateBorder(Shapes[Shapes.Count - 1].Model);
                myShape.LayoutTransform = new ScaleTransform(-1, 1);
                Canvas.SetTop(myShape, CanvasActualHenght / 2 - myShape.MaxHeight / 2);
                Canvas.SetLeft(myShape, CanvasActualLenhgt / 2 + PlussesWihth / 2);
                myShape.Uid = "Right";
                CanvasChildrens.Add(myShape);
            }
        }
    }
}
