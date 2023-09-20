/*
 *  The contents of this file are subject to MIT Licence.  Please
 *  view License.txt for further details. 
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2015 Simon Carter
 *
 *  Purpose:  Simple control for selecting a colour
 *
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GSendControls
{
    /// <summary>
    /// Color selector control
    /// 
    /// Allows users to select colors
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        #region Constructors

        /// <summary>
        /// Constructor - Initialises new instance
        /// </summary>
        public ColorSelector()
        {
            InitializeComponent();

            LoadColors();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Description above color selector
        /// </summary>
        public string Description
        {
            get
            {
                return lblDescription.Text;
            }

            set
            {
                lblDescription.Text = value;
            }
        }

        /// <summary>
        /// Selected color
        /// </summary>
        public Color Color
        {
            get
            {
                Color Result = (Color)cmbColorSelector.SelectedItem;

                return Result;
            }

            set
            {
                foreach (Color color in cmbColorSelector.Items)
                {
                    if (color.Name == value.Name)
                    {
                        cmbColorSelector.SelectedItem = color;
                        break;
                    }
                }
            }
        }

        #endregion Properties

        #region Private Methods

        private void LoadColors()
        {
            cmbColorSelector.Items.Clear();

            foreach (KnownColor colorValue in Enum.GetValues(typeof(KnownColor)))
            {
                Color color = Color.FromKnownColor(colorValue);
                cmbColorSelector.Items.Add(color);
            }

            if (cmbColorSelector.SelectedIndex == -1)
                cmbColorSelector.SelectedIndex = 0;
        }

        private void cmbColorSelector_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            System.Drawing.Color currentColor = (System.Drawing.Color)cmbColorSelector.Items[e.Index];

            // Draw the background of the item.
            e.DrawBackground();

            // Create a square filled with the animals color. Vary the size
            // of the rectangle based on the length of the animals name.
            Rectangle rectangle = new Rectangle(2, e.Bounds.Top + 2,
                    e.Bounds.Height, e.Bounds.Height - 4);
            e.Graphics.FillRectangle(new SolidBrush(currentColor), rectangle);

	        e.Graphics.DrawString(currentColor.Name, cmbColorSelector.Font, System.Drawing.Brushes.Black, 
                new RectangleF(e.Bounds.X+rectangle.Width, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));


            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        #endregion Private Methods
    }
}
