namespace BaseToolsLibrary.IO
{
    public interface IFontColorProvider
    {
        void AddKey(string key, IFontColor color);
        IFontColor GetColorByKey(string key);
        IFontColor GetDefault();

        IFontColor Success { get; }
        IFontColor Failure { get; }

        void SetDefault(IFontColor color);
    }
}
