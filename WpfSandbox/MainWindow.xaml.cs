// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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

        MyDataSource dataSource = new MyDataSource { Age = 24, Age2 = 242, Age3 = 243 };

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = dataSource;
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