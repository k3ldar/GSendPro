namespace GSendShared.Plugins
{
    /// <summary>
    /// Usage of plugin, describes which host it is allowed to be applied to
    /// </summary>
    [Flags]
    public enum PluginHosts
    {
        None = 0,

        Editor = 1,

        SenderHost = 2,

        Sender = 4,

        Service = 8,

        Any = Editor | SenderHost | Sender | Service,
    }

    /// <summary>
    /// Plugin options
    /// </summary>
    [Flags]
    public enum PluginOptions
    {
        None = 0,

        HasMenuItems = 1,

        HasWorkArea = 2,

        HasToolbarButtons = 4,

        MessageReceived = 8,
    }

    /// <summary>
    /// Available parent menu items
    /// </summary>
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

    /// <summary>
    /// Type of menu that can be created
    /// </summary>
    public enum MenuType
    {
        Seperator,

        MenuItem
    }

    /// <summary>
    /// Type of toolbar button that can be created
    /// </summary>
    public enum ButtonType
    {
        Seperator,

        Button
    }
}
