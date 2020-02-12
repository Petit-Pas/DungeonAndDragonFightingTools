﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DDFight.Controlers.InputBoxes
{

    /// <summary>
    /// Logique d'interaction pour BindableRichTextBox.xaml
    /// </summary>
    public partial class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(BindableRichTextBox), new FrameworkPropertyMetadata
            (null, new PropertyChangedCallback(OnDocumentChanged)));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            RichTextBox rtb = (RichTextBox)obj;
            if (args.NewValue != null)
                rtb.Document = (FlowDocument)args.NewValue;
        }
    }
}   
