# totp
TOTP Generation base on time and user identification

Time-based One-time Password Algorithm which computes a one-time password from shared key (in our case, the user id) and a DateTime.

TOTP is an example of a hash-based message authentication code (HMAC). It combines a secret key with the current timestamp using a cryptographic hash function to generate a one-time password. The timestamp typically increases in 30-second intervals, so passwords generated close together in time from the same secret key will be equal.


TOTP is based on HOTP with a timestamp replacing the incrementing counter.

It has been adopted as Internet Engineering Task Force standard RFC 6238.(RFC 4226 for HOTP)

For more information please visit:
Wikipedia: http://en.wikipedia.org/wiki/Time-based_One-time_Password_Algorithm
RFC: http://tools.ietf.org/html/rfc6238

## Requirements

* Microsoft .NET Framework 4.0

## Usage

### TOTP algorithm uses HOTP
        
    public string GenerateOtp(byte[] key, DateTime time)
    {
        if (time < UnixTime.UnixEpochDate)
            throw new ArgumentOutOfRangeException("time", "Time cannot be less than unix epoch");

        var stepsSinceUnixEpoch = (long)(UnixTime.GetUnixTicks(time) / DefaultTimeStep);

        return _hotp.GenerateOtp(key, stepsSinceUnixEpoch);
    }

### Call TOTP service

    string otp = _totp.GenerateOtp(keyBytes, dateTime);

### Call generate password service

    var password GeneratePassword(userId, dateTime)


## Source Code

Source code can be found on Github:

https://github.com/Spiti/totp

## About

.NET Library using C# programming language. It implements a clear form of TOTP algorithm.
It includes a console application in order to do a quick password generation using the TOTP algorithm.
The project is formed by small, clear and concise steps. The code is unit tested and helps you understand
the TOTP algorithm strategy.



