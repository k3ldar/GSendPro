namespace GSendShared
{
    public enum MachineType
    {
        Unspecified,

        CNC,

        Laser,

        Printer,
    }

    [Flags]
    public enum MachineOptions
    {
        None = 0,

        LimitSwitches = 1,

        CanHome = 2,
    }

    [Flags]
    public enum CommandAttributes : uint
    {
        /// <summary>
        /// No specific attributes
        /// </summary>
        None = 0,

        /// <summary>
        /// Duplicate of previous command
        /// </summary>
        Duplicate = 1,

        /// <summary>
        /// Command contains a value for setting spindle speed
        /// </summary>
        SpindleSpeed = 2,

        /// <summary>
        /// Moves to safe Z
        /// </summary>
        SafeZ = 4,

        /// <summary>
        /// Moves to Home z
        /// </summary>
        HomeZ = 8,

        /// <summary>
        /// Moves to home
        /// </summary>
        Home = 16,

        /// <summary>
        /// Value applied to feed rate is in error
        /// </summary>
        FeedRateError = 32,

        /// <summary>
        /// Value applied to spindle speed is in error
        /// </summary>
        SpindleSpeedError = 64,

        /// <summary>
        /// Value applied to X/Y/Z movement is an error
        /// </summary>
        MovementError = 128,

        MovementZUp = 256,

        MovementZDown = 512,

        MovementXMinus = 1024,

        MovementXPlus = 2048,

        MovementYMinus = 4096,

        MovementYPlus = 8192,
    }

    public enum UnitOfMeasurement
    {
        None = 0,

        Inch = 1,

        MM = 2,

        Error = 4,
    }
}
