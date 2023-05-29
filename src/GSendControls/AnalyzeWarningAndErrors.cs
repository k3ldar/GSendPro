using System;
using System.Linq;

using GSendShared;

namespace GSendControls
{
    public sealed class AnalyzeWarningAndErrors
    {
        public void ViewAndAnalyseWarningsAndErrors(WarningContainer warningsAndErrors, IGCodeAnalyses gCodeAnalyses)
        {
            warningsAndErrors.Clear(true);

            switch (gCodeAnalyses.UnitOfMeasurement)
            {
                case UnitOfMeasurement.None:
                case UnitOfMeasurement.Error:
                    AddMessage(warningsAndErrors, InformationType.Error, GSend.Language.Resources.GCodeUnitOfMeasureError);
                    break;
            }

            if ((gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant) || gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant)) &&
                !gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.TurnsOffCoolant))
            {
                AddMessage(warningsAndErrors, InformationType.Warning, GSend.Language.Resources.ErrorCoolantNotTurnedOff);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidGCode))
            {
                AddMessage(warningsAndErrors, InformationType.Error, GSend.Language.Resources.WarningContainsInvalidGCode);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.MultipleJobNames))
            {
                AddMessage(warningsAndErrors, InformationType.Error, GSend.Language.Resources.M650MultipleJobNamesSpecified);
            }

            if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.InvalidJobName))
            {
                AddMessage(warningsAndErrors, InformationType.Error, GSend.Language.Resources.M650InvalidJobNameSpecified);
            }

            if (gCodeAnalyses.SubProgramCount > 0)
            {
                foreach (IGCodeCommand item in gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')))
                {
                    string subProgram = $"{item}";

                    if (!String.IsNullOrEmpty(item.Comment))
                        subProgram += $" {item.Comment}";

                    AddMessage(warningsAndErrors, InformationType.Error, String.Format(GSend.Language.Resources.ErrorSubProgramMissing,
                        subProgram, item.LineNumber));
                }
            }

            foreach (string error in gCodeAnalyses.Errors)
            {
                AddMessage(warningsAndErrors, InformationType.Error, error);
            }
        }

        private void AddMessage(WarningContainer warningsAndErrors, InformationType informationType, string message)
        {
            if (!warningsAndErrors.Contains(informationType, message))
                warningsAndErrors.AddWarningPanel(informationType, message);
        }
    }
}
