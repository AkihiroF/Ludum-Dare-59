using System;

namespace Utils
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static int ToInt(this Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }

        public static uint ToUint(this Guid value)
        {
            byte[] bytes = value.ToByteArray();
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}