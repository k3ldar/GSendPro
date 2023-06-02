using GSendShared;

namespace GSendControls
{
    public class WarningErrorList
    {
        public string Message { get; set; }

        public bool MarkedForRemoval { get; set; }

        public bool IsNew { get; set; }

        public InformationType InfoType { get; set; }
    }
}
