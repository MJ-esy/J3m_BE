namespace J3m_BE.Extensions
{
    public static class IdListExtensions
    {
        // Remove non-positive and duplicate ids, return empty list if null or no valid ids (only keeping ID > 0)
        public static List<int> NormalizeIds(this IEnumerable<int>? ids) =>
            ids?.Where(id => id > 0).Distinct().ToList() ?? new List<int>();
    }
}
