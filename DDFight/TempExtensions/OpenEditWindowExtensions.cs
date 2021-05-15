using BaseToolsLibrary.Memory;
using DDFight.Game.Aggression.Spells.Display;
using DDFight.Game.Entities.Display;
using DDFight.Game.Status.Display;
using DDFight.Windows;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.Windows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.WpfExtensions
{
    public static class OpenEditWindowExtensions
    {
        public static bool OpenEditWindowGeneric<T, U>(this T entity) 
            where T : ICopyAssignable 
            where U : Window, IValidableWindow, new()
        {
            U window = new U();
            T temporary = (T)entity.Clone();
            
            window.DataContext = temporary;
            window.ShowCentered();

            if (window.Validated == true)
            {
                entity.CopyAssign(temporary);
                return true;
            }
            return false;
        }

        public static bool OpenEditWindow(this PlayableEntity playableEntity)
        {
            return OpenEditWindowGeneric<PlayableEntity, PlayableEntityEditWindow>(playableEntity);
        }

        public static bool OpenEditWindow(this OnHitStatus onHitStatus)
        {
            return OpenEditWindowGeneric<OnHitStatus, OnHitStatusEditWindow>(onHitStatus);
        }

        public static bool OpenEditWindow(this CustomVerboseStatus status)
        {
            if (status is OnHitStatus onHitStatus)
                return OpenEditWindowGeneric<OnHitStatus, OnHitStatusEditWindow>(onHitStatus);
            return OpenEditWindowGeneric<CustomVerboseStatus, CustomVerboseStatusEditWindow>(status);
        }

        public static bool OpenEditWindow(this Spell spell)
        {
            return OpenEditWindowGeneric<Spell, SpellEditWindow>(spell);
        }

        public static bool OpenEditWindow(this HitAttackTemplate hitAttackTemplate)
        {
            return OpenEditWindowGeneric<HitAttackTemplate, HitAttackTemplateEditWindow>(hitAttackTemplate);
        }
    }
}
