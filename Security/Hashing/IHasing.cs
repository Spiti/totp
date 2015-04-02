namespace Security.Hashing
{
    public interface IHasing
    {
        byte[] Encode(byte[] buffer);
    }
}