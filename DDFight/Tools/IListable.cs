using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools
{
    public interface IListable : INotifyPropertyChanged, ICloneable, INameable
    {
        bool Edit();
    }
}
