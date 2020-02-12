// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;

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

        private string dc = "toto";

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = dc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("click: " + this.DataContext);
        }
    }
}