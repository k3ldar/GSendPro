/*
 *  The contents of this file are subject to MIT Licence.  Please
 *  view License.txt for further details. 
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2014 Simon Carter
 *
 *  Purpose:  Base wizard page, for displaying wizard items in WizardForm
 *
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSendControls
{
    /// <summary>
    /// Base wizard page
    /// </summary>
    [DefaultValue(typeof(Size), "566, 356")]
    public class BaseWizardPage : BaseControl
    {
        public BaseWizardPage()
        {

        }

        #region Properties

        /// <summary>
        /// Get's an instance of the wizard that is currently being used
        /// </summary>
        public WizardForm MainWizardForm { get; internal set; }

        #endregion Properties

        #region Virtual Methods

        /// <summary>
        /// Method used to Load contents of a wizard page, from a file
        /// </summary>
        /// <param name="fileName"></param>
        public virtual void LoadFromFile(string fileName)
        {

        }

        /// <summary>
        /// Method used to save contents of the wizard page to a file
        /// </summary>
        /// <param name="fileName"></param>
        public virtual void SaveToFile(string fileName)
        {

        }


        /// <summary>
        /// Method called when a page is shown in the wizard
        /// </summary>
        public virtual void PageShown()
        {

        }

        /// <summary>
        /// Method to determine wether the page should be skipped or not
        /// </summary>
        /// <returns>bool, if result is false, page will not be skipped, if true, page will be skipped</returns>
        public virtual bool SkipPage()
        {
            return false;
        }

        /// <summary>
        /// Virtual method to be overridden in each wizard page for when previous button is clicked
        /// </summary>
        /// <returns>bool, true if can return to previous step, otherwise false</returns>
        public virtual bool PreviousClicked()
        {
            return true;
        }

        /// <summary>
        /// Virtual method to be overridden in each wizard page for when Next button is clicked
        /// </summary>
        /// <returns>bool, true if can continue to the next step, otherwise false</returns>
        public virtual bool NextClicked()
        {
            return true;
        }

        /// <summary>
        /// Virtual method to be overridden in final page prior to Finish being clicked
        /// </summary>
        /// <returns>bool, true if can proceed otherwise false</returns>
        public virtual bool BeforeFinish()
        {
            return true;
        }

        /// <summary>
        /// Indicates that cancel has been clicked
        /// </summary>
        public virtual bool CancelClicked()
        {
            return true;
        }

        /// <summary>
        /// Determines wether a page can be cancelled or not
        /// </summary>
        /// <returns></returns>
        public virtual bool CanCancel()
        {
            return true;
        }

        /// <summary>
        /// Overridden by descendant page, determines wether the Previous button is enabled or not
        /// </summary>
        /// <returns></returns>
        public virtual bool CanGoPrevious()
        {
            return true;
        }

        /// <summary>
        /// Overridden by descendant page, determines wether the Next button is enabled or not
        /// </summary>
        /// <returns></returns>
        public virtual bool CanGoNext()
        {
            return true;
        }

        /// <summary>
        /// Overridden by descendant page, determines wether the Finish button is enabled or not
        /// </summary>
        /// <returns></returns>
        public virtual bool CanGoFinish()
        {
            return true;
        }

        public virtual void LoadResources()
        {

        }

        #endregion Virtual Methods

        protected override Size DefaultSize => new Size(566, 356);

        protected override void OnSizeChanged(EventArgs e)
        {
            Width = 566;
            Height = 356;
        }

        /// <summary>
        /// Maximum dimensions of the base wizard page
        /// </summary>
        public override Size MaximumSize
        {
            get
            {
                return new Size(566, 356);
            }
            set
            {
                base.MaximumSize = new Size(566, 356);
            }
        }

        /// <summary>
        /// Minimum dimensions of the base wizard page
        /// </summary>
        public override Size MinimumSize
        {
            get
            {
                return new Size(566, 356);
            }
            set
            {
                base.MinimumSize = new Size(566, 356);
            }
        }

        public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = false; }

        protected static string FormatPercentDiff(decimal val1, decimal val2)
        {
            if (val1 == 0 || val2 == 0)
                return "0%";

            decimal diff = (val1 - val2) / val1 * 100;
            return $"{diff.ToString("n")}%";
        }

        protected static string FormatAccelerationValue(decimal speed)
        {
            return $"{speed.ToString("n")} mm/sec2";
        }

        protected static string FormatSpeedValue(decimal speed)
        {
            return $"{speed.ToString("n")} mm/min";
        }
    }
}
