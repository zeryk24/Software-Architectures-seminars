namespace FoodDelivery.Contracts.Utils;

//Example of "necessary shit"
public static class IdConverter
{
    public static Guid IntToGuid(int value)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public static int GuidToInt(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
}