using System;
using System.Collections.Generic;
using System.Linq;

using GSendShared;

namespace GSendControls
{
    public sealed class AnalyzeWarningAndErrors
    {
        private readonly ISubprograms _subprograms;

        public AnalyzeWarningAndErrors(ISubprograms subprograms)
        {
            _subprograms = subprograms ?? throw new ArgumentNullException(nameof(subprograms));
        }

        public void ViewAndAnalyseWarningsAndErrors(WarningContainer warningsAndErrors, List<WarningErrorList> issues, IGCodeAnalyses gCodeAnalyses)
        {
            warningsAndErrors?.Clear(true);

            switch (gCodeAnalyses.UnitOfMeasurement)
            {
                case UnitOfMeasurement.None:
                case UnitOfMeasurement.Error:
                    AddMessage(warningsAndErrors, issues, InformationType.Error, GSend.Language.Resources.GCodeUnitOfMeasureError);
                    break;
            }

            if ((gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant) || gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant)) &&
                !gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.TurnsOffCoolant))
            {
                AddMessage(warningsAndErrors, issues, InformationType.Warning, GSend.Language.Resources.ErrorCoolantNotTurnedOff);
            }

            if (!gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.HasEndProgram))
            {
                AddMessage(warningsAndErrors, issues, InformationType.Error, GSend.Language.Resources.AnalysesError24);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidGCode))
            {
                AddMessage(warningsAndErrors, issues, InformationType.Error, GSend.Language.Resources.AnalysesWarningContainsInvalidGCode);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.MultipleJobNames))
            {
                AddMessage(warningsAndErrors, issues, InformationType.Error, GSend.Language.Resources.M602MultipleJobNamesSpecified);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName))
            {
                AddMessage(warningsAndErrors, issues, InformationType.Error, GSend.Language.Resources.M602InvalidJobNameSpecified);
            }

            if (gCodeAnalyses.SubProgramCount > 0)
            {
                foreach (IGCodeCommand item in gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')))
                {
                    string subProgram = $"{item}";

                    if (!String.IsNullOrEmpty(item.Comment))
                        subProgram += $" {item.Comment}";

                    if (!_subprograms.Exists($"{item.Command}{item.CommandValue}"))
                    {
                        AddMessage(warningsAndErrors, issues, InformationType.Error, String.Format(GSend.Language.Resources.ErrorSubProgramMissing,
                            subProgram, item.LineNumber));
                    }
                }
            }

            foreach (string error in gCodeAnalyses.Errors)
            {
                AddMessage(warningsAndErrors, issues, InformationType.Error, error);
            }

            foreach (string warning in gCodeAnalyses.Warnings)
            {
                AddMessage(warningsAndErrors, issues, InformationType.Warning, warning);
            }
        }

        private static void AddMessage(WarningContainer warningsAndErrors, List<WarningErrorList> issues, InformationType informationType, string message)
        {
            if (warningsAndErrors != null)
            {
                if (!warningsAndErrors.Contains(informationType, message))
                    warningsAndErrors.AddWarningPanel(informationType, message);
            }
            else if (issues != null)
            {
                List<WarningErrorList> existingItems = issues.Where(i => i.Message.Equals(message) && i.InfoType.Equals(informationType)).ToList();

                if (existingItems.Count > 0)
                {
                    existingItems.ForEach(i => i.MarkedForRemoval = false);
                }
                else
                {
                    issues.Add(new WarningErrorList()
                    {
                        Message = message,
                        InfoType = informationType,
                        IsNew = true,
                        MarkedForRemoval = false
                    });
                }
            }
        }
    }
}
