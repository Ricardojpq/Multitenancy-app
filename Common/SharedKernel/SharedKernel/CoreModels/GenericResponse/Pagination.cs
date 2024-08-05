namespace SharedKernel.CoreModels.GenericResponse
{
    public static class Pagination
    {
        public static PagedListResponse<TResult> ToPagedList<TResult>(this IQueryable<TResult> collection, int start, int limit)
        {
            return new PagedListResponse<TResult>
            {
                data = collection.Skip(start).Take(limit),
                totalItems = collection.Select(x => 1).ToList().Count
            };
        }
    }
}
