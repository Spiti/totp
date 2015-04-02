using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Security.Hashing;
using Security.TOTP;

namespace Security.Tests
{
    [TestFixture]
    public class UserCredentialServiceTests
    {
        private UserCredentialService _sut;

        private Mock<ITOTP> _totpMock;
        private Mock<IHasing> _hasingMock;

        [SetUp]
        public void BeforeEach()
        {
            _totpMock = new Mock<ITOTP>();
            _hasingMock = new Mock<IHasing>();

            _sut = new UserCredentialService(_totpMock.Object, _hasingMock.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GeneratePassword_WhenUsedIdIs0_ThrowsException()
        {
            _sut.GeneratePassword(0, DateTime.Now);
        }

        [Test]
        public void GeneratePassword_ReturnsOTPPasswordUsingHashedUserId_AndInputDate()
        {
            var now = DateTime.Now;
            byte[] hasedBytes = { 10, 20, 30, 40, 50 };

            _hasingMock.Setup(c => c.Encode(It.IsAny<byte[]>()))
                .Returns(hasedBytes);

            _sut.GeneratePassword(123, now);

            _totpMock.Verify(c => c.GenerateOtp(hasedBytes, now));
        }
    }
}
