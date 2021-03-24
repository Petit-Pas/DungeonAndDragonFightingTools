namespace WpfToolsLibrary.ValidationRules
{ 
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
    }
}
