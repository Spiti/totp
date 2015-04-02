using Security.Hashing;
using Security.TOTP;
using System;

namespace Security
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly ITOTP _totp;
        private readonly IHasing _hasing;

        public UserCredentialService(ITOTP totp, IHasing hasing)
        {
            _totp = totp;
            _hasing = hasing;
        }

        /// <summary>
        /// Generate a one time used password base on user ID and input time
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GeneratePassword(int userId, DateTime dateTime)
        {
            if (userId == 0)
                throw new ArgumentException("ID cannot be 0", "userId");

            byte[] userIdBytes = BitConverter.GetBytes(userId);

            return _totp.GenerateOtp(_hasing.Encode(userIdBytes), dateTime);
        }
    }
}
