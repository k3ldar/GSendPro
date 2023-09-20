﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "Fine as is", Scope = "member", Target = "~M:GSendControls.HeartbeatPanel.DrawHeartBeat(System.Drawing.Graphics@,System.Drawing.Rectangle@)")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:GSendControls.ShortcutHandler.FindMatchingCombination(System.Collections.Generic.List{System.Int32})~System.Collections.Generic.List{System.Int32}")]
[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "Fine as is", Scope = "member", Target = "~M:GSendControls.GCodeAnalysesDetails.LoadAnalyser(GSendShared.IGCodeAnalyses)")]
[assembly: SuppressMessage("Major Bug", "S2259:Null pointers should not be dereferenced", Justification = "<Pending>", Scope = "member", Target = "~M:GSendControls.Plugins.PluginHelper.InitializeAllPlugins(GSendShared.Plugins.IPluginHost)")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
[assembly: SuppressMessage("Blocker Code Smell", "S3237:\"value\" contextual keyword should be used", Justification = "Forcing a size", Scope = "member", Target = "~P:GSendControls.BaseWizardPage.MinimumSize")]
[assembly: SuppressMessage("Blocker Code Smell", "S3237:\"value\" contextual keyword should be used", Justification = "Forcing a size", Scope = "member", Target = "~P:GSendControls.BaseWizardPage.MaximumSize")]
[assembly: SuppressMessage("Minor Code Smell", "S6602:\"Find\" method should be used instead of the \"FirstOrDefault\" extension", Justification = "Doesn't play nicely with arrays", Scope = "member", Target = "~M:GSendControls.BaseForm.LoadSettings")]
[assembly: SuppressMessage("Blocker Code Smell", "S3237:\"value\" contextual keyword should be used", Justification = "can not resize", Scope = "member", Target = "~P:GSendControls.BaseWizardPage.AutoSize")]
