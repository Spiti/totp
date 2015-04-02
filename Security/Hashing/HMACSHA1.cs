
namespace Security.HMAC
{
    public class HMACSHA1 : IHMAC
    {
        public byte[] Encode(byte[] key, byte[] buffer)
        {
            var hmac = new System.Security.Cryptography.HMACSHA1(key);

            return hmac.ComputeHash(buffer);
        }
    }
}