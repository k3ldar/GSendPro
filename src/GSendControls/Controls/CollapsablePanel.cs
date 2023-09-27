/*
 *  The contents of this file are subject to MIT Licence.  Please
 *  view License.txt for further details. 
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2015 Simon Carter
 *
 *  Purpose:  Vertical/Horizontal collapsable panel
 *
 */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#pragma warning disable IDE1005 // Delegate invocation can be simplified
#pragma warning disable IDE0017 // initialization can be simplified

namespace GSendControls
{
#pragma warning disable 1591

    /// <summary>
    /// Collapsable Panel
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class CollapsablePanel : BaseControl
    {
        #region Private Members

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Original size when not collapsed
        /// </summary>
        private int _originalSize;

        #region Property Variables

        private Orientation _orientation = Orientation.Vertical;

        private bool _customImages;

        private Color _collapsedColorFrom;

        private Color _collapsedColorTo;

        private Color _expandColorFrom;

        private Color _expandColorTo;

        private string _headerText;

        private StringAlignment _headerTextAlign;

        private Font _headerFont;

        private Color _headerForeColor;

        private Image _expandImage;

        private Image _collapseImage;

        /// <summary>
        /// Height of collapsed panel
        /// </summary>
        private int _collapsedSize = 30;

        #endregion Property Variables

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Constructor - Initialises a new instance of the class
        /// </summary>
        public CollapsablePanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
            CustomImages = false;
            Collapsed = false;
            HeaderText = "Collapsable Panel";
            HeaderForeColor = Color.Black;
            HeaderTextAlign = StringAlignment.Near;
            HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
            CollapsedColorFrom = Color.DarkBlue;
            CollapsedColorTo = Color.LightBlue;
            ExpandColorFrom = Color.LightBlue;
            ExpandColorTo = Color.DarkBlue;
            ExpandImage = Resources.ExpandHorizontal;
            CollapseImage = Resources.CollapseHorizonal;
        }

        #endregion Constructors

        #region Properties

        [Description("Determines whether the panel is collapsed or not")]
        [Category("Behaviour")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool Collapsed { get; private set; }

        [Description("Determines whether the panel collapses horizontally or vertically")]
        [Category("Behaviour")]
        [DefaultValue(Orientation.Vertical)]
        public Orientation Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                if (Collapsed)
                    throw new NotSupportedException();

                _orientation = value;

                foreach (Control ctl in this.Controls)
                {
                    CheckControlPosition(ctl);
                }

                if (!CustomImages)
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            ExpandImage = Resources.ExpandHorizontal;
                            CollapseImage = Resources.CollapseHorizonal;
                            break;

                        case System.Windows.Forms.Orientation.Vertical:
                            ExpandImage = Resources.ExpandVertical;
                            CollapseImage = Resources.CollapseVertical;
                            break;
                    }
                }

                this.Invalidate(true);
            }
        }

        [Description("Determines wether custom images are displayed, or not.  Standard images will be displayed if false")]
        [Category("Header")]
        [DefaultValue(false)]
        public bool CustomImages 
        { 
            get
            {
                return _customImages;
            }
            
            set
            {
                _customImages = value;

                if (_customImages)
                {
                    if (ExpandImage != null)
                    {
                        ExpandImage.Dispose();
                        ExpandImage = null;
                    }

                    if (CollapseImage != null)
                    {
                        CollapseImage.Dispose();
                        CollapseImage = null;
                    }
                }
                else
                {
                    switch (Orientation)
                    {
                        case System.Windows.Forms.Orientation.Horizontal:
                            ExpandImage = Resources.ExpandHorizontal;
                            CollapseImage = Resources.CollapseHorizonal;
                            break;

                        case System.Windows.Forms.Orientation.Vertical:
                            ExpandImage = Resources.ExpandVertical;
                            CollapseImage = Resources.CollapseVertical;
                            break;
                    }
                }

                this.Invalidate(!Collapsed);
            }
        }

        [Description("Height of the Panel when collapsed")]
        [Category("Header")]
        [DefaultValue(30)]
        public int CollapsedSize
        {
            get
            {
                return _collapsedSize;
            }

            set
            {
                _collapsedSize = value;

                this.Invalidate(true);
            }
        }

        [Description("Starting Color of panel when collapsed")]
        [Category("Header")]
        public Color CollapsedColorFrom 
        { 
            get
            {
                return _collapsedColorFrom;
            }

            set
            {
                _collapsedColorFrom = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("End Color of panel when collapsed")]
        [Category("Header")]
        public Color CollapsedColorTo
        {
            get
            {
                return _collapsedColorTo;
            }

            set
            {
                _collapsedColorTo = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Starting Color of panel when collapsed")]
        [Category("Header")]
        public Color ExpandColorFrom
        {
            get
            {
                return _expandColorFrom;
            }

            set
            {
                _expandColorFrom = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("End Color of panel when collapsed")]
        [Category("Header")]
        public Color ExpandColorTo
        {
            get
            {
                return _expandColorTo;
            }

            set
            {
                _expandColorTo = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Text displayed on header")]
        [Category("Header")]
        public string HeaderText
        {
            get
            {
                return _headerText;
            }

            set
            {
                _headerText = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Header Text Alignment")]
        [Category("Header")]
        public StringAlignment HeaderTextAlign
        {
            get
            {
                return _headerTextAlign;
            }

            set
            {
                _headerTextAlign = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Header Font")]
        [Category("Header")]
        public Font HeaderFont
        {
            get
            {
                return _headerFont;
            }

            set
            {
                _headerFont = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Header text color")]
        [Category("Header")]
        public Color HeaderForeColor
        {
            get
            {
                return _headerForeColor;
            }

            set
            {
                _headerForeColor = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Image displayed when the control can be expanded")]
        [Category("Header")]
        public Image ExpandImage
        {
            get
            {
                return _expandImage;
            }

            set
            {
                _expandImage = value;
                Invalidate(!Collapsed);
            }
        }

        [Description("Image displayed when the control can be expanded")]
        [Category("Header")]
        public Image CollapseImage
        {
            get
            {
                return _collapseImage;
            }

            set
            {
                _collapseImage = value;
                Invalidate(!Collapsed);
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Forces the panel to collapse, if expanded
        /// </summary>
        public virtual void Collapse()
        {
            if (Collapsed)
                return;

            if (RaiseBeforeCollapse())
                return;

            if (_orientation == Orientation.Horizontal)
            {
                _originalSize = Height;
                Height = CollapsedSize;
            }
            else
            {
                _originalSize = Width;
                Width = CollapsedSize;
            }

            RaiseAfterCollapse();

            Invalidate(false);
            Collapsed = true;
            Update();
        }

        /// <summary>
        /// Forces the panel to expand, if collapsed
        /// </summary>
        public virtual void Expand()
        {
            if (!Collapsed)
                return;

            if (RaiseBeforeExpand())
                return;

            if (_orientation == System.Windows.Forms.Orientation.Horizontal)
                Height = _originalSize;
            else
                Width = _originalSize;

            RaiseAfterExpand();

            Invalidate(true);
            Collapsed = false;
            Update();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Draw the header
        /// </summary>
        /// <param name="g">Graphic object being drawn to</param>
        /// <param name="rect">Size of the area to draw</param>
        private void DrawHeader(Graphics g)
        {
            if (g == null)
                throw new ArgumentNullException(nameof(g));

            Rectangle headerRect;

            LinearGradientMode mode = Orientation == Orientation.Horizontal ? 
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            if (Collapsed)
            {
                headerRect = new Rectangle(0, 0, Width -1, Height -1);

                using LinearGradientBrush aGB = new(headerRect, CollapsedColorFrom, CollapsedColorTo, mode);
                g.FillRectangle(aGB, headerRect);
            }
            else
            {
                headerRect = new Rectangle(0, 0,
                    Orientation == Orientation.Horizontal ? Width -1 : CollapsedSize -1,
                    Orientation == Orientation.Horizontal ? CollapsedSize - 1 : Height - 1);

                using LinearGradientBrush aGB = new(headerRect, ExpandColorFrom, ExpandColorTo, mode);
                g.FillRectangle(aGB, headerRect);
            }

            Image expandCollapsIndicator = Collapsed ? ExpandImage : CollapseImage;
            int imageOffset = 10;

            // draw image
            if (expandCollapsIndicator != null)
            {
                // where will the image be located
                Point imageLocation;

                if (Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    imageOffset += expandCollapsIndicator.Width;
                    imageLocation = new Point(Width - (expandCollapsIndicator.Width + 10), 
                        (CollapsedSize - expandCollapsIndicator.Height) / 2);
                    headerRect = new Rectangle(headerRect.Left, headerRect.Top, 
                        headerRect.Right - imageOffset, headerRect.Bottom);
                }
                else
                {
                    imageOffset += expandCollapsIndicator.Height;
                    imageLocation = new Point((CollapsedSize - expandCollapsIndicator.Height) / 2, 10);
                    headerRect = new Rectangle(headerRect.Left, headerRect.Top + imageOffset, 
                        headerRect.Right, headerRect.Bottom - imageOffset);
                }

                // draw images if they are set
                g.DrawImage(expandCollapsIndicator, imageLocation);
            }

            // draw text
            SolidBrush fntBrush = new(HeaderForeColor);
            StringFormat format = new();
            format.Alignment = HeaderTextAlign;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisWord;

            if (Orientation == Orientation.Vertical)
            {
                format.FormatFlags = StringFormatFlags.DirectionVertical;
                g.TranslateTransform(headerRect.Right, headerRect.Bottom + headerRect.Top, MatrixOrder.Prepend);
                g.RotateTransform(180);
            }

            if (RightToLeft == RightToLeft.Yes)
            {
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }

            g.DrawString(HeaderText, HeaderFont, fntBrush, headerRect, format);

            if (Orientation == Orientation.Vertical)
                g.ResetTransform();
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        /// <summary>
        /// Checks the position of child controls, ensuring they don't overlap the header
        /// </summary>
        /// <param name="control">Control who's position is being verified</param>
        private void CheckControlPosition(Control control)
        {
            if (Orientation == Orientation.Horizontal)
            {
                if (control.Top <= CollapsedSize)
                    control.Top = CollapsedSize + 1;
            }
            else
            {
                if (control.Left <= CollapsedSize)
                    control.Left = CollapsedSize + 1;
            }
        }

        /// <summary>
        /// When child control is moved, ensures it does not overlap the header
        /// </summary>
        /// <param name="sender">child control</param>
        /// <param name="e">Empty Event Args</param>
        private void Control_Move(object sender, EventArgs e)
        {
            CheckControlPosition((Control)sender);
        }

        #endregion Private Methods

        #region Overridden Methods

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            HeaderFont.Dispose();
            HeaderFont = null;

            base.Dispose(disposing);
        }

        /// <summary>
        /// Overridden OnControlAdded method
        /// </summary>
        /// <param name="e">ControlEventArguments</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            CheckControlPosition(e.Control);
            e.Control.Move += Control_Move;
            base.OnControlAdded(e);
        }

        /// <summary>
        /// Overridden OnControlRemoved method
        /// </summary>
        /// <param name="e">ControlEventArguments</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            CheckControlPosition(e.Control);
            e.Control.Move -= Control_Move;
            base.OnControlRemoved(e);
        }

        /// <summary>
        /// Overridden OnPaint method
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawHeader(e.Graphics);
        }

        /// <summary>
        /// Overridden OnMouseClick method
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (Collapsed)
            {
                Expand();
            }
            else
            {
                Rectangle headerRect = new(0, 0,
                    Orientation == Orientation.Horizontal ? Width - 2 : CollapsedSize - 2,
                    Orientation == Orientation.Horizontal ? CollapsedSize - 2 : Width - 2);

                if (headerRect.Contains(e.X, e.Y))
                    Collapse();
            }

            base.OnMouseClick(e);
        }

        /// <summary>
        /// Overridden OnResize method
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(!Collapsed);
        }

        #endregion Overridden Methods

        #region Event Raise Wrappers

        /// <summary>
        /// Event raised after the panel has been collapsed
        /// </summary>
        protected virtual void RaiseAfterCollapse()
        {
            if (AfterCollapse != null)
                AfterCollapse(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event raised before the panel is collapsed
        /// </summary>
        /// <returns></returns>
        protected virtual bool RaiseBeforeCollapse()
        {
            bool Result = false;

            if (BeforeCollapse != null)
            {
                CancelEventArgs args = new(false);
                BeforeCollapse(this, args);
                Result = args.Cancel;
            }

            return Result;
        }

        /// <summary>
        /// Event raised after the panel has been collapsed
        /// </summary>
        protected virtual void RaiseAfterExpand()
        {
            if (AfterExpand != null)
                AfterExpand(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event raised before the panel is collapsed
        /// </summary>
        /// <returns></returns>
        protected virtual bool RaiseBeforeExpand()
        {
            bool Result = false;

            if (BeforeExpand != null)
            {
                CancelEventArgs args = new(false);
                BeforeExpand(this, args);
                Result = args.Cancel;
            }

            return Result;
        }

        #endregion Event Raise Wrappers

        #region Events

        [Description("Event fired after the panel is collapsed")]
        [Category("Collapse")]
        public event EventHandler AfterCollapse;

        [Description("Event fired before the panel is collapsed")]
        [Category("Collapse")]
        public event CancelEventHandler BeforeCollapse;

        [Description("Event fired after the panel is expanded")]
        [Category("Expand")]
        public event EventHandler AfterExpand;

        [Description("Event fired before the panel is expanded")]
        [Category("Expand")]
        public event CancelEventHandler BeforeExpand;

        #endregion Events
    }

#pragma warning restore 1591
}
