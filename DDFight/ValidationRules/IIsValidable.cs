using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.ValidationRules
{
    /// <summary>
    ///     Interface for controls with a validationRule
    /// </summary>
    interface IValidable
    {
        /// <summary>
        ///     Returns the last ValidationRule output computed
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }
}
