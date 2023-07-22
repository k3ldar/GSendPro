namespace GSendShared.Models
{
    public sealed class InformationMessageModel
    {
        public InformationMessageModel(InformationType informationType, string message)
        {
            InformationType = informationType;
            Message = message;
        }

        public InformationType InformationType { get; set; }

        public string Message { get; set; }
    }
}
