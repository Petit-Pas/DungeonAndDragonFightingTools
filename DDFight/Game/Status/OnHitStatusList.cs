using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class OnHitStatusList : INotifyPropertyChanged, ICloneable
    {
        public OnHitStatusList() { }

        public ObservableCollection<OnHitStatus> List
        {
            get => _list;
            set
            {
                _list = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<OnHitStatus> _list = new ObservableCollection<OnHitStatus>();

        /// <summary>
        ///     Watch out to do the difference with this (set to true if the status is applied after a Saving on a Spell), 
        ///     not the same as OnHitStatus.HasSavingThrow which represent a chance to resist a status only
        /// </summary>
        [XmlAttribute]
        public bool HasSavingThrow
        {
            get => _hasSavingThrow;
            set
            {
                _hasSavingThrow = value;
                foreach (OnHitStatus status in List)
                {
                    status.HasSpellSaving = value;
                }
                NotifyPropertyChanged();
            }
        }
        private bool _hasSavingThrow = false;

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

        public void init_copy(OnHitStatusList to_copy)
        {
            List = to_copy.List.Clone();
            HasSavingThrow = to_copy.HasSavingThrow;
        }

        public OnHitStatusList(OnHitStatusList to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new OnHitStatusList(this);
        }

        public virtual void CopyAssign(object _to_copy)
        {
            OnHitStatusList to_copy = (OnHitStatusList)_to_copy;
            init_copy(to_copy);
        }

    }
}
