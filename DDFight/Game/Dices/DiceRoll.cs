using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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

        public void AddDice(Dices to_add)
        {
            foreach (Dices dices in DicesList)
            {
                if (dices.DiceValue == to_add.DiceValue)
                {
                    dices.DiceAmount += to_add.DiceAmount;
                    return;
                }
            }
            DicesList.Add(to_add);
        }

        public static Regex rgx = new Regex(@"^((?:[0-9]+)|(?:[0-9]+d[0-9]+))((?:(?:\+|\-)[0-9]+)|(?:(?:\+|\-)[0-9]+d[0-9]+))*$", RegexOptions.IgnoreCase);

        /// <summary>
        ///     Initializes a DiceRoll from an input string (ex: 2d4+1d6+3)
        /// </summary>
        /// <param name="format"></param>
        public DiceRoll(string format)
        {
            format.Replace("D", "d");
            DicesList = new List<Dices>();

            Match match = rgx.Match(format);
            if (match.Success)
                try
                {
                    for (int i = 1; i != match.Groups.Count; i += 1)
                    {
                        foreach (Capture capture in match.Groups[i].Captures)
                        {
                            string captured = capture.ToString();
                            if (captured.Length > 0)
                                if (captured.Contains("d"))
                                {
                                    AddDice(new Dices(captured));
                                }
                                else
                                {
                                    Modifier += Int32.Parse(captured);
                                }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("WARNING: wrong format for constructor format in DiceRoll(string): " + format);
                    Logger.Log(e.GetType().ToString());
                    Logger.Log(e.Message);
                }
                else 
                    Logger.Log("WARNING: wrong format for constructor format in DiceRoll(string): " + format);
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
                    string new_dice = d.ToString();
                    if (new_dice.StartsWith("-") || format.Length == 0)
                        format = format + new_dice;
                    else
                        format = format + "+" + new_dice;
                }
            }
            return format;
        }

        public void Reset()
        {
            LastRoll = 0;
        }

        /// <summary>
        ///     Converts this object to a string (opposite so ctor(string))
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string format = RollTemplateToString();

            if (Modifier != 0)
            {
                if (format != "" && Modifier > 0)
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

            foreach (Dices dice in DicesList)
            {
                // in case of critical, the dices are rolled twice (with 1d4+2, a critical shall roll 2d4+2)
                result += dice.Roll();
                if (critical == true)
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
