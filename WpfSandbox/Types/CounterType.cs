using DDFight.Game.Counters;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSandbox.Types
{
    public class CounterType : INotifyPropertyChanged, ICloneable
    {
        public static CounterList ConvertListCounterType(ObservableCollection<CounterType> list)
        {
            CounterList result = new CounterList();
            foreach (CounterType count in list)
                result.AddElementSilent(ConvertCounterType(count));
            return result;
        }

        public static Counter ConvertCounterType(CounterType to_copy)
        {
            Counter result = new Counter()
            {
                CurrentValue = to_copy.CurrentValue,
                MaxValue = to_copy.MaxValue,
                Name = to_copy.Name,
            };
            return result;
        }

        public CounterType() { }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name = "Counter Name";

        /// <summary>
        ///     The maximum of the value, 0 for no maximum value
        /// </summary>
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                NotifyPropertyChanged();
            }
        }
        private int _maxValue = 0;

        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                NotifyPropertyChanged();
            }
        }
        private int _currentValue = 0;


        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IClonable

        /// <summary>
        ///     this method is required to completely initialize an instance of this by copying another object
        /// </summary>
        private void init_copy(CounterType to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            MaxValue = to_copy.MaxValue;
            CurrentValue = to_copy.CurrentValue;
        }

        /// <summary>
        ///     copy contructor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected CounterType(CounterType to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new CounterType(this);
        }

        /// <summary>
        ///     reinitialize this object by copying the received one
        /// </summary>
        /// <param name="_to_copy"></param>
        public virtual void CopyAssign(object _to_copy)
        {
            CounterType to_copy = (CounterType)_to_copy;
            init_copy(to_copy);
        }
        #endregion
    }
}
