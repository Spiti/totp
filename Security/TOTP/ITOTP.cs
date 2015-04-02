using System;

namespace Security.TOTP
{
    public interface ITOTP
    {
        /// <summary>
        /// TOTP = HOTP(K, T), where T is an integer and represents the number of time 
        /// steps between the initial countertime T0 and the current Unix time.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        string GenerateOtp(byte[] key, DateTime time);
    }
}