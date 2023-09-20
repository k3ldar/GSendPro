using System;
using System.Collections;
using System.Windows.Forms;

using Shared;

namespace GSendControls
{
    /// <summary>
    /// Base settings control class
    /// </summary>
    public partial class BaseSettings : BaseControl
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="treeNode"></param>
        public BaseSettings(TreeNode treeNode)
        {
            TreeNode = treeNode;
        }

        #endregion Constructors

        #region Virtual Methods

        /// <summary>
        /// Controls can override and save data
        /// </summary>
        /// <returns></returns>
        public virtual bool SettingsSave()
        {
            return true;
        }

        /// <summary>
        /// Allows a control to confirm settings changed if necessary
        /// </summary>
        /// <returns></returns>
        public virtual bool SettingsConfirm()
        {
            return true;
        }

        /// <summary>
        /// Method called after control is initialised and loaded
        /// </summary>
        public virtual void SettingsLoaded()
        {

        }

        /// <summary>
        /// Method called after control is initialised and loaded
        /// </summary>
        public virtual void SettingsControlLoaded()
        {

        }

        /// <summary>
        /// Method called when settings form is shown
        /// </summary>
        public virtual void SettingShown()
        {

        }

        /// <summary>
        /// Method called when settings form hidden
        /// </summary>
        public virtual void SettingHidden()
        {

        }

        #endregion Virtual Methods

        #region Public Methods

        /// <summary>
        /// Updates all settings
        /// </summary>
        public void UpdateAll()
        {
            if (OnUpdate != null)
                OnUpdate(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the OnRefreshLists event
        /// </summary>
        public static void RaiseUpdateLists()
        {
            if (OnRefreshLists != null)
                OnRefreshLists(null, EventArgs.Empty);
        }

        #endregion Public Methods

        #region Properties

        /// <summary>
        /// Help ID for Page
        /// </summary>
        public int HelpID { get; set; }

        /// <summary>
        /// Indicates wether settings have been changed or not
        /// </summary>
        public bool SettingsChanged { get; set; }

        /// <summary>
        /// Node associated with the setting
        /// </summary>
        internal TreeNode TreeNode { get; set; }

        #endregion Properties

        #region Events

        /// <summary>
        /// OnUpdate event
        /// </summary>
        internal event EventHandler OnUpdate;

        /// <summary>
        /// Event for when lists have been refreshed
        /// </summary>
        public static event EventHandler OnRefreshLists;

        #endregion Events
    }


    /// <summary>
    /// public class used to hold settings data
    /// </summary>
    public sealed class Setting
    {
        #region Constructors

        internal Setting(string name, string description, BaseSettings settingsPanel)
        {
            Name = name;
            Description = description;
            SettingsPanel = settingsPanel;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Name of the setting
        /// </summary>
        internal string Name { get; private set; }

        /// <summary>
        /// Description for the setting
        /// 
        /// To be displayed at the top of the form
        /// </summary>
        internal string Description { get; private set; }

        /// <summary>
        /// Settings control to be displayed
        /// </summary>
        internal BaseSettings SettingsPanel { get; private set; }

        #endregion Properties
    }
}
