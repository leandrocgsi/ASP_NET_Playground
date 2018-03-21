using System.Collections.Generic;

namespace UpBrasil.OTP.API.Utils
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        /*List<D> ParseVOListToEntityList(List<O> origin);*/
    }
}