namespace GSendCommon.Settings
{
    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Kestrel
    {
        public Endpoints Endpoints { get; set; }
    }

    public class Endpoints
    {
        public HTTP HTTP { get; set; }
        public HTTPS HTTPS { get; set; }
    }

    public class HTTP
    {
        public Uri Url { get; set; }
    }

    public class HTTPS
    {
        public Uri Url { get; set; }
    }
}
