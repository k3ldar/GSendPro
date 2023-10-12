// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S2925:\"Thread.Sleep\" should not be used in tests", Justification = "Tests are only used manually with arduino device", Scope = "member", Target = "~M:GSendTests.GCService.LiveTestsWithArduino.ConnectToGrblSendTwoCommands")]
[assembly: SuppressMessage("Major Code Smell", "S2925:\"Thread.Sleep\" should not be used in tests", Justification = "Tests are only used manually with arduino device", Scope = "member", Target = "~M:GSendTests.GCService.LiveTestsWithArduino.HomeAndPause")]
[assembly: SuppressMessage("Major Code Smell", "S2925:\"Thread.Sleep\" should not be used in tests", Justification = "Tests are only used manually with arduino deviceTests are only used manually with arduino device>", Scope = "member", Target = "~M:GSendTests.GCService.GCodeProcessorTests.Unlock_MachineLocked_ReturnsTrueAndSendsCommandToCom")]
[assembly: SuppressMessage("Minor Code Smell", "S3878:Arrays should not be created for params parameters", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.MCodeOverrideTests.M620Tests.Process_M620UnableToOpenComPort_Throws_FileNotFoundException")]
[assembly: SuppressMessage("Minor Code Smell", "S3878:Arrays should not be created for params parameters", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.MCodeOverrideTests.M620Tests.Process_M620OpenComPort_PreventsSendingOfCommand_ReturnsTrue")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.GCService.ProcessorMediatorTests.ExecuteAsync_StartsAllMachines_Success")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.Mocks.MockComPortFactory.GetComPort(System.String)~GSendShared.IComPort")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.Mocks.MockComPortFactory.CreateComPort(GSendShared.Abstractions.IComPortModel)~GSendShared.IComPort")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendTests.Mocks.MockComPortFactory.DeleteComPort(GSendShared.IComPort)")]
