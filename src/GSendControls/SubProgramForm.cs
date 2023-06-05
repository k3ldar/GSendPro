using System;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public partial class SubProgramForm : BaseForm
    {
        private readonly ISubPrograms _subPrograms;

        public SubProgramForm(ISubPrograms subPrograms)
        {
            _subPrograms = subPrograms ?? throw new ArgumentNullException(nameof(subPrograms));
            InitializeComponent();
        }

        protected override string SectionName => nameof(SubProgramForm);

        public string SubprogramName { get; private set; }

        public string Description { get; private set; }

        protected override void LoadResources()
        {
            Text = GSend.Language.Resources.SaveSubProgram;
            lblDescription.Text = GSend.Language.Resources.Description;
            lblSubProgramId.Text = GSend.Language.Resources.SubProgramId;
            btnCancel.Text = GSend.Language.Resources.Cancel;
            btnOK.Text = GSend.Language.Resources.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string name = $"O{numericSubProgramId.Value}";

            if (_subPrograms.Exists(name))
            {
                DialogResult overWrite = MessageBox.Show(this,
                    String.Format(GSend.Language.Resources.SubprogramExistsOverwrite, name),
                    GSend.Language.Resources.Subprogram, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (overWrite == DialogResult.No)
                    return;
            }

            if (txtDescription.Text.Length < 10)
            {
                MessageBox.Show(this,
                    GSend.Language.Resources.SubprogramDescription,
                    GSend.Language.Resources.Subprogram,
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            SubprogramName = name;
            Description = txtDescription.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
