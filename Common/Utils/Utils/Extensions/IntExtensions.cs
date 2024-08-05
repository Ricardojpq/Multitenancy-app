namespace Utils.Extensions
{
    public static class IntExtensions
    {
        public static string ToEmptyIfZero(this int val)
        {
            if (val == 0)
                return string.Empty;

           return val.ToString();
        }

    }
}
