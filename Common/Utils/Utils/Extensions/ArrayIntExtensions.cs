namespace Utils.Extensions
{
    public static class ArrayIntExtensions
    {
        public static bool IsSecuence(this int[] values)
        {
            if (values.Length == 0) return false;
            var p = values.First();
            IEnumerable<int> secuence = Enumerable.Range(p, values.Length);
            return secuence.SequenceEqual(values);
        }
    }
}
