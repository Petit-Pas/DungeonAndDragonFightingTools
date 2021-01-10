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
        
        public static bool AreAllRollableChildrenRolled(FrameworkElement elem)
        {
            foreach (IRollableControl ctrl in elem.FindAllChildren<IRollableControl>())
                if (ctrl.IsFullyRolled() == false)
                    return false;
            return true;
        }
    }

    public interface IRollableControl
    {
        /// <summary>
        ///     Rolls any rollable child control
        /// </summary>
        void RollControl();

        /// <summary>
        ///     tells wether there is still any rollable control that hasnt been rolled
        /// </summary>
        /// <returns></returns>
        bool IsFullyRolled();
    }
}
