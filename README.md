# totp
TOTP Generation base on time and user identification

Time-based One-time Password Algorithm which computes a one-time password from shared key(in our case, the user id) and a DateTime
.

TOTP is an example of a hash-based message authentication code (HMAC). It combines a secret key with the current timestamp using a cryptographic hash function to generate a one-time password. The timestamp typically increases in 30-second intervals, so passwords generated close together in time from the same secret key will be equal.


TOTP is based on HOTP with a timestamp replacing the incrementing counter.

It has been adopted as Internet Engineering Task Force standard RFC 6238.(RFC 4226 for HOTP)

For more information please vizit:
Wikipedia: http://en.wikipedia.org/wiki/Time-based_One-time_Password_Algorithm
RFC: http://tools.ietf.org/html/rfc6238
