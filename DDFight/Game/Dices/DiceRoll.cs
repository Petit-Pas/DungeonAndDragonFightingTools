using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Dices
{
    /// <summary>
    ///     represents 1 set of Dices to roll, example: 1d4+2d6+5
    /// </summary>
    public class DiceRoll : INotifyPropertyChanged, ICloneable
    {
        public DiceRoll() 
        {
            if (!GlobalVariables.Loading)
                init();
        }

        public DiceRoll(string format)
        {
            DicesList = new List<Dices>();
            try
            {
                int indexD = format.IndexOf('d');
                while (indexD != -1)
                {
                    Console.WriteLine("loop");
                    int indexP = format.IndexOf('+');
                    string subFormat;
                    if (indexP != -1)
                    {
                        subFormat = format.Substring(0, indexP);
                        format = format.Substring(indexP + 1);
                        DicesList.Add(new Dices(subFormat));
                    }
                    else
                    {
                        DicesList.Add(new Dices(format));
                        format = "";
                    }
                    indexD = format.IndexOf('d');
                }
                if (format.Length != 0)
                {
                    Modifier = Int32.Parse(format);
                }
            }
            catch (Exception)
            {
                Logger.Log("WARNING: wrong format for constructor format in DiceRoll(string): " + format);
            }
        }

        private void init()
        {
            DicesList = new List<Dices>();
        }

        public override string ToString()
        {
            string format = "";

            if (DicesList != null)
            {
                foreach (Dices d in DicesList)
                {
                    format = format + d.ToString() + "+";
                }
            }
            if (format.Length != 0)
            {
                format = format.Substring(0, format.Length - 1);
            }
            if (Modifier != 0)
            {
                if (format != "")
                    format += '+';
                format = format + Modifier.ToString();
            }
            return format;
        }

        /// <summary>
        ///     the dices to throw
        /// </summary>
        public List<Dices> DicesList
        {
            get => _dicesList;
            set
            {
                _dicesList = value;
                NotifyPropertyChanged();
            }
        }
        private List<Dices> _dicesList = null;

        /// <summary>
        ///     Modifier to add to the result of the dices
        /// </summary>
        public int Modifier
        {
            get => _modifier;
            set
            {
                _modifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _modifier;

        #region INotifyPropertyChanged

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region ICloneable

        protected DiceRoll(DiceRoll to_copy)
        {
            Modifier = to_copy.Modifier;
            DicesList = (List<Dices>)to_copy.DicesList.Clone();
        }

        public object Clone()
        {
            return new DiceRoll();
        }

        #endregion
    }
}
