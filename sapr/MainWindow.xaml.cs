using Microsoft.Win32;
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
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    
}
