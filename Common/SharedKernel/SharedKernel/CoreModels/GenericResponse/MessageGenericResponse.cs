namespace SharedKernel.CoreModels.GenericResponse
{
    public class MessageGenericResponse
    {
        public MessageGenericResponse(string message)
        {
            Message = message;
        }
        public string Message
        {
            get; set;
        }
    }
}
