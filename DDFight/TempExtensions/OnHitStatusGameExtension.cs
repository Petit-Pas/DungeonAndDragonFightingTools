using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DDFight.Game.Aggression.Display;
using DDFight.Game.Status.Display;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight.Events;
using DnDToolsLibrary.Status;
using System.ComponentModel;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.Extensions;

namespace TempExtensionsOnHitStatus
{
    public static class OnHitStatusGameExtension
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        /// <summary>
        ///     is injected in OnHitStatus.Register at the start of the application in order to handle the events at a higher layer of the application 
        /// </summary>
        /// <param name="status"></param>
        public static void Register(OnHitStatus status)
        {
            if (status.Affected != null)
            {
                if ((status.CanRedoSavingThrow && status.SavingIsRemadeAtStartOfTurn) ||
                    (status.HasAMaximumDuration && !status.DurationIsCalculatedOnCasterTurn && status.DurationIsBasedOnStartOfTurn) ||
                    status.DotDamageList.Count != 0)
                    status.Affected.NewTurnStarted += status.Affected_NewTurnStarted;
                if ((status.CanRedoSavingThrow && status.SavingIsRemadeAtStartOfTurn == false) ||
                    (status.HasAMaximumDuration && !status.DurationIsCalculatedOnCasterTurn && !status.DurationIsBasedOnStartOfTurn) ||
                    status.DotDamageList.Count != 0)
                    status.Affected.TurnEnded += status.Affected_TurnEnded;
            }
            if (status.Caster != null)
            {
                if ((status.HasAMaximumDuration && status.DurationIsCalculatedOnCasterTurn && status.DurationIsBasedOnStartOfTurn) ||
                    status.DotDamageList.Count != 0)
                    status.Caster.NewTurnStarted += status.Caster_NewTurnStarted;
                if ((status.HasAMaximumDuration && status.DurationIsCalculatedOnCasterTurn && !status.DurationIsBasedOnStartOfTurn) ||
                    status.DotDamageList.Count != 0)
                    status.Caster.TurnEnded += status.Caster_TurnEnded;

                if (status.EndsOnCasterLossOfConcentration)
                    status.Caster.PropertyChanged += status.Caster_PropertyChanged;
            }
        }

        /// <summary>
        ///     is injected in OnHitStatus.Register at the start of the application in order to handle the events at a higher layer of the application 
        /// </summary>
        /// <param name="status"></param>
        public static void Unregister(OnHitStatus status)
        {
            if (status.Caster != null)
            {
                status.Caster.PropertyChanged -= status.Caster_PropertyChanged;
                status.Caster.NewTurnStarted -= status.Caster_NewTurnStarted;
                status.Caster.TurnEnded -= status.Caster_TurnEnded;
            }
            if (status.Affected != null)
            {
                status.Affected.NewTurnStarted -= status.Affected_NewTurnStarted;
                status.Affected.TurnEnded -= status.Affected_TurnEnded;
            }
        }

        /// <summary>
        ///     Will trigger any dot damage required
        /// </summary>
        /// <param name="start"> true if its start of turn, false otherwise </param>
        /// <param name="caster"> true if its caster's turn, false otherwise </param>
        public static void CheckDotDamage(this OnHitStatus onHitStatus, bool start, bool caster)
        {
            DamageResultList to_apply = new DamageResultList();
            foreach (DotTemplate dot in onHitStatus.DotDamageList)
            {
                if (dot.TriggersStartOfTurn == start && dot.TriggersOnCastersTurn == caster)
                    to_apply.AddElementSilent(new DamageResult(dot));
            }
            if (to_apply.Count != 0)
            {
                DamageResultListRollableWindow window = new DamageResultListRollableWindow() { DataContext = to_apply, };
                window.TitleControl.Text = onHitStatus.Header + " inflicts damage to " + onHitStatus.Affected.DisplayName;
                window.ShowCentered();

                if (window.Validated)
                {
                    onHitStatus.Affected.TakeHitDamage(to_apply);
                }
            }
        }

        public static void Affected_TurnEnded(this OnHitStatus onHitStatus, object sender, TurnEndedEventArgs args)
        {
            bool expired = false;

            onHitStatus.CheckDotDamage(false, false);

            if (onHitStatus.HasAMaximumDuration && !onHitStatus.DurationIsCalculatedOnCasterTurn && !onHitStatus.DurationIsBasedOnStartOfTurn)
                expired = onHitStatus.RemoveDuration();
            if (!expired && onHitStatus.CanRedoSavingThrow && !onHitStatus.SavingIsRemadeAtStartOfTurn)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(onHitStatus.Caster, onHitStatus.Affected, false);
                window.DataContext = onHitStatus;
                window.ShowCentered();
            }
        }

        public static void Caster_TurnEnded(this OnHitStatus onHitStatus, object sender, TurnEndedEventArgs args)
        {
            onHitStatus.CheckDotDamage(false, true);

            if (onHitStatus.HasAMaximumDuration && onHitStatus.DurationIsCalculatedOnCasterTurn && !onHitStatus.DurationIsBasedOnStartOfTurn)
                onHitStatus.RemoveDuration();
        }

        public static void Caster_NewTurnStarted(this OnHitStatus onHitStatus, object sender, StartNewTurnEventArgs args)
        {
            onHitStatus.CheckDotDamage(true, true);

            if (onHitStatus.HasAMaximumDuration && onHitStatus.DurationIsCalculatedOnCasterTurn && onHitStatus.DurationIsBasedOnStartOfTurn)
                onHitStatus.RemoveDuration();
        }

        public static void Caster_PropertyChanged(this OnHitStatus onHitStatus, object sender, PropertyChangedEventArgs e)
        {
            if (onHitStatus.EndsOnCasterLossOfConcentration && e.PropertyName == "IsFocused" && onHitStatus.Caster.IsFocused == false)
            {
                console.AddEntry("Due to ");
                console.AddEntry($"{onHitStatus.Caster.DisplayName}", fontWeightProvider.SemiBold);
                console.AddEntry("'s loss of concentration, ");
                onHitStatus.RemoveStatus();
            }
        }

        public static void Affected_NewTurnStarted(this OnHitStatus onHitStatus, object sender, StartNewTurnEventArgs args)
        {
            bool expired = false;

            onHitStatus.CheckDotDamage(true, false);

            if (onHitStatus.HasAMaximumDuration && !onHitStatus.DurationIsCalculatedOnCasterTurn && onHitStatus.DurationIsBasedOnStartOfTurn)
                expired = onHitStatus.RemoveDuration();
            if (!expired && onHitStatus.CanRedoSavingThrow && onHitStatus.SavingIsRemadeAtStartOfTurn)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(onHitStatus.Caster, onHitStatus.Affected, false);
                window.DataContext = onHitStatus;

                window.ShowCentered();
            }
        }

        /// <summary>
        ///     removes 1 turn from the Remaining rounds variable
        ///     if the status expires, the function removes it from the target of the status
        /// </summary>
        /// <returns></returns>
        private static bool RemoveDuration(this OnHitStatus onHitStatus)
        {
            onHitStatus.RemainingRounds -= 1;
            if (onHitStatus.RemainingRounds <= 0)
            {
                console.AddEntry("The Status inflicted by ");
                console.AddEntry($"{onHitStatus.Caster.DisplayName}", fontWeightProvider.SemiBold);
                console.AddEntry(" has expired. ");
                onHitStatus.RemoveStatus();

                // if caster was focused on this, he can now be "un"focused
                if (onHitStatus.EndsOnCasterLossOfConcentration && onHitStatus.Caster.IsFocused && onHitStatus.Caster == onHitStatus.Affected)
                    onHitStatus.Caster.IsFocused = false;

                return true;
            }
            return false;
        }

        /// <summary>
        ///     Remove the status from the target, and unregister all events
        /// </summary>
        public static void RemoveStatus(this OnHitStatus onHitStatus)
        {
            console.AddEntry($"{onHitStatus.Affected.DisplayName}", fontWeightProvider.SemiBold);
            console.AddEntry(" is no more affected by ");
            console.AddEntry($"{onHitStatus.Header}", fontWeightProvider.SemiBold);
            console.AddEntry(".\r\n");

            onHitStatus.Affected.CustomVerboseStatusList.RemoveElement(onHitStatus);
            onHitStatus.Unregister();
        }


        /// <summary>
        ///     A function that applies this status to the given target
        ///     it will register to any required event for the status to automatically ends
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        /// <param name="application_success"> tells wether or not the application is a success, only used with "false" to tell the OnApplyDamage can be resisted / canceled </param>
        /// <param name="multiple_application"> tells that a status will be applied more than once ==> to avoid the removal of concentration on every new affected ==> false for the first call, true for the other ones </param>
        public static void Apply(this OnHitStatus onHitStatus, PlayableEntity caster, PlayableEntity target, bool application_success = true, bool multiple_application = false)
        {
            // the applied status is a copy
            OnHitStatus applied = (OnHitStatus)onHitStatus.Clone();

            if (applied.OnApplyDamageList.Count != 0)
            {
                DamageResultList onApplyDamageList = onHitStatus.OnApplyDamageList.GetResultList();
                foreach (DamageResult dmg in onApplyDamageList)
                    dmg.LastSavingWasSuccesfull = !application_success;
                DamageResultListRollableWindow window = new DamageResultListRollableWindow() { DataContext=onApplyDamageList };
                window.ShowCentered();
                if (window.Validated)
                    target.TakeHitDamage(onApplyDamageList);
            }

            if (application_success)
            {
                console.AddEntry($"{caster.DisplayName}", fontWeightProvider.Bold);
                console.AddEntry(" applies ");
                console.AddEntry($"{onHitStatus.Header}", fontWeightProvider.Bold);
                console.AddEntry(" on ");
                console.AddEntry($"{target.DisplayName}\r\n", fontWeightProvider.Bold);

                applied.Caster = caster;
                applied.Affected = target;
                target.CustomVerboseStatusList.AddElementSilent(applied);
                OnHitStatus.RegisterEvents(applied);
                
                if (applied.EndsOnCasterLossOfConcentration)
                {
                    if (caster.IsFocused == true && multiple_application == false)
                        caster.IsFocused = false;
                    caster.IsFocused = true;
                }
            }
        }

        /// <summary>
        ///     Will open a window if a check has to be made for the OnHitStatus to affect the target, then apply the status if required
        /// </summary>
        /// <param name="caster"> the one that tries to apply the status </param>
        /// <param name="target"> the target of the status </param>
        public static void CheckIfApply(this OnHitStatus onHitStatus, PlayableEntity caster, PlayableEntity target)
        {
            if (onHitStatus.HasApplyCondition)
            {
                OnHitStatusApplyWindow window = new OnHitStatusApplyWindow(caster, target);
                window.DataContext = onHitStatus;
                window.ShowCentered();
            }
            else
            {
                onHitStatus.Apply(caster, target);
            }
        }

    }
}
