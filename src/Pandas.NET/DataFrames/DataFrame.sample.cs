using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using Tensorflow.Util;

namespace PandasNet;

public partial class DataFrame
{
    public DataFrame sample(int n = 0, float frac = 0, int random_state = 0, bool replace = false)
    {
        if (n == 0 && frac == 0)
        {
            throw new ArgumentException("Either n or frac should be greater than 0");
        }
        if (n != 0 && frac != 0)
        {
            throw new ArgumentException("Only one of n or frac should be greater than 0");
        }
        if (frac > 0)
        {
            n = (int)Math.Ceiling(frac * _index.size);
        }
        if (n > _index.size)
        {
            throw new ArgumentException("n should be less than the size of the DataFrame");
        }

        // treat axis as 0 for now.  support for axis=1 should be added in the future
        var rnd = new Random(random_state);

        // make a list that we can sample from
        List<int> sampleIndex = null;
        
        if(!replace){
            // randomize the index and take the first n elements, no duplicates
            sampleIndex = Enumerable
                .Range(0, _index.size)
                .OrderBy(arg => rnd.Next())
                .Take(n).ToList();
        }
        else{
            // for each sample, randomly select an index allowing duplicates
            var sampleIndexes = Enumerable.Range(0, _index.size);
            for (int i = 0; i < n; i++)
            {
                sampleIndex.Add(sampleIndexes.ElementAt(rnd.Next(0, sampleIndexes.Count()-1)));
            }
        }

        // initialize a dictionary to hold the data
        Dictionary<Column, ArrayList> data = new Dictionary<Column, ArrayList>();
        foreach (var s in _data)
        {
            // init the array based on the dtype
            ArrayList array =new ArrayList();
            data.Add(s.column, array);
        }

        // fill the arrays with the sampled data
        for (int i = 0; i < sampleIndex.Count; i++)
        {
            foreach (var s in _data)
            {
                data[s.column].Add(s.data.GetValue(sampleIndex[i]));
            }
        }

        // create a new DataFrame with the sampled data
        DataFrame df = new DataFrame(data.Select(x => new Series(x.Value.ToArray(x.Key.DType), x.Key)).ToList(), index: new Series(sampleIndex.ToArray()));
        return df;

    }
}
