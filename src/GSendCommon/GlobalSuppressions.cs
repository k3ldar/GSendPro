// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.GCodeProcessor.ValidateGrblSettings")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:GSendCommon.Settings.HTTP")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:GSendCommon.Settings.HTTPS")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~P:GSendCommon.GCodeProcessor.RapidsSpeed")]
[assembly: SuppressMessage("Major Code Smell", "S3010:Static fields should not be updated in constructors", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.ProcessorMediator.#ctor(System.IServiceProvider,PluginManager.Abstractions.ILogger,GSendShared.IGSendDataProvider,GSendShared.IComPortFactory,PluginManager.Abstractions.INotificationService,PluginManager.Abstractions.ISettingsProvider,GSendShared.Abstractions.IGCodeParserFactory)")]
[assembly: SuppressMessage("Style", "IDE0057:Use range operator", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.MCodeOverrides.M622Override.Process(GSendShared.Abstractions.IGCodeOverrideContext,System.Threading.CancellationToken)~System.Boolean")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>", Scope = "member", Target = "~P:GSendCommon.GCodeProcessor.RapidsSpeed")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.ComPortFactory.DeleteComPort(GSendShared.IComPort)")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.ComPortFactory.GetComPort(System.String)~GSendShared.IComPort")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.GCodeProcessor.InternalGetBufferSize~System.Int32")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.ComPortFactory.CreateComPort(GSendShared.Abstractions.IComPortModel)~GSendShared.IComPort")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.GCodeOverrideContext.ProcessMCodeOverrides(GSendShared.IGCodeLine)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.GCodeProcessor.SendCommandWaitForOKCommand(System.String)~System.String")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendCommon.ProcessorMediator.OpenProcessors")]
