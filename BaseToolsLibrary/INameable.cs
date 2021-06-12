namespace BaseToolsLibrary
{
    /// <summary>
    ///     to be sure to have a name to bind to
    /// </summary>
    public interface INameable
    {
        string DisplayName { get; set; }
        string Name { get; set; }
    }
}
