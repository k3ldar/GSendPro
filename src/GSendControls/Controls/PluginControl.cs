using System.Windows.Forms;

namespace GSendControls.Controls
{
    public class PluginControl : BaseControl
    {
        new public Control Parent
        {
            get
            {
                return null;
            }

            set
            {
                base.Parent = value;
            }
        }

        new public AnchorStyles Anchor
        {
            get => base.Anchor;

            set
            {
                base.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            }
        }

        public void UpdatePosition(Control parentControl)
        {
            Left = 2;
            Top = 2;
            Height = parentControl.Height - 4;
            Width = parentControl.Width - 4;
            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }
    }
}
