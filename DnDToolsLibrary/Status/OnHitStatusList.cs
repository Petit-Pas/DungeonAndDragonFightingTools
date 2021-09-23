using DnDToolsLibrary.Memory;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Status
{
    public class OnHitStatusList : GenericList<OnHitStatus>, INotifyPropertyChanged, ICloneable
    {
        public OnHitStatusList() : base() {
        }

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
                foreach (OnHitStatus status in this)
                {
                    status.HasSpellSaving = value;
                }
                NotifyPropertyChanged();
            }
        }
        private bool _hasSavingThrow = false;

        public void init_copy(OnHitStatusList to_copy)
        {
            HasSavingThrow = to_copy.HasSavingThrow;

        }

        public OnHitStatusList(OnHitStatusList to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public override object Clone()
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
