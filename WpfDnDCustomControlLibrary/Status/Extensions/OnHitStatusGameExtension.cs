using DnDToolsLibrary.Status;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace WpfDnDCustomControlLibrary.Status.Extensoions
{
    public static class OnHitStatusGameExtension
    {
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
                Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;
                paragraph.Inlines.Add(RunExtensions.BuildRun("The Status inflicted by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(RunExtensions.BuildRun(onHitStatus.Caster.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(RunExtensions.BuildRun(" has expired. ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
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
            Paragraph paragraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(RunExtensions.BuildRun(onHitStatus.Affected.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(" is no more affected by ", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(RunExtensions.BuildRun(onHitStatus.Header, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(RunExtensions.BuildRun(".\r\n", (Brush)System.Windows.Application.Current.Resources["Light"], 15, FontWeights.Normal));

            onHitStatus.Affected.CustomVerboseStatusList.RemoveElement(onHitStatus);
            onHitStatus.UnregisterToAll();
        }
    }
}
