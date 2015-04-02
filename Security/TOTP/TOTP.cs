using Security.HOTP;
using System;

namespace Security.TOTP
{
    /// <summary>
    /*
     * HOTP: An HMAC-Based One-Time Password Algorithm
     * 
     * TOTP is the time-based variant of this algorithm, where a value T,
     * derived from a time reference and a time step, replaces the counter C
     * in the HOTP computation.
     * 
     * For more info see: https://tools.ietf.org/html/rfc6238
     */
    /// </summary>
    public class TOTP : ITOTP
    {
        /// <summary>
        /// Represents the time step in seconds
        /// </summary>
        private const int DefaultTimeStep = 30;

        private readonly IHOTP _hotp;

        public TOTP(IHOTP hotp)
        {
            _hotp = hotp;
        }

        /// <summary>
        /// TOTP = HOTP(K, T), where T is an integer and represents the number of time 
        /// steps between the initial countertime T0 and the current Unix time.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GenerateOtp(byte[] key, DateTime time)
        {
            if (time < UnixTime.UnixEpochDate)
                throw new ArgumentOutOfRangeException("time", "Time cannot be less than unix epoch");

            var stepsSinceUnixEpoch = (long)(UnixTime.GetUnixTicks(time) / DefaultTimeStep);

            return _hotp.GenerateOtp(key, stepsSinceUnixEpoch);
        }
    }
}