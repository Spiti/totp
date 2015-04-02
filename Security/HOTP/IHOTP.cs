namespace Security.HOTP
{
    public interface IHOTP
    {
        /// <summary>
        /// The HOTP algorithm is based on an increasing counter value 
        /// and a static symmetric key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        string GenerateOtp(byte[] key, long counter);
    }
}