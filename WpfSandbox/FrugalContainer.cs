using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfSandbox
{
    public class FrugalContainer : Decorator
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(0, 0);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            // get it all
            Child.Measure(arrangeSize);
            Child.Arrange(new Rect(arrangeSize));
            return Child.RenderSize;
        }
    }
}
