using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Spells
{
    public class SpellsList : INotifyPropertyChanged, ICloneable
    {
        public SpellsList(bool isMainSpellList = false)
        {
            this.isMainSpellList = isMainSpellList;
        }

        public SpellsList()
        {
            this.isMainSpellList = false;
        }

        public bool isMainSpellList = false;

        public ObservableCollection<Spell> Spells
        {
            get => _spellList;
            set 
            {
                _spellList = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<Spell> _spellList = new ObservableCollection<Spell>();

        /// <summary>
        ///     Method to add and save a spell
        /// </summary>
        /// <param name="spell"></param>
        public void AddSpell(Spell spell)
        {
            Spells.Add(spell);
            NotifyPropertyChanged("Spells");
            Save();
        }

        /// <summary>
        ///     Method to remove a spell, then save
        /// </summary>
        /// <param name="spell"></param>
        public void RemoveSpell(Spell spell)
        {
            Spells.Remove(spell);
            NotifyPropertyChanged("Spells");
            Save();
        }

        /// <summary>
        ///     Method to save Spells as they are
        /// </summary>
        public void Save()
        {
            Spells.Sort((x, y) => {
                return x.Name.CompareTo(y.Name);
            });
            if (isMainSpellList)
                SaveManager.SaveSpells(this);
        }

        /// <summary>
        ///     Method to replace a spell by a new one
        /// </summary>
        /// <param name="to_update"></param>
        /// <param name="temporary"></param>
        public void ReplaceSpell(Spell to_replace, Spell new_spell)
        {
            for (int i = 0; i != Spells.Count; i += 1)
            {
                if (to_replace == Spells[i])
                {
                    Spells[i] = new_spell;
                    break;
                }
            }
            NotifyPropertyChanged("Spells");
            Save();
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


        #region IClonable

        /// <summary>
        ///     this method is required to completely initialize an instance of this by copying another object
        /// </summary>
        private void init_copy(SpellsList to_copy)
        {
            Spells = to_copy.Spells.Clone();
        }

        /// <summary>
        ///     copy contructor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected SpellsList(SpellsList to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new SpellsList(this);
        }

        /// <summary>
        ///     reinitialize this object by copying the received one
        /// </summary>
        /// <param name="_to_copy"></param>
        public virtual void CopyAssign(object _to_copy)
        {
            SpellsList to_copy = (SpellsList)_to_copy;
            init_copy(to_copy);
        }

        #endregion IClonable
    }
}
