using System.Collections.Generic;

namespace RestfulAPIWithAspNet.Utils.Converter
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> ParseList(List<O> origin);
    }
}