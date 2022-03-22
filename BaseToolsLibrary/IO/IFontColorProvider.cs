namespace BaseToolsLibrary.IO
{
    public interface IFontColorProvider
    {
        void AddKey(string key, IFontColor color);
        IFontColor GetColorByKey(string key);
        IFontColor GetDefault();
        void SetDefault(IFontColor color);
    }
}
