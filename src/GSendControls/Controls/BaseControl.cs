/*
 *  The contents of this file are subject to MIT Licence.  Please
 *  view License.txt for further details. 
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2012 Simon Carter
 *
 *  Purpose:  Base Control Class
 *
 */
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GSendControls
{
    /// <summary>
    /// Base Control Class
    /// </summary>
    public partial class BaseControl : UserControl
    {
        #region Constructors

        /// <summary>
        /// Constructor - Initialises a new instance
        /// </summary>
        public BaseControl()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// If set this control will show hint information for the active control
        /// </summary>
        public Label HintControl { get; set; }

        #endregion Properties

        #region Virtual Methods

        /// <summary>
        /// Method called when language changes
        /// </summary>
        /// <param name="culture">New UI culture being used</param>
        public virtual void LanguageChanged(CultureInfo culture)
        {

        }

        /// <summary>
        /// Virtual method to obtain Hint Text
        /// </summary>
        /// <param name="controlName">Name of control</param>
        /// <param name="subControlName">Sub control name</param>
        /// <returns>Hint text for control</returns>
        protected virtual string GetHintText(string controlName, string subControlName)
        {
            return String.Empty;
        }

        #endregion Virtual Methods

        #region Protected Methods

        /// <summary>
        /// Overridden OnCreateControl event
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        /// <summary>
        /// Overridden OnLoad method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                LanguageChanged(Thread.CurrentThread.CurrentUICulture);

            PrepareAutoHint(this);
        }

        #endregion Protected Methods

        #region Public Methods

        /// <summary>
        /// Shows a dialog with stop icon
        /// </summary>
        /// <param name="Title">Title of dialog box</param>
        /// <param name="Message">Message to be displayed</param>
        /// <returns>true if user clicks yes, otherwise false</returns>
        protected static bool ShowHardConfirm(string Title, string Message)
        {
            return MessageBox.Show(Message, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes;
        }

        /// <summary>
        /// Shows a dialog with question icon
        /// </summary>
        /// <param name="Title">Title of dialog box</param>
        /// <param name="Message">Message to be displayed</param>
        /// <returns>true if user clicks yes, otherwise false</returns>
        protected static bool ShowQuestion(string Title, string Message)
        {
            return MessageBox.Show(Message, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Show's a dialog with information icon
        /// </summary>
        /// <param name="Title">Title of dialog box</param>
        /// <param name="Message">Message to be displayed</param>
        protected static void ShowInformation(string Title, string Message)
        {
            MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows a dialog with error icon
        /// </summary>
        /// <param name="Title">Title of dialog box</param>
        /// <param name="Message">Message to be displayed</param>
        protected static void ShowError(string Title, string Message)
        {
            MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Public Methods

        #region Private Methods

        private void PrepareAutoHint(Control control)
        {
            foreach (Control ctl in control.Controls)
            {
                ctl.Enter += ctl_Enter;
                PrepareAutoHint(ctl);
            }
        }

        private void ctl_Enter(object sender, EventArgs e)
        {
            if (HintControl == null)
                return;

            if (sender is Control ctl)
            {
                HintControl.Text = GetHintText(this.Name, ctl.Name);
            }
        }

        /// <summary>
        /// Internal method used for designer puroposes only
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private static string GetLocalizedControls(ControlCollection parent, string results)
        {
            if (parent == null)
                return results;

            StringBuilder Result = new(results);

            foreach (Control child in parent)
            {
                if (!String.IsNullOrEmpty(child.Text))
                {
                    Result.Append(String.Format("{0}.Text = LanguageStrings.\"{1}\"\r\n", child.Name, child.Text));
                }

                Result.Append(GetLocalizedControls(child.Controls, String.Empty));
            }

            return Result.ToString();
        }

        #endregion Private Methods
    }
}
