using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Attacks.HitAttacks;

using WpfDnDCustomControlLibrary.Attacks.HitAttacks;
using WpfToolsLibrary.Extensions;

namespace DDFight.WpfExtensions
{
    public static class OpenExecuteWindowExtensions
    {
        private static ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private static IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        public static void ExecuteHitAttack(this HitAttackTemplate template)
        {
            HitAttackExecuteWindow window = new HitAttackExecuteWindow() { DataContext = template, };
            window.ShowCentered();
        }
    }
}
