using DDFight.Game.Aggression.Spells.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression
{
    public class Spell : AAttackTemplate, ICloneable
    {
        public Spell () : base()
        {
        }

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

        public int BaseLevel
        {
            get => _baseLevel;
            set 
            {
                _baseLevel = value;
                NotifyPropertyChanged();
            }
        }
        private int _baseLevel = 0;

        #region EditWindow

        [XmlIgnore]
        public bool Validated = false;

        public bool OpenEditWindow()
        {
            SpellEditWindow window = new SpellEditWindow();
            Spell temporary = (Spell)this.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (temporary.Validated == true)
            {
                this.CopyAssign(temporary);
                return true;
            }
            return false;
        }

        #endregion EditWindow

        #region ICloneable

        private void init_copy(Spell to_copy)
        {
            this.BaseLevel = to_copy.BaseLevel;
            this.Description = (string)to_copy.Description.Clone();
        }

        protected Spell(Spell to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public new object Clone()
        {
            return new Spell(this);
        }

        #region ICopyAssignable

        public override void CopyAssign(object to_copy)
        {
            base.CopyAssign(to_copy);
            init_copy((Spell)to_copy);
        }

        #endregion ICopyAssignable
        #endregion ICloneable
    }
}
