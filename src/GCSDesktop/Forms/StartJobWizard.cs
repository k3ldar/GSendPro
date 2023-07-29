using System;
using System.Drawing;
using System.Windows.Forms;

using GSendApi;

using GSendControls;

using GSendShared;
using GSendShared.Models;

namespace GSendDesktop.Forms
{
    public partial class StartJobWizard : BaseForm
    {
        private readonly MachineStateModel _machineStatusModel;
        private readonly IGCodeAnalyses _gCodeAnalyses;
        private int _errorCount = 0;

        public StartJobWizard()
        {
            InitializeComponent();
        }

        protected override string SectionName => nameof(StartJobWizard);

        public StartJobWizard(MachineStateModel machineStatusModel, IGCodeAnalyses gCodeAnalyses,
            IGSendApiWrapper machineApiWrapper)
            : this()
        {
            _machineStatusModel = machineStatusModel ?? throw new ArgumentNullException(nameof(machineStatusModel));
            _gCodeAnalyses = gCodeAnalyses ?? throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (machineApiWrapper == null)
                throw new ArgumentNullException(nameof(machineApiWrapper));

            string jobName = DesktopSettings.ReadValue<string>(nameof(StartJobWizard), Constants.StartWizardSelectedJob, String.Empty);

            if (!String.IsNullOrEmpty(gCodeAnalyses.JobName))
                jobName = gCodeAnalyses.JobName;

            foreach (IJobProfile jobProfile in machineApiWrapper.JobProfilesGet())
            {
                int index = cmbJobProfiles.Items.Add(jobProfile);

                if (jobProfile.Name.Equals(jobName))
                    cmbJobProfiles.SelectedIndex = index;
            }

            if (cmbJobProfiles.SelectedIndex == -1)
                cmbJobProfiles.SelectedIndex = 0;

            int selectedIndex = 0;

            foreach (IToolProfile toolProfile in machineApiWrapper.ToolProfilesGet())
            {
                int idx = cmbTool.Items.Add(toolProfile);

                if (gCodeAnalyses.Tools.Contains(toolProfile.Id.ToString()))
                    selectedIndex = idx;
            }

            cmbTool.SelectedIndex = selectedIndex;

            ValidateCoordinateSystem();


            lblErrors.Text = String.Format(GSend.Language.Resources.StartJobErrorCount, _errorCount);
        }

        public IToolProfile ToolProfile => GetToolProfile();

        public IJobProfile JobProfile => GetJobProfile();

        public bool IsSimulation => cbSimulate.Checked;

        private IToolProfile GetToolProfile()
        {
            return cmbTool.SelectedItem as IToolProfile;
        }

        private IJobProfile GetJobProfile()
        {
            return cmbJobProfiles.SelectedItem as IJobProfile;
        }

        private void ValidateCoordinateSystem()
        {
            string[] coords = _gCodeAnalyses.CoordinateSystems.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (coords.Length == 0)
                coords = new string[] { _machineStatusModel.CoordinateSystem.ToString() };

            lstCoordinates.Items.Clear();

            foreach (string coords2 in coords)
            {
                foreach (char axis in new char[] { 'X', 'Y', 'Z' })
                {
                    string name = $"{coords2}Zero{axis}Set";

                    if (Enum.TryParse(name, true, out MachineStateOptions option))
                    {
                        lstCoordinates.Items.Add(option);

                        if (!_machineStatusModel.MachineStateOptions.HasFlag(option))
                            _errorCount++;
                    }
                }
            }
        }

        protected override void LoadResources()
        {
            Text = GSend.Language.Resources.StartJobWizard;
            btnCancel.Text = GSend.Language.Resources.Cancel;
            btnStart.Text = GSend.Language.Resources.Start;
            cbSimulate.Text = GSend.Language.Resources.SimulateRun;
            lblJobProfile.Text = GSend.Language.Resources.JobProfile;
            lblTool.Text = GSend.Language.Resources.ToolProfile;
            lblWarnings.Text = GSend.Language.Resources.CheckList;
        }

        private void lstCoordinates_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0)
            {
                ListBox box = (ListBox)sender;
                Color fore = box.ForeColor;

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    fore = SystemColors.HighlightText;

                if (Enum.TryParse(box.Items[e.Index].ToString(), true, out MachineStateOptions option))
                {
                    string text = $"{option.ToString()[..3]} {option.ToString().Substring(7, 1)}";
                    TextRenderer.DrawText(e.Graphics, text, box.Font, new Point(1, e.Bounds.Top + 2), fore);

                    if (_machineStatusModel.MachineStateOptions.HasFlag(option))
                        e.Graphics.DrawImage(imageList1.Images[1], new Point(40, e.Bounds.Top + 1));
                    else
                        e.Graphics.DrawImage(imageList1.Images[0], new Point(40, e.Bounds.Top + 1));
                }
            }

            e.DrawFocusRectangle();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IJobProfile jobProfile = cmbJobProfiles.Items[cmbJobProfiles.SelectedIndex] as IJobProfile;
            DesktopSettings.WriteValue(nameof(StartJobWizard), Constants.StartWizardSelectedJob, jobProfile.Name);
            DialogResult = DialogResult.OK;
        }

        private void cmbTool_DrawItem(object sender, DrawItemEventArgs e)
        {
            IToolProfile toolProfile = cmbTool.Items[e.Index] as IToolProfile;

            e.DrawBackground();


            using SolidBrush brush = new(e.ForeColor);

            e.Graphics.DrawString(toolProfile.Name, e.Font, brush, e.Bounds.Left + 1, e.Bounds.Top + 1);

            if (!e.State.HasFlag(DrawItemState.NoFocusRect))
                e.DrawFocusRectangle();
        }

        private void cmbJobProfiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            IJobProfile jobProfile = cmbJobProfiles.Items[e.Index] as IJobProfile;

            e.DrawBackground();


            using SolidBrush brush = new(e.ForeColor);

            e.Graphics.DrawString(jobProfile.Name, e.Font, brush, e.Bounds.Left + 1, e.Bounds.Top + 1);

            if (!e.State.HasFlag(DrawItemState.NoFocusRect))
                e.DrawFocusRectangle();
        }
    }
}
