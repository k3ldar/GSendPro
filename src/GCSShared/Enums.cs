namespace GSendShared
{
    [Flags]
    public enum MachineStateOptions
    {
        None = 0,

        G54ZeroZSet = 1,

        G54ZeroXSet = 2,

        G54ZeroYSet = 4,

        G55ZeroZSet = 8,

        G55ZeroXSet = 16,

        G55ZeroYSet = 32,

        G56ZeroZSet = 64,

        G56ZeroXSet = 128,

        G56ZeroYSet = 256,

        G57ZeroZSet = 512,

        G57ZeroXSet = 1024,

        G57ZeroYSet = 2048,

        G58ZeroZSet = 4096,

        G58ZeroXSet = 8192,

        G58ZeroYSet = 16384,

        G59ZeroZSet = 32768,

        G59ZeroXSet = 65536,

        G59ZeroYSet = 131072,

        SimulationMode = 262144,
    }

    [Flags]
    public enum AnalysesOptions
    {
        None = 0,

        ContainsDuplicates = 1,

        HasEndProgram = 2,

        HasCommandAfterEnd = 4,

        UsesMistCoolant = 8,

        UsesFloodCoolant = 16,

        TurnsOffCoolant = 32,

        ContainsToolChanges = 64,

        ContainsAutomaticToolChanges = 128,

        ContainsCRLF = 256,

        ContainsCoordinateCommands = 512,

        InvalidGCode = 1024,

        MultipleJobNames = 2048,

        InvalidJobName = 4096,
    }

    public enum ServiceType
    {
        Daily = 0,

        Minor = 1,

        Major = 2
    }

    public enum SpindleType
    {
        Integrated = 0,

        VFD = 1,

        External = 2
    }

    public enum CoordinateSystem
    {
        G54,
        G55,
        G56,
        G57,
        G58,
        G59,
    }

    public enum InformationType
    {
        Alarm = 0,

        Error = 1,

        Warning = 2,

        Information = 3,

        ErrorKeep = 4,
    }

    public enum ConnectResult
    {
        Success = 0,

        AlreadyConnected = 1,

        TimeOut = 2,

        Error = 3,
    }

    public enum UpdateSettingResult
    {
        Success = 0,

        Failed = 1,

        Timeout = 2,

        Error = 3
    }

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
        /// <summary>
        /// No options available
        /// </summary>
        None = 0,

        /// <summary>
        /// Machine has limit switches
        /// </summary>
        LimitSwitches = 1,

        /// <summary>
        /// Spindle soft start/stop
        /// </summary>
        SoftStart = 2,

        /// <summary>
        /// Spindle default to counter clock wise 
        /// </summary>
        SpindleCounterClockWise = 4,

        /// <summary>
        /// Machine has a tool changer
        /// </summary>
        ToolChanger = 8,

        /// <summary>
        /// maintain a service schedule
        /// </summary>
        ServiceSchedule = 16,

        /// <summary>
        /// Has mist coolant
        /// </summary>
        MistCoolant = 32,

        /// <summary>
        /// Has flood coolant
        /// </summary>
        FloodCoolant = 64,

        /// <summary>
        /// Will autocorrect between laser and spindle $31
        /// </summary>
        AutoCorrectLaserSpindleMode = 128,

        /// <summary>
        /// Automatically selects the correct display based on gcode file
        /// </summary>
        AutoUpdateDisplayFromFile = 256,

        /// <summary>
        /// Override slider for X is linked
        /// </summary>
        OverrideRapids = 512,

        /// <summary>
        /// Override slider for Y is linked
        /// </summary>
        OverrideXY = 1024,

        /// <summary>
        /// Override slider for Z Up is linked
        /// </summary>
        OverrideZUp = 2048,

        /// <summary>
        /// Override slider for Z Down is linked
        /// </summary>
        OverrideZDown = 4096,

        /// <summary>
        /// Override slider for spindle is linked
        /// </summary>
        OverrideSpindle = 8192,

        /// <summary>
        /// Provides a warning if layer height is greater than set amount
        /// </summary>
        LayerHeightWarning = 16384,
    }

    public enum RapidsOverride
    {
        Low = 0,

        Medium = 1,

        High = 2,
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

        AllowSpeedOverride = 16384,

        /// <summary>
        /// Use rapid rate, should not be overridden for speeds
        /// </summary>
        UseRapidRate = 32768,

        EndProgram = 65536,

        StartProgram = 131072,

        ToolChange = 262144,

        UnconditionalStop = 524288,

        Arc = 1048576,

        Dwell = 2097152,

        SubProgram = 4194304,

        InvalidLineTooLong = 8388608,

        Extrude = 16777216,

        ContainsVariables = 33554432,

        InvalidCommentTooLong = 67108864,
    }

    public enum UnitOfMeasurement
    {
        None = 0,

        Inch = 1,

        Mm = 2,

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

    public enum ZeroAxis
    {
        None = 0,

        X = 1, 
        
        Y = 2, 
        
        Z = 3, 
        
        All = 4
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
        /// G-code command in block requires an integer value.
        /// </summary>
        IntegerRequired = 23,

        /// <summary>
        /// More than one g-code command that requires axis words found in block.
        /// </summary>
        MoreThanOneCommandForAxis = 24,

        /// <summary>
        /// Repeated g-code word found in block.
        /// </summary>
        RepeatedWord = 25,

        /// <summary>
        /// No axis words found in block for g-code command or current modal state which requires them.
        /// </summary>
        NoAxisWords = 26,

        /// <summary>
        /// Line number value is invalid.
        /// </summary>
        LineNumber = 27,

        /// <summary>
        /// G-code command is missing a required value word.
        /// </summary>
        CommandMissingValue = 28,

        /// <summary>
        /// G59.x work coordinate systems are not supported.
        /// </summary>
        G59xNotSupported = 29,

        /// <summary>
        /// G53 only allowed with G0 and G1 motion modes.
        /// </summary>
        G53OnlyAllowedWithG0AndG1 = 30,

        /// <summary>
        /// Axis words found in block when no command or current modal state uses them.
        /// </summary>
        AxisWordFoundWithNoCommand = 31,

        /// <summary>
        /// G2 and G3 arcs require at least one in-plane axis word.
        /// </summary>
        ArcsRequireAtLeastOnAxis = 32,

        /// <summary>
        /// Motion command target is invalid.
        /// </summary>
        MotionCommandTargetInvalid = 33,

        /// <summary>
        /// Arc radius value is invalid.
        /// </summary>
        ArcRadiusValueIsInvalid = 34,

        /// <summary>
        /// G2 and G3 arcs require at least one in-plane offset word.
        /// </summary>
        ArcsRequireAtLeastOneInPlaneAxisWord = 35,

        /// <summary>
        /// Unused value words found in block.
        /// </summary>
        UnusedValueWordsInBlock = 36,

        /// <summary>
        /// G43.1 dynamic tool length offset is not assigned to configured tool length axis.
        /// </summary>
        DynamicToolOffsetNotAssigned = 37,

        /// <summary>
        /// Tool number greater than max supported value.
        /// </summary>
        ToolNumberGreaterThanMaxSupportedValue = 38,
    }

    public enum GrblAlarm
    {
        /// <summary>
        /// Undefined exception
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Hard limit triggered. Machine position is likely lost due to sudden and immediate halt. Re-homing is highly recommended.
        /// </summary>
        HardLimitTriggered = 1,

        /// <summary>
        /// G-code motion target exceeds machine travel. Machine position safely retained. Alarm may be unlocked.
        /// </summary>
        MotionExceedsMachineTravel = 2,

        /// <summary>
        /// Reset while in motion. Grbl cannot guarantee position. Lost steps are likely. Re-homing is highly recommended.
        /// </summary>
        ResetWhileInMotion = 3,

        /// <summary>
        /// Probe fail. The probe is not in the expected initial state before starting probe cycle, where G38.2 and G38.3 is not triggered and G38.4 and G38.5 is triggered.
        /// </summary>
        ProbeFail = 4,

        /// <summary>
        /// Probe fail. Probe did not contact the workpiece within the programmed travel for G38.2 and G38.4.
        /// </summary>
        ProbeFailWithinLimit = 5,

        /// <summary>
        /// Homing fail. Reset during active homing cycle.
        /// </summary>
        HomingFailResetDuringHoming = 6,

        /// <summary>
        /// Homing fail. Safety door was opened during active homing cycle.
        /// </summary>
        HomingFailSafetyDoorOpen = 7,

        /// <summary>
        /// Homing fail. Cycle failed to clear limit switch when pulling off. Try increasing pull-off setting or check wiring.
        /// </summary>
        HomingFailCycleFailed = 8,

        /// <summary>
        /// Homing fail. Could not find limit switch within search distance. Defined as 1.5 * max_travel on search and 5 * pulloff on locate phases.
        /// </summary>
        HomingFailLimitSwitches = 9,
    }

    public enum LineStatus
    {
        Undefined = 0,

        Sent = 1,

        Failed = 2,

        Processed = 3,
    }

    public enum MachineState
    {
        Undefined = 0,

        Idle = 1,

        Run = 2,

        Hold = 3,

        Jog = 4,

        Alarm = 5,

        Door = 6,

        Check = 7,

        Home = 8,

        Sleep = 9,
    }

    public enum JogDirection
    {
        XPlus = 1,

        XMinus = 2,

        YPlus = 3,

        YMinus = 4,

        ZPlus = 5,

        ZMinus = 6,

        XMinusYPlus = 7,

        XPlusYPlus = 8,

        XPlusYMinus = 9,

        XMinusYMinus = 10,
    }

    public enum FeedRateDisplayUnits
    {
        MmPerMinute,

        MmPerSecond,

        InchPerMinute,

        InchPerSecond,
    }

    public enum AxisConfiguration
    {
        None = 0,

        ReverseX = 1,

        ReverseY = 2,

        ReverseXandY = 3,

        ReverseZ = 4,

        ReverseXandZ = 5,

        ReverseYandZ = 6,

        ReversAll = 7
    }

    public enum Pin
    {
        High = 0, 
        
        Low = 1
    }

    [Flags]
    public enum ReportType
    {
        MachinePosition = 1,

        WorkPosition = 2,

        PlannerBuffer = 4,

        RXBuffer = 8,

        LimitPins = 16,
    }

    public enum FeedbackUnit
    {
        Inch = 1,

        Mm = 2,
    }
}
