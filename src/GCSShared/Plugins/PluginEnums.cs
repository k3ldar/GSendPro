namespace GSendShared.Plugins
{
    [Flags]
    public enum PluginUsage
    {
        None = 0,

        Editor = 1,

        SenderHost = 2,

        Sender = 4,

        Service = 8,

        Any = Editor | SenderHost | Sender | Service,
    }

    [Flags]
    public enum PluginOptions
    {
        None = 0,

        HasMenuItems = 1,

        HasWorkArea = 2,

        HasToolbarButtons = 4,

        MessageReceived = 8,
    }

    public enum MenuParent
    {
        None,

        File,

        Edit,

        View,

        Machine,

        Options,

        Tools,

        Action,

        Subprograms,

        Help,
    }

    public enum MenuType
    {
        Seperator,

        MenuItem
    }
}
