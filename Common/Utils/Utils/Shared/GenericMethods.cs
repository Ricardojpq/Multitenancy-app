namespace Utils.Shared
{
    public class GenericMethods
    {
        public bool CheckDateOverlapping(DateTime from1, DateTime? to1, DateTime from2, DateTime? to2)
        {
            bool overlaps = false;

            var effDateInRange = from1 >= from2 && (!to1.HasValue || from2 <= to1.Value) && (to2 >= from1);
            var rangeOverlap = (from2 <= from1 && (!to2.HasValue || to2.Value >= from1))
                || (from2 >= from1 && (!to1.HasValue || to1.Value > to2));

            var effDateEqto1 = (!to1.HasValue || from2 <= to1.Value) && (to2 >= from1);

            if (effDateInRange || rangeOverlap || effDateEqto1)
            {
                return true;
            }

            //return object with Success: true or false for overlap and Message: message indicating which date range conflicts with the <param> entity </param> date range
            return overlaps;
        }
    }
}
