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
