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

        SpeedOverride = 16384,

        DoNotProcess = 32768,
    }

    public enum UnitOfMeasurement
    {
        None = 0,

        Inch = 1,

        MM = 2,

        Error = 4,
    }

    public enum CommandSent
    {
        CoolantOff,

        MistOn,

        FloodOn,

        SpindleSpeedSet,

        SpindleOff,

        Unlock,

        Home,

        ZeroAxis,
    }

    [Flags]
    public enum Axis : byte
    {
        X = 1,

        Y = 2,

        Z = 4
    }

    /// <summary>
    /// GRBL Error codes
    /// </summary>
    public enum GrblError
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// G-code words consist of a letter and a value. Letter was not found.
        /// </summary>
        LetterNotFound = 1,

        /// <summary>
        /// Missing the expected G-code word value or numeric value format is not valid
        /// </summary>
        BadNumberFormat = 2,

        /// <summary>
        /// Grbl ‘$’ system command was not recognized or supported.
        /// </summary>
        InvalidStatement = 3,

        /// <summary>
        /// Negative value received for an expected positive value.
        /// </summary>
        NegativeValue = 4,

        /// <summary>
        /// Homing cycle failure. Homing is not enabled via settings.
        /// </summary>
        HomingDisabled = 5,

        /// <summary>
        /// An EEPROM read failed. Auto-restoring affected EEPROM to default values.
        /// </summary>
        EEPromReadFail = 7,

        /// <summary>
        /// Grbl ‘$’ command cannot be used unless Grbl is IDLE. Ensures smooth operation during a job.
        /// </summary>
        NotIdle = 8,

        /// <summary>
        /// G-code commands are locked out during alarm or jog state.
        /// </summary>
        Locked = 9,

        /// <summary>
        /// Soft limits cannot be enabled without homing also enabled.
        /// </summary>
        HomingNotEnabled = 10,

        /// <summary>
        /// Max characters per line exceeded. File most likely formatted improperly
        /// </summary>
        LineOverflow = 11,

        /// <summary>
        /// Build info or startup line exceeded EEPROM line length limit. Line not stored.
        /// </summary>
        LineTooLong = 14,

        /// <summary>
        /// Jog target exceeds machine travel. Jog command has been ignored.
        /// </summary>
        TravelExceeded = 15,

        /// <summary>
        /// Laser mode requires PWM output.
        /// </summary>
        SettingDisabled = 17,

        /// <summary>
        /// Unsupported or invalid g-code command found in block. This usually means that you used the wrong 
        /// Post-Processor to make your file, or that some incompatible code within needs to be manually deleted.
        /// </summary>
        UnsupportedCommand = 20,

        /// <summary>
        /// More than one g-code command from same modal group found in block.
        /// </summary>
        ModalGroupViolation = 21,

        /// <summary>
        /// Feed rate has not yet been set or is undefined.
        /// </summary>
        UndefinedFeedRate = 22,

        /// <summary>
        /// Error codes 23-38 all give different reasons for a file failing. To look up further, google "GRBL Error Code [Insert Number] for more information.
        /// </summary>
        InvalidGCode = 23,
    }

    public enum CommandStatus
    {
        Undefined = 0,

        Sent = 1,

        Failed = 2,

        Processed = 3,
    }
}
