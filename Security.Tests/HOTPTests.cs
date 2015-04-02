using System;
using NUnit.Framework;
using Security.HMAC;

namespace Security.Tests
{
    public class HOTPTests
    {
        private HOTP.HOTP _sut;

        readonly byte[] _keyBytes = { 0, 0, 0, 1 };

        [SetUp]
        public void BeforeEach()
        {
            _sut = new HOTP.HOTP(new HMACSHA1());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateOtp_WhenKeyIsNull_ExceptionIsRaised()
        {
            _sut.GenerateOtp(null, 0);
        }

        [Test]
        public void GenerateOtp_When2OTPGeneratedWithSameKeyAndCounter_ReturnsSameOTPCodes()
        {
            string otpCode1 = _sut.GenerateOtp(_keyBytes, 1);
            string otpCode2 = _sut.GenerateOtp(_keyBytes, 1);

            Assert.AreEqual(otpCode1, otpCode2);
        }

        [Test]
        public void GenerateOtp_When2OTPGeneratedWithSameCounter_ButDifferentKey_ReturnedOTPCodesAreDifferent()
        {
            var differentKeyBytes = new byte[] { 0, 0, 0, 2 };

            string otp1 = _sut.GenerateOtp(_keyBytes, 1);
            string otp2 = _sut.GenerateOtp(differentKeyBytes, 1);

            Assert.AreNotEqual(otp1, otp2);
        }

        [Test]
        public void GenerateOtp_When2OTPGeneratedAndCounterDifferenceIsOne_OTPCodesMatchingIsValid()
        {
            string otpCode = _sut.GenerateOtp(_keyBytes, 1);

            Assert.IsTrue(ValidateOtpCodeMatching(otpCode, 2));
        }

        [Test]
        public void GenerateOtp_When2OTPGeneratedAndCounterDifferenceIs2_OTPCodesMatchingIsInvalid()
        {
            string otpCode = _sut.GenerateOtp(_keyBytes, 1);

            Assert.IsFalse(ValidateOtpCodeMatching(otpCode, 3));
        }

        private bool ValidateOtpCodeMatching(string otpCode, int steps)
        {
            for (var step = steps - 1; step < steps + 1; step++)
            {
                if (_sut.GenerateOtp(_keyBytes, step)
                    .Equals(otpCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
