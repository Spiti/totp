using System;
using System.Linq;
using NUnit.Framework;

namespace Security.Tests
{
    [TestFixture]
    public class UnixTimeTests
    {
        [Test]
        public void GetUnixTicks_WhenTimeIsLocal_ItIsChangedToUtc()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var localTime = ChangeTimeToLocal(unixEpoch);

            Assert.AreEqual(0, UnixTime.GetUnixTicks(localTime));
        }

        [Test]
        public void GetUnixTicks_ReturnsPassedTicks()
        {
            var unixEpoch = new DateTime(2016, 6, 13, 16, 18, 33, DateTimeKind.Utc);

            Assert.AreEqual(1465834713, UnixTime.GetUnixTicks(unixEpoch));
        }

        private static DateTime ChangeTimeToLocal(DateTime unixEpoch)
        {
            var currentTimeZone = TimeZone.CurrentTimeZone;
            var currentTimeZoneInfo =
                TimeZoneInfo.GetSystemTimeZones()
                .First(t => t.StandardName == currentTimeZone.StandardName);

            return TimeZoneInfo.ConvertTimeFromUtc(unixEpoch, currentTimeZoneInfo);
        }
    }
}