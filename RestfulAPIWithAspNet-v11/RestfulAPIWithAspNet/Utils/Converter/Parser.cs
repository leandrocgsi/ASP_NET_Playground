namespace UpBrasil.OTP.API.Utils
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
    }
}