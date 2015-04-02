using System;
using System.Globalization;
using Security.HMAC;

namespace Security.HOTP
{
    /// <summary>
    /*
     *  HOTP: An HMAC-Based One-Time Password Algorithm 
     * 
     * The HOTP class allow for the generation and
     * verification of one-time password using the
     * HOTP specified algorithm.
     *
     * For details about algorithm see: https://tools.ietf.org/html/rfc4226
     */
    /// </summary>
    public class HOTP : IHOTP
    {
        private readonly IHMAC _hmac;

        /// <summary>
        /// Number of digits returned by otp algorithm
        /// </summary>
        private const int Digits = 8;

        public HOTP(IHMAC hmac)
        {
            _hmac = hmac;
        }

        /// <summary>
        /// The HOTP algorithm is based on an increasing counter value 
        /// and a static symmetric key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public string GenerateOtp(byte[] key, long counter)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            byte[] hash = _hmac.Encode(key, GetCounterBytes(counter));

            int binaryCode = TruncateHash(hash);

            int otpCode = DoBinaryCodeReduction(binaryCode);

            return otpCode.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Put counter value into byte array
        /// </summary>
        /// <param name="counter"></param>
        private static byte[] GetCounterBytes(long counter)
        {
            var counterBytes = new byte[8];

            for (var i = counterBytes.Length - 1; i >= 0; i--)
            {
                counterBytes[i] = (byte)(counter & 0xff);
                counter >>= 8;
            }

            return counterBytes;
        }

        /// <summary>
        /// Reduction modulo 10^Digit
        /// </summary>
        /// <param name="binaryCode"></param>
        /// <returns></returns>
        private static int DoBinaryCodeReduction(int binaryCode)
        {
            return binaryCode % (int)Math.Pow(10, Digits);
        }

        /// <summary>
        /// Take 4 bytes from hash starting at offset bytes
        /// Discard the most significant bit and store the rest as an (unsigned) 32-bit integer
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static int TruncateHash(byte[] hash)
        {
            var truncationOffset = hash[hash.Length - 1] & 0xF;

            var binaryCode = ((hash[truncationOffset] & 0x7F) << 24) |
                             ((hash[truncationOffset + 1] & 0xFF) << 16) |
                             ((hash[truncationOffset + 2] & 0xFF) << 8) |
                             (hash[truncationOffset + 3] & 0xFF);

            return binaryCode;
        }
    }
}
