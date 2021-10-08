using BaseToolsLibrary;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Dice
{
    /// <summary>
    ///     represent an amount of one given dice, example: 1d6 or 3d12
    /// </summary>
    public class Dices : ICloneable, INotifyPropertyChanged, IEquivalentComparable<Dices>
    {
        static private Random rand = new Random();

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
                int d_index = format.IndexOf('d');
                string diceAmount = format.Substring(0, d_index);
                string diceValue = format.Substring(d_index + 1);
                DiceAmount = Int32.Parse(diceAmount);
                DiceValue = Int32.Parse(diceValue);
            }
            catch (Exception)
            {
                Console.WriteLine("WARNING: Bad string format sent to Dices(string) Ctor: " + format);
            }
        }

        public override string ToString()
        {
            return DiceAmount.ToString() + "d" + DiceValue.ToString();
        }

        #region Properties

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

        #endregion

        public int Roll(bool critical = false)
        {
            int result = 0;
            string output = "";

            Console.Write("rolling " + DiceAmount.ToString() + " " + DiceValue.ToString() + ": ");
            for (int i = 0; i != Math.Abs(DiceAmount) * (critical ? 2 : 1); i++)
            {
                int new_val = rand.Next(1, DiceValue + 1);
                Console.Write(new_val.ToString() + (i + 1 == DiceAmount ? "" : ", "));
                output = output + (output.Length == 0 ? "" : ", ") + new_val.ToString();
                result += new_val;
            }
            result = DiceAmount > 0 ? result : -result;
            Console.WriteLine(" ==> result: " + result.ToString());

            output = this.ToString() + (critical ? " (crit) " : "") + " => " + output + " (" + result + ")";
            RolledDices = output;
            return result;
        }

        public string RolledDices
        {
            get => _rolledDices;
            set
            {
                _rolledDices = value;
                NotifyPropertyChanged();
            }
        }
        private string _rolledDices = "";

        public void Reset()
        {
            RolledDices = "";
        }

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

        public bool IsEquivalentTo(Dices toCompare)
        {
            if (this.DiceAmount != toCompare.DiceAmount)
                return false;
            if (this.DiceValue != toCompare.DiceValue)
                return false;
            if (this.RolledDices != toCompare.RolledDices)
                return false;
            return true;
        }
    }
}
