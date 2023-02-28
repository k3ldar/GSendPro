namespace GSendApi
{
    public sealed class GSendApiException : Exception
    {
        public GSendApiException()
        {
        }

        public GSendApiException(string message)
            : base(message)
        {
        }
    }
}
