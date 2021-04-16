﻿using System;
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
using WpfToolsLibrary.Commands.WindowCommands;

namespace WpfCustomControlLibrary.Windows
{
    /// <summary>
    /// Interaction logic for WindowTitleBar.xaml
    /// </summary>
    public partial class WindowTitleBar : UserControl
    {
        public WindowTitleBar()
        {
            InitializeComponent();
            TitleBar_PreviewMouseMove = new DragMoveCommand_PreviewMouseMove();
            TitleBar_PreviewMouseLeftButtonDown = new DragMoveCommand_PreviewMouseLeftButtonDown();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }
        private static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(WindowTitleBar),
                new PropertyMetadata("Title")
            );

        public DragMoveCommand_PreviewMouseMove TitleBar_PreviewMouseMove { get; set; }
        public DragMoveCommand_PreviewMouseLeftButtonDown TitleBar_PreviewMouseLeftButtonDown { get; set; }
    }
}
