using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfSandbox
{
    public class EmbeddedClass : INotifyPropertyChanged
    {
        public int EmbeddedInt
        {
            get => _embeddedInt;
            set
            {
                if (_embeddedInt != value)
                {
                    _embeddedInt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _embeddedInt = 1;

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            Console.WriteLine("property {0} in mainDataContext changed", propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class MainDataContext : INotifyPropertyChanged
    {
        public int MainInt
        {
            get => _mainInt;
            set
            {
                if (_mainInt != value)
                {
                    _mainInt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _mainInt = 0;

        public EmbeddedClass Embedded { get => _embedded ; set => _embedded = value; }
        private EmbeddedClass _embedded = new EmbeddedClass();

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            Console.WriteLine("property {0} in mainDataContext changed", propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
