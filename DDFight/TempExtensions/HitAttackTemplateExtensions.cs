using DDFight.Windows;
using DnDToolsLibrary.Attacks.HitAttacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.TempExtensions
{
    public static class HitAttackTemplateExtensions
    {
        public static bool OpenEditWindow(this HitAttackTemplate hitAttackTemplate)
        {
            HitAttackTemplateEditWindow window = new HitAttackTemplateEditWindow();
            HitAttackTemplate temporary = (HitAttackTemplate)hitAttackTemplate.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (window.Validated == true)
            {
                hitAttackTemplate.CopyAssign(temporary);
                return true;
            }
            return false;
        }
    }
}
