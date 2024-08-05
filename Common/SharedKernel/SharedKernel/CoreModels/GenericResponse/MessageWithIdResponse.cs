namespace SharedKernel.CoreModels.GenericResponse
{
    public class MessageWithIdResponse
    {
        public MessageWithIdResponse(string message, Guid id)
        {
            Message = message;
            Id = id;
        }
        public string Message
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }
    }
}
