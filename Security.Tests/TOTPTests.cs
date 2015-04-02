using Moq;
using NUnit.Framework;
using Security.HOTP;
using System;

namespace Security.Tests
{
    public class TOTPTests
    {
        private TOTP.TOTP _sut;
        private Mock<IHOTP> _hotpMock;

        private readonly byte[] _keyBytes = { 0, 0, 0, 1 };

        [SetUp]
        public void BeforeEach()
        {
            _hotpMock = new Mock<IHOTP>();
            _sut = new TOTP.TOTP(_hotpMock.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GenerateOtp_WhenTimeIsLessThanEpochTime_ThrowsAndException()
        {
            _sut.GenerateOtp(_keyBytes, DateTime.MinValue);
        }

        [Test]
        public void GenerateOtp_UnixTicksAreDevided_ByDefaultTimeStep()
        {
            var time = new DateTime(1970, 1, 1, 0, 1, 15, DateTimeKind.Utc);

            _sut.GenerateOtp(_keyBytes, time);

            _hotpMock.Verify(c => c.GenerateOtp(_keyBytes, 2));
        }
    }
}