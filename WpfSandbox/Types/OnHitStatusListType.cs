using DDFight;
using DDFight.Game.Status;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class OnHitStatusListType : INotifyPropertyChanged, ICloneable
    {

        public static OnHitStatusList Convert(OnHitStatusListType list)
        {
            OnHitStatusList result = new OnHitStatusList();
            foreach (OnHitStatusType status in list.List)
            {
                result.AddElementSilent(OnHitStatusType.Convert(status));
            }
            return result;
        }
        
        public OnHitStatusListType() { }

        [XmlArrayItem("OnHitStatus", typeof(OnHitStatusType))]
        public ObservableCollection<OnHitStatusType> List
        {
            get => _list;
            set
            {
                _list = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<OnHitStatusType> _list = new ObservableCollection<OnHitStatusType>();

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
                foreach (OnHitStatusType status in List)
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void init_copy(OnHitStatusListType to_copy)
        {
            List = to_copy.List.Clone();
            HasSavingThrow = to_copy.HasSavingThrow;
        }

        public OnHitStatusListType(OnHitStatusListType to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new OnHitStatusListType(this);
        }

        public virtual void CopyAssign(object _to_copy)
        {
            OnHitStatusListType to_copy = (OnHitStatusListType)_to_copy;
            init_copy(to_copy);
        }

    }
}
