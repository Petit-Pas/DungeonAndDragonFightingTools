﻿using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Dices
{
    /// <summary>
    ///     represents 1 set of Dices to roll, example: 1d4+2d6+5
    /// </summary>
    public class DiceRoll : INotifyPropertyChanged, ICloneable
    {
        public DiceRoll() 
        {
            if (!Global.Loading)
                init();
        }

        /// <summary>
        ///     Initializes a DiceRoll from an input string (ex: 2d4+1d6+3)
        /// </summary>
        /// <param name="format"></param>
        public DiceRoll(string format)
        {
            DicesList = new List<Dices>();
            try
            {
                int indexD = format.IndexOf('d');
                while (indexD != -1)
                {
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

        /// <summary>
        ///     example : 1d6+1d4+4 will only return "1d6+1d4"
        /// </summary>
        /// <returns></returns>
        public string RollTemplateToString()
        {
            string format = "";

            if (DicesList != null)
            {
                foreach (Dices d in DicesList)
                {
                    format = format + d.ToString() + "+";
                }
            }
            if (format.EndsWith("+"))
                format = format.Substring(0, format.Length - 1);
            return format;
        }

        /// <summary>
        ///     Converts this object to a string (opposite so ctor(string))
        /// </summary>
        /// <returns></returns>
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
        ///     does not contain the modifier, dices only
        /// </summary>
        [XmlIgnore]
        public int LastRoll
        {
            get => _lastRoll;
            set
            {
                _lastRoll = value;
                NotifyPropertyChanged();
            }
        }
        private int _lastRoll = 0;


        /// <summary>
        ///     contains the modifier
        /// </summary>
        [XmlIgnore]
        public int LastResult
        {
            get => LastRoll + Modifier;
        }

        public static int Roll(string input, bool advantage = false, bool disadvantage = false)
        {
            DiceRoll dice = new DiceRoll(input);
            dice.Roll();

            if (advantage != disadvantage)
            {
                DiceRoll second_dice = new DiceRoll(input);
                second_dice.Roll();

                if (advantage == true)
                    return dice.LastResult > second_dice.LastResult ? dice.LastResult : second_dice.LastResult;
                return dice.LastResult < second_dice.LastResult ? dice.LastResult : second_dice.LastResult;
            }

            return dice.LastResult;
        }

        public void Roll(bool critical = false)
        {
            int result = 0;

            foreach(Dices dice in DicesList)
            {
                // in case of critical, the dices are rolled twice, but not the bonus to damage (with 1d4+2, a critical shall roll 2d4+2, and not 2d4+4)
                if (dice.ToString().Contains("d") && critical == true)
                    result += dice.Roll();
                result += dice.Roll();
            }
            LastRoll = result;
        }

        #region Properties 

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

        [XmlAttribute]
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

        #endregion

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
            return new DiceRoll(this);
        }

        #endregion
    }
}
