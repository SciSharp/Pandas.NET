using System;

namespace PandasNet;

public partial class Series
{
    public Series map<Tin, Tout>(Func<Tin, Tout> func)
    {
        var data = new Tout[size];
        for (int i = 0; i < size; i++)
        {
            data[i] = func((Tin)_data.GetValue(i));
        }
        return new Series(data, column);
    }
}
