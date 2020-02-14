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

        }

        private void SugarChecked(object sender, RoutedEventArgs e)
        {
            order.Content += "with sugar";
        }
        private void CreamChecked(object sender, RoutedEventArgs e)
        {
            order.Content += "with cream";
        }

        private void SmlClicked(object sender, RoutedEventArgs e)
        {
            order.Content += "Small";
        }

        private void MdClicked(object sender, RoutedEventArgs e)
        {
            order.Content += "Medium";
        }

        private void LrgClicked(object sender, RoutedEventArgs e)
        {
            order.Content += "Large";
        }
    }
}