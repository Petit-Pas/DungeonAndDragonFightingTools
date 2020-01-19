using DDFight.Game.Fight;
using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace DDFight.Game
{
    /// <summary>
    ///     This class is intended to contain all the generic (and saved) informations:
    ///         - players
    ///         - ennemies schemes
    /// </summary>
    public class GameDataContext : INotifyPropertyChanged
    {
        /// <summary>
        ///     The list of existing character
        /// </summary>
        public CharactersList CharacterList = new CharactersList();

        /// <summary>
        ///     The list of all possible ennemies encountered
        /// </summary>
        public MonstersList MonsterList = new MonstersList();

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        ///     Used to log what happens on the user side
        /// </summary>
        [XmlIgnore]
        public FlowDocument UserLogs
        {
            get => _userLogs;
            set
            {
                _userLogs = value;
                NotifyPropertyChanged();
            }
        }
        private FlowDocument _userLogs = new FlowDocument ();

        [XmlIgnore]
        public FightDataContext FightContext
        {
            get => _fightContext;
            set
            {
                _fightContext = value;
                NotifyPropertyChanged();
            }
        }
        private FightDataContext _fightContext = new FightDataContext();

    }
}
