/*
 *  The contents of this file are subject to MIT Licence.  Please
 *  view License.txt for further details. 
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2014 Simon Carter
 *
 *  Purpose:  Extended Listview with column sorting, tooltip and state saving
 *
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Shared;

namespace GSendControls
{
    /// <summary>
    /// List view extender
    /// </summary>
    public class ListViewEx : System.Windows.Forms.ListView
    {
        #region Private Members

        #region Timer / Tooltip variables

        private Point _oldPoint;
        private bool _moveStart;
        private readonly Timer _timerTooltip;
        private readonly ToolTip _tooltip;

        #endregion Timer / Tooltip variables

        #region Sort Members

        private readonly Font _sortFont;
        private bool _listSorted;

        /// <summary>
        /// Column Sorter
        /// </summary>
        private readonly ListViewColumnSorter _lvColumnSorter;

        #endregion Sort Members

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewEx()
            : base()
        {
            this.DoubleBuffered = true;
            _listSorted = false;
            _lvColumnSorter = new ListViewColumnSorter();
            this.ListViewItemSorter = _lvColumnSorter;
            _lvColumnSorter.SortColumn = 0;
            _lvColumnSorter.SortOrder = SortOrder.Ascending;
            AllowColumnReorder = true;
            OwnerDraw = true;
            _sortFont = new Font("Helvetica", this.Font.Size, FontStyle.Bold);

            // tooltip
            _tooltip = new ToolTip();

            //timer for tooltip
            _timerTooltip = new Timer
            {
                Interval = 1000,
                Enabled = false
            };
            _timerTooltip.Tick += new EventHandler(timerTooltip_Tick);

            SaveName = this.Name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Indicates wether tool tips are shown or not
        /// </summary>
        public bool ShowToolTip { get; set; }

        /// <summary>
        /// Name used when saving/loading state
        /// </summary>
        public string SaveName { get; set; }

        #endregion Properties

        #region Overridden Methods

        /// <summary>
        /// overridden ondrawitem
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// Overridden ondrawsubitem
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        /// <summary>
        /// Overridden Draw Column Header
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            string sortText = _lvColumnSorter.SortOrder == SortOrder.Ascending ? "↓" : "↑";

            using (StringFormat sf = new())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;

                // Draw the standard header background.
                e.DrawBackground();

                e.Graphics.DrawString(e.Header.Text, this.Font,
                    Brushes.Black, e.Bounds, sf);

                if (e.ColumnIndex == _lvColumnSorter.SortColumn)
                {
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Near;

                    Rectangle newBounds = e.Bounds;
                    newBounds.Width = newBounds.Width - 2;
                    e.Graphics.DrawString(sortText, _sortFont,
                        Brushes.Black, newBounds, sf);
                }
            }
        }

        /// <summary>
        /// Overridden Column Click Column
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnClick(System.Windows.Forms.ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvColumnSorter.SortOrder == SortOrder.Ascending)
                {
                    _lvColumnSorter.SortOrder = SortOrder.Descending;
                }
                else
                {
                    _lvColumnSorter.SortOrder = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvColumnSorter.SortColumn = e.Column;
                _lvColumnSorter.SortOrder = SortOrder.Ascending;
            }

            this.Sort();
        }

        /// <summary>
        /// Overridden Dispose Method
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
#if DEBUG
            System.GC.SuppressFinalize(this);
#endif
            _sortFont.Dispose();
            SaveLayout();
            _timerTooltip.Enabled = false;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Overridden on mouse move method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //tooltip code
            if (Utilities.Between(e.X, _oldPoint.X - 10, _oldPoint.X + 10) &&
                Utilities.Between(e.Y, _oldPoint.Y - 10, _oldPoint.Y + 10))
                return;

            if (_moveStart)
            {
                _tooltip.Hide(this);
                _oldPoint = new Point(e.X, e.Y);

                _timerTooltip.Enabled = e.Button == System.Windows.Forms.MouseButtons.None;
            }
            else
            {
                _moveStart = true;
            }

        }

        /// <summary>
        /// Overridden OnMouseDown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _timerTooltip.Enabled = false;
            _tooltip.Hide(this);
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Overridden mouse wheel event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            _tooltip.Hide(this);
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Overridden OnLeave Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            _timerTooltip.Enabled = false;
            base.OnLeave(e);
        }

        /// <summary>
        /// Overridden OnItemMouseHover
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemMouseHover(ListViewItemMouseHoverEventArgs e)
        {
            base.OnItemMouseHover(e);
        }

        #endregion Overridden Methods

        #region Public Methods

        /// <summary>
        /// Sorts the list view
        /// </summary>
        public new void Sort()
        {
            base.Sort();
            _listSorted = true;
            Invalidate(true);
        }

        /// <summary>
        /// Saves the layout for the listview
        /// </summary>
        public void SaveLayout()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || String.IsNullOrEmpty(this.Name))
                return;

            string file = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            file = Utilities.AddTrailingBackSlash(Utilities.AddTrailingBackSlash(file) + Application.ProductName);
            System.IO.Directory.CreateDirectory(file);

            file += this.SaveName + this.Name + ".dat";

            XML.SetXMLValue("Settings", "SortOrder", _lvColumnSorter.SortOrder.ToString(), file);
            XML.SetXMLValue("Settings", "SortColumn", _lvColumnSorter.SortColumn.ToString(), file);
            XML.SetXMLValue("Settings", "Sorted", _listSorted.ToString(), file);

            foreach (ColumnHeader header in this.Columns)
            {
                string name = String.IsNullOrEmpty(header.Name) ? header.Text : header.Name;
                try
                {
                    name = Validation.Validate(name, ValidationTypes.AtoZ);

                    XML.SetXMLValue("Settings", String.Format("{0}DisplayIndex", name), header.DisplayIndex.ToString(), file);
                    XML.SetXMLValue("Settings", String.Format("{0}Width", name), header.Width.ToString(), file);
                }
                catch (Exception err)
                {
                    if (err.Message.Contains("is not of type AtoZ"))
                        break;
                    else
                        throw;
                }
            }
        }

        /// <summary>
        /// Loads the layout for the listview
        /// </summary>
        public void LoadLayout()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || String.IsNullOrEmpty(this.Name))
                return;

            string file = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            file = Utilities.AddTrailingBackSlash(Utilities.AddTrailingBackSlash(file) + Application.ProductName) +
                this.SaveName + this.Name + ".dat";

            if (System.IO.File.Exists(file))
            {
                try
                {
                    string sortOrder = XML.GetXMLValue("Settings", "SortOrder", "Ascending", file);
                    _lvColumnSorter.SortOrder = (SortOrder)Enum.Parse(typeof(SortOrder), sortOrder);

                    _lvColumnSorter.SortColumn = XML.GetXMLValue("Settings", "SortColumn", 0, file);
                    _listSorted = XML.GetXMLValue("Settings", "Sorted", false, file);

                    foreach (ColumnHeader header in this.Columns)
                    {
                        string name = String.IsNullOrEmpty(header.Name) ? header.Text : header.Name;
                        name = Validation.Validate(name, ValidationTypes.AtoZ);

                        try
                        {
                            header.DisplayIndex = Utilities.CheckMinMax(XML.GetXMLValue("Settings",
                                String.Format("{0}DisplayIndex", name), header.DisplayIndex, file), 0, this.Columns.Count - 1);
                            header.Width = XML.GetXMLValue("Settings", String.Format("{0}Width", name), header.Width, file);
                        }
                        catch
                        {
                            // ignore errors here
                        }
                    }

                    if (_listSorted)
                        Sort();
                }
                catch (Exception err)
                {
                    // any error here, delete the config file
                    EventLog.Add(err);

                    if (System.IO.File.Exists(file))
                        System.IO.File.Delete(file);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Raise tool tip event
        /// </summary>
        /// <param name="e"></param>
        private void RaiseTooltip(ToolTipEventArgs e)
        {
            ToolTipShow?.Invoke(this, e);
        }

        #region Timer / Tooltip

        private void timerTooltip_Tick(object sender, EventArgs e)
        {
            _timerTooltip.Enabled = false;
            _moveStart = false;

            if (!this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
                return;

            Point currPos = this.PointToClient(MousePosition);
            ToolTipEventArgs args = new(GetItemAt(currPos.X, currPos.Y));

            RaiseTooltip(args);

            if (!args.AllowShow)
                return;

            currPos.X += 5;
            currPos.Y += 5;
            _tooltip.ToolTipIcon = args.Icon;
            _tooltip.IsBalloon = args.ShowBaloon;
            _tooltip.ToolTipTitle = args.Title;
            _tooltip.UseAnimation = false;
            _tooltip.Show(args.Text, this, currPos);
        }

        #endregion Timer / Tooltip

        #endregion Private Methods

        #region Events

        /// <summary>
        /// Event raised when tooltip needs to be shown
        /// </summary>
        public event ToolTipEventHandler ToolTipShow;

        #endregion Events
    }
}
