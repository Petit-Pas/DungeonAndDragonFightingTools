using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Dices
{
    public class Dices : ICloneable, INotifyPropertyChanged
    {
        public Dices()
        {
        }

        /// <summary>
        ///     Warning, should be a string formatted as follows: "2d6"
        /// </summary>
        /// <param name="format"></param>
        public Dices(string format)
        {
            try
            {
                DiceAmount = Int32.Parse(format.Substring(0, format.IndexOf('d')));
                DiceValue = Int32.Parse(format.Substring(format.IndexOf('d') + 1));
            }
            catch (Exception) 
            {
                Logger.Log("WARNING: Bad string format sent to Dices(string) Ctor: " + format);
            }
        }

        public override string ToString()
        {
            return DiceAmount.ToString() + "d" + DiceValue.ToString();
        }

        [XmlAttribute]
        public int DiceValue
        {
            get => _diceValue;
            set
            {
                if (_diceValue != value)
                {
                    _diceValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _diceValue = 1;
        
        [XmlAttribute]
        public int DiceAmount
        {
            get => _diceAmount;
            set
            {
                if (_diceAmount != value)
                {
                    _diceAmount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _diceAmount = 1;

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

        protected Dices(Dices to_copy)
        {
            DiceAmount = to_copy.DiceAmount;
            DiceValue = to_copy.DiceValue;
        }

        public object Clone()
        {
            return new Dices(this);
        }

        #endregion
    }
}
