using DDFight.WpfExtensions;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.ListExtensions
{
    public static class ModifyListExtension
    {
        public static bool AddElement(this CustomVerboseStatusList customVerboseStatusList, CustomVerboseStatus status = null)
        {
            if (status == null)
                status = new CustomVerboseStatus();
            if (status.OpenEditWindow())
            {
                customVerboseStatusList.AddElementSilent(status);
                return true;
            }
            return false;
        }

        public static bool AddElement(this SpellList spellList, Spell spell = null)
        {
            if (spell == null)
                spell = new Spell();
            if (spell.OpenEditWindow())
            {
                spellList.AddElementSilent(spell);
                return true;
            }
            return false;
        }

        public static bool AddElement(this OnHitStatusList OnHitStatusList, OnHitStatus status = null)
        {
            if (status == null)
                status = new OnHitStatus();
            if (status.OpenEditWindow())
            {
                OnHitStatusList.AddElementSilent(status);
                return true;
            }
            return false;
        }

    }
}
