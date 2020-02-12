using System;

namespace DDFight.Windows
{
    public class AskYesNoDataContext
    {
        /// <summary>
        ///     The message to be displayed in the window
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        ///     True if the user clicked yes, false in any other way
        /// </summary>
        public bool Yes = false;
    }
}
