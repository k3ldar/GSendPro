// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>", Scope = "member", Target = "~M:GSendShared.CommandLineArgs.ConvertArgsToDictionary(System.String[])~System.Collections.Generic.Dictionary{System.String,System.String}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "Null expected", Scope = "member", Target = "~P:GSendShared.Plugins.InternalPlugins.HelpMenu.HelpMenuPlugin.ToolbarItems")]
[assembly: SuppressMessage("Minor Code Smell", "S1643:Strings should not be concatenated using '+' in a loop", Justification = "OK for this scenario", Scope = "member", Target = "~M:GSendShared.Helpers.ValidateParameters.ExtractM623Properties(GSendShared.IGCodeCommand,System.Int32)~GSendShared.Models.M623Model")]
[assembly: SuppressMessage("Minor Code Smell", "S1643:Strings should not be concatenated using '+' in a loop", Justification = "OK for this scenario", Scope = "member", Target = "~M:GSendShared.Models.GCodeVariableBlockModel.ParseVariables")]
[assembly: SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification = "<Pending>", Scope = "member", Target = "~M:GSendShared.CommandLineArgs.ConvertArgsToDictionary(System.String[])~System.Collections.Generic.Dictionary{System.String,System.String}")]
[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "<Pending>", Scope = "member", Target = "~F:GSendShared.Models.OverrideModel._constructing")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "Not necessary", Scope = "member", Target = "~M:GSendShared.Providers.Internal.Enc.AesImpl.Decrypt(System.String,System.Byte[])~System.String")]
