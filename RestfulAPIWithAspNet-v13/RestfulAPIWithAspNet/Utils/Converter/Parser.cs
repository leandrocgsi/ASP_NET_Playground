using System.Collections.Generic;

namespace UpBrasil.OTP.API.Utils
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        /*List<D> ParseVOListToEntityList(List<O> origin);
        List<D> ParseEntityListToVOList(List<O> origin);*/
    }

    public abstract class Parser<R>
    {
        public Error _Error { get; private set; }
        public R _Data { get; private set; }

        public bool IsOk
        {
            get
            {
                return _Error == null;
            }
        }

        public bool HasError
        {
            get
            {
                return !IsOk;
            }
        }

        protected Parser(R data)
        {
            _Data = data;
        }

        protected Parser(Error error)
        {
            _Error = error;
        }
    }
}