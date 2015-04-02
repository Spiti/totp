using System.Security.Cryptography;

namespace Security.Hashing
{
    public class SHA1 : IHasing
    {
        public byte[] Encode(byte[] buffer)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            return sha1.ComputeHash(buffer);
        }
    }
}
