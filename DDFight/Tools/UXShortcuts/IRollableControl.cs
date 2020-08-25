using System.Windows;
using System.Windows.Input;

namespace DDFight.Tools.UXShortcuts
{
    public class RollableWindowTool
    {
        public static bool IsRollControlPressed(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.R &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) ||
                Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt) ||
                Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
            {
                return true;
            }
            return false;
        }

        public static void RollRollableChildren(FrameworkElement elem)
        {
            foreach (IRollableControl ctrl in elem.FindAllChildren<IRollableControl>())
                ctrl.RollControl();
        }
    }

    public interface IRollableControl
    {
        void RollControl();
    }
}
