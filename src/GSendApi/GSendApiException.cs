namespace GSendApi
{
    //#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public sealed class GSendApiException : Exception
    {
        public GSendApiException()
        {
        }

        public GSendApiException(string message)
            : base(message)
        {
        }

        public GSendApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    //#pragma warning restore S3925 // "ISerializable" should be implemented correctly
}
