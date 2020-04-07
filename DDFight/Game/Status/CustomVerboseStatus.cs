using DDFight.Game.Characteristics;
using DDFight.Game.Status.Display;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DDFight.Game.Status
{
    public class CustomVerboseStatus : INotifyPropertyChanged, ICloneable
    {
        public CustomVerboseStatus() { }

        [XmlIgnore]
        public bool Validated = false;

        [XmlAttribute]
        // Should be 1 single world
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                NotifyPropertyChanged();
            }
        }
        private string _header = "";

        [XmlAttribute]
        // Should be a short sentence explaining the condition
        public string ToolTip
        {
            get => _toolTip;
            set
            {
                _toolTip = value;
                NotifyPropertyChanged();
            }
        }
        private string _toolTip = "";

        [XmlAttribute]
        // Should be a long description explaining both condition and the way it gets removed
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }
        private string _description = "";


        [XmlAttribute]
        public bool HasApplyCondition
        {
            get => _hasApplyCondition;
            set
            {
                _hasApplyCondition = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasApplyCondition = false;

        [XmlAttribute]
        public CharacteristicsEnum ApplySavingCharacteristic
        {
            get => _applySavingCharacteristic;
            set
            {
                _applySavingCharacteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _applySavingCharacteristic = CharacteristicsEnum.Wisdom;

        // 0 for default spellcasting ability DC
        [XmlAttribute]
        public int ApplySavingDifficulty
        {
            get => _applySavingDifficulty;
            set
            {
                _applySavingDifficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _applySavingDifficulty = 0;

        #region ICloneable

        public void init_copy(CustomVerboseStatus to_copy)
        {
            Description = (string)to_copy.Description.Clone();
            Header = (string)to_copy.Header.Clone();
            ToolTip = (string)to_copy.ToolTip.Clone();
            HasApplyCondition = to_copy.HasApplyCondition;
            ApplySavingCharacteristic = to_copy.ApplySavingCharacteristic;
            ApplySavingDifficulty = to_copy.ApplySavingDifficulty;
        }

        public CustomVerboseStatus (CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new CustomVerboseStatus(this);
        }

        public void CopyAssign(CustomVerboseStatus to_copy)
        {
            init_copy(to_copy);
        }

        #region DisplayOption

        /// <summary>
        ///     Will open a window to edit this instance
        /// </summary>
        /// <returns> True if the current instance has changed, false otherwise </returns>
        public bool OpenEditWindow()
        {
            CustomVerboseStatusEditWindow window = new CustomVerboseStatusEditWindow();

            CustomVerboseStatus dc = (CustomVerboseStatus)this.Clone();

            window.DataContext = dc;

            window.ShowDialog();

            if (dc.Validated)
            {
                this.CopyAssign(dc);
                return true;
            }
            return false;
        }

        #endregion DisplayOption

        #endregion ICloneable

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
    }
}
