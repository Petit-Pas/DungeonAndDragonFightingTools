using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.Diagnostics;

namespace TempExtensionsOnHitStatus
{
    public static class OnHitStatusGameExtension
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

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
            Trace.WriteLine("WARNING OnHitStatusGameExtensions.Apply(OnHitStatus) is still being called");
        }
    }
}
