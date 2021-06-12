using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Status.TempCommands
{
    public static class OnHitStatusTempCommands
    {
        public static void TryApply(this OnHitStatus onHitStatus, SavingThrow savingThrow, PlayableEntity applicant, PlayableEntity target)
        {
            if (onHitStatus.HasApplyCondition)
            {
                ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
                IFontColorProvider colorProvider = DIContainer.GetImplementation<IFontColorProvider>();
                IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

                // there is a saving throw to resist the status
                Characteristic charac = target.Characteristics.GetCharacteristic(onHitStatus.ApplySavingCharacteristic);

                console.AddEntry(target.DisplayName, fontWeightProvider.Bold);
                console.AddEntry(" tries to restist the ");
                console.AddEntry(onHitStatus.Header, fontWeightProvider.Bold);
                console.AddEntry(" status from ");
                console.AddEntry(applicant.DisplayName, fontWeightProvider.Bold);
                console.AddEntry(". ");
                console.AddEntry($"{savingThrow.Result}/{savingThrow.Difficulty}", fontWeightProvider.Bold);
                console.AddEntry(" ==> ");

                if (savingThrow.Result >= savingThrow.Difficulty)
                {
                    //resist
                    console.AddEntry("Success\r\n", fontWeightProvider.Bold);
                    //applyStatus(false);
                }
                else
                {
                    //fails
                    console.AddEntry("Failure\r\n", fontWeightProvider.Bold);
                    //applyStatus();
                }
            }
        }
    }
}
