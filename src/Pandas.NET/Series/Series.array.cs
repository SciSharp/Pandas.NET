namespace PandasNet;

public partial class Series
{
    public T[] array<T>()
    {
        return _data as T[];
    }
}
