namespace BaseToolsLibrary
{
    public interface IEquivalentComparable<T>
        where T : class
    {
        bool IsEquivalentTo(T toCompare);
    }
}
