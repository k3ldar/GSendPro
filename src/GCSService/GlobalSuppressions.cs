// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Array, so not available", Scope = "member", Target = "~M:GSendService.Api.MachineApi.ValidateMachineModel(GSendShared.IMachine,System.Boolean,System.String@)~System.Boolean")]
[assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "Debug Only", Scope = "member", Target = "~F:GSendService.Internal.CheckForUpdatesThread.UpdateBaseUri")]
[assembly: SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.GetActiveLicense~GSendShared.Abstractions.ILicense")]
[assembly: SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.SetActiveLicense(GSendShared.Abstractions.ILicense@)")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobProfileApi.JobProfileUpdate(GSendShared.IJobProfile)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobProfileApi.JobProfileDelete(System.Int64)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobExecutionApi.JobExecutionTooltime(System.Int64)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.ComPortsApi.GetAllPorts~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobExecutionApi.CreateJobExecution(System.Int64,System.Int64,System.Int64)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobProfileApi.JobProfilesGet~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Api.JobProfileApi.JobProfileAdd(GSendShared.IJobProfile)~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.GetActiveLicense~GSendShared.Abstractions.ILicense")]
[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "<Pending>", Scope = "member", Target = "~M:GSendService.Internal.LicenseFactory.SetActiveLicense(GSendShared.Abstractions.ILicense@)")]
