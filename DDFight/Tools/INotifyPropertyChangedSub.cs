using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools
{
    interface INotifyPropertyChangedSub
    {
        void PropertyChangedSubscript(PropertyChangedEventHandler handler);
    }
}
