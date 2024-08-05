namespace SharedKernel.CoreModels.GenericResponse
{
    /// <summary>
    ///     Lista paginada
    /// </summary>
    /// <typeparam name="TResult">
    ///     Tipo de la lista paginada
    /// </typeparam>
    public class PagedListResponse<TResult>
    {
        /// <summary>
        ///     Constructor Vacio
        /// </summary>
        public PagedListResponse()
        {
        }
        /// <summary>
        ///     Informacion de la lista paginada
        /// </summary>
        public IEnumerable<TResult> data
        {
            get; set;
        }
        /// <summary>
        ///     Número de registros de la lista
        /// </summary>
        public int totalItems
        {
            get; set;
        }
    }

}
