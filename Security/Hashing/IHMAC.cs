namespace Security.HMAC
{
    public interface IHMAC
    {
        byte[] Encode(byte[] key, byte[] buffer);
    }
}