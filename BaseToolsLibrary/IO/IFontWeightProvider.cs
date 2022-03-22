namespace BaseToolsLibrary.IO
{
    public interface IFontWeightProvider
    {
        IFontWeight Bold { get; }
        IFontWeight SemiBold { get; }
        IFontWeight Normal { get; }
    }
}
