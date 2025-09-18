namespace API_Inventario.Utils.Objects
{
    public class CreateSuccessResponse<T>
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public T Data { get; set; }
    }
}
