// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfSandbox;

namespace BindValidation
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private MainDataContext dc;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dc = new MainDataContext();
            this.DataContext = dc;
        }
    }
}