using System;
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

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour BindableRichTextBox.xaml
    /// </summary>
    public partial class BindableRichTextBox : RichTextBox
    {
        public BindableRichTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DocumentProperty =
       DependencyProperty.Register("Document", typeof(FlowDocument),
       typeof(BindableRichTextBox), new FrameworkPropertyMetadata
       (null, new PropertyChangedCallback(OnDocumentChanged)));

        public FlowDocument Document
        {
            get
            {
                return (FlowDocument)GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            Console.WriteLine("detects change");
            RichTextBox rtb = (RichTextBox)obj;
            if (args.NewValue != null)
                rtb.Document = (FlowDocument)args.NewValue;
            rtb.ScrollToEnd();
        }
    }
}
