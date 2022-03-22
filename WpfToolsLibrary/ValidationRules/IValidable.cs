using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfToolsLibrary.Extensions;

namespace WpfToolsLibrary.ValidationRules
{
    public static class ValidableWindowExtensions
    {
        public static bool IsValidateControlPressed(this FrameworkElement _, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) ||
                Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt) ||
                Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
            {
                return true;
            }
            return false;
        }

        public static bool AreAllValidableChildrenValid(this FrameworkElement elem)
        {
            foreach (IValidable ctrl in elem.FindAllChildren<IValidable>())
                if (ctrl.IsValid() == false)
                    return false;
            return true;
        }

        /*public static List<string> GetAllValidationErrors(this FrameworkElement elem)
        {
            List<string> result = new List<string>();
            foreach (IValidable ctrl in elem.FindAllChildren<IValidable>())
                if (ctrl.IsValid() == false)
                    result.Add(ctrl.GetValidationErrorMessage());
            return result;
        }*/
    }

    public static class IValidableExtensions
    { 
        public static string GetFirstValidationErrorMessage(this FrameworkElement elem)
        {
            if (elem is IValidable validable)
            {
                if (!validable.IsValid())
                    return Validation.GetErrors(elem)[0].ErrorContent.ToString();
            }
            return null;
        }
    }

    /// <summary>
    ///     Interface for controls with a validationRule
    /// </summary>
    public interface IValidable
    {
        //TODO should provide a list of errors for easier debugging

        /// <summary>
        ///     Returns the last ValidationRule output computed
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        //string GetValidationErrorMessage();

    }
}
