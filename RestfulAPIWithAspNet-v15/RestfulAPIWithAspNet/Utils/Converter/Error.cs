using System.Runtime.Serialization;

namespace UpBrasil.OTP.API.Utils
{
    public class Error
    {
        [IgnoreDataMember]
        public bool Localized { get; private set; }
    }
}