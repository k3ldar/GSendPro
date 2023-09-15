// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Array, so not available", Scope = "member", Target = "~M:GSendService.Api.MachineApi.ValidateMachineModel(GSendShared.IMachine,System.Boolean,System.String@)~System.Boolean")]
[assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "Debug Only", Scope = "member", Target = "~F:GSendService.Internal.CheckForUpdatesThread.UpdateBaseUri")]
[assembly: SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.GetActiveLicense~GSendShared.Abstractions.ILicense")]
[assembly: SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.SetActiveLicense(GSendShared.Abstractions.ILicense@)")]
