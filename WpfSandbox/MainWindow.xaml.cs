// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

        List<MyDataSource> list = new List<MyDataSource>();



        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            list.Add(new MyDataSource("ADE"));
            list.Add(new MyDataSource("1BCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3"));
            list.Add(new MyDataSource("4dfshfABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE")); list.Add(new MyDataSource("ABCDE"));
            list.Add(new MyDataSource("1ABCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3ABCDE"));
            list.Add(new MyDataSource("4ABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE")); list.Add(new MyDataSource("ABCDE"));
            list.Add(new MyDataSource("1ABCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3ABCDE"));
            list.Add(new MyDataSource("4ABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE")); list.Add(new MyDataSource("ABCDE"));
            list.Add(new MyDataSource("1ABCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3ABCDE"));
            list.Add(new MyDataSource("4ABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE")); list.Add(new MyDataSource("ABCDE"));
            list.Add(new MyDataSource("1ABCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3ABCDE"));
            list.Add(new MyDataSource("4ABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE")); list.Add(new MyDataSource("ABCDE"));
            list.Add(new MyDataSource("1ABCDE"));
            list.Add(new MyDataSource("2ABCDE"));
            list.Add(new MyDataSource("3ABCDE"));
            list.Add(new MyDataSource("4ABCDE"));
            list.Add(new MyDataSource("5ABCDE"));
            list.Add(new MyDataSource("6ABCDE"));
            list.Add(new MyDataSource("7ABCDE"));
            list.Add(new MyDataSource("8ABCDE"));
            list.Add(new MyDataSource("9ABCDE"));

            itemControl.ItemsSource = list;
        }

        private void UseCustomHandler(object sender, RoutedEventArgs e)
        {
            return;
            /*var myBindingExpression = textBox3.GetBindingExpression(TextBox.TextProperty);
            var myBinding = myBindingExpression.ParentBinding;
            myBinding.UpdateSourceExceptionFilter = ReturnExceptionHandler;
            myBindingExpression.UpdateSource();*/
        }

        private void DisableCustomHandler(object sender, RoutedEventArgs e)
        {
            return;
            // textBox3 is an instance of a TextBox
            // the TextProperty is the data-bound dependency property
            /*var myBinding = BindingOperations.GetBinding(textBox3, TextBox.TextProperty);
            myBinding.UpdateSourceExceptionFilter -= ReturnExceptionHandler;
            BindingOperations.GetBindingExpression(textBox3, TextBox.TextProperty).UpdateSource();*/
        }

        private object ReturnExceptionHandler(object bindingExpression, Exception exception) => "This is from the UpdateSourceExceptionFilterCallBack.";
    }
}