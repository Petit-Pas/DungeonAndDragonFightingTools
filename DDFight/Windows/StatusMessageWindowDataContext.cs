using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DDFight.Windows
{
    public class StatusMessageWindowDataContext
    {
        /// <summary>
        ///     The icon to display
        /// </summary>
        public ImageSource Icon { get; set; }

        /// <summary>
        ///     The message to prompt
        /// </summary>
        public String Message { get; set; }
    }
}
