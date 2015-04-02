using System;

namespace Security
{
    public class UnixTime
    {
        public static readonly DateTime UnixEpochDate =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetUnixTicks(DateTime date)
        {
            return (date.ToUniversalTime() - UnixEpochDate).TotalSeconds;
        }
    }
}