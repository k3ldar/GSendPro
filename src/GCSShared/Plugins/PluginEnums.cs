namespace GSendShared.Plugins
{
    [Flags]
    public enum PluginUsage
    {
        None = 0,

        Editor = 1,

        Sender = 2,

        Service = 4
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

        Help,
    }

    public enum MenuType
    {
        Seperator,

        MenuItem
    }
}
