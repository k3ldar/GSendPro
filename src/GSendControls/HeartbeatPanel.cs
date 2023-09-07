/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  Service Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Service Manager under the GPL, then the GPL applies to all loadable 
 *  Service Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2010 - 2018 Simon Carter.  All Rights Reserved.
 *
 *  Product:  Service Manager
 *  
 *  File: HeartbeatPanel.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  07/07/2018  Simon Carter        Initially Created
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#pragma warning disable IDE0044

namespace GSendControls
{
    public class HeartbeatPanel : Panel
    {
        #region Private Members

        private readonly Pen _primaryPen;
        private readonly Pen _secondaryPen;
        private readonly Brush _backGroundBrush;
        private readonly Brush _primaryBrush;
        private readonly Brush _secondaryBrush;
        private readonly Brush _textBrush;
        private readonly Queue<int> _points;
        private float _highestPoint;
        private static StringFormat _format = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        #endregion Private Members

        #region Constructors

        public HeartbeatPanel()
        {
            BackGround = Color.LightCyan;
            PrimaryColor = Color.SlateGray;
            SecondaryColor = Color.DarkSeaGreen;
            MaximumPoints = 60;
            _points = new Queue<int>(MaximumPoints);

            _primaryBrush = new SolidBrush(PrimaryColor);
            _secondaryBrush = new SolidBrush(SecondaryColor);
            _backGroundBrush = new SolidBrush(BackGround);
            _primaryPen = new Pen(_primaryBrush);
            _secondaryPen = new Pen(_secondaryBrush);
            _textBrush = new SolidBrush(ForeColor);
            DoubleBuffered = true;
        }

        public HeartbeatPanel(Color backGround, Color primary, Color secondary)
        {
            BackGround = backGround;
            PrimaryColor = primary;
            SecondaryColor = secondary;
            MaximumPoints = 60;
            _points = new Queue<int>(MaximumPoints);

            _primaryBrush = new SolidBrush(PrimaryColor);
            _secondaryBrush = new SolidBrush(SecondaryColor);
            _backGroundBrush = new SolidBrush(BackGround);
            _primaryPen = new Pen(_primaryBrush);
            _secondaryPen = new Pen(_secondaryBrush);
            _textBrush = new SolidBrush(ForeColor);
            DoubleBuffered = true;
        }

        public HeartbeatPanel(Color backGround, Color primary, Color secondary, int maximumPoints)
        {
            BackGround = backGround;
            PrimaryColor = primary;
            SecondaryColor = secondary;
            MaximumPoints = maximumPoints;
            _points = new Queue<int>(MaximumPoints);

            _primaryBrush = new SolidBrush(PrimaryColor);
            _secondaryBrush = new SolidBrush(SecondaryColor);
            _backGroundBrush = new SolidBrush(BackGround);
            _primaryPen = new Pen(_primaryBrush);
            _secondaryPen = new Pen(_secondaryBrush);
            _textBrush = new SolidBrush(ForeColor);
            DoubleBuffered = true;
            AutoPoints = false;
        }

        #endregion Constructors

        #region Properties

        public Color BackGround { get; set; }

        public Color PrimaryColor { get; set; }

        public Color SecondaryColor { get; set; }

        public int MaximumPoints { get; set; }

        public bool AutoPoints { get; set; }

        public string GraphName { get; set; }

        #endregion Properties

        #region Public Methods

        public void AddPoint(int point)
        {
            if (!AutoPoints && (point < 0 || point > MaximumPoints))
                throw new ArgumentOutOfRangeException(nameof(point));

            if (_points.Count > 0 && _points.Count >= MaximumPoints)
                _points.TryDequeue(out int _);

            _points.Enqueue(point);

            if (AutoPoints)
            {
                _highestPoint = 0;

                foreach (int pt in _points)
                {
                    if (pt > _highestPoint)
                        _highestPoint = pt;
                }
            }

            Invalidate();
        }

        #endregion Public Methods

        #region Overridden Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawHeartBeat(e.Graphics, e.ClipRectangle);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _primaryPen?.Dispose();
                _primaryBrush?.Dispose();
                _secondaryBrush?.Dispose();
                _backGroundBrush?.Dispose();
                _secondaryPen?.Dispose();
                _textBrush?.Dispose();
            }
        }

        #endregion Overridden Methods

        #region Private Methods

        private void DrawHeartBeat(in Graphics graphics, in Rectangle rectangle)
        {
            int[] points = _points.ToArray();
            Rectangle fullArea = new(0, 0, rectangle.Width - 1, rectangle.Height - 1);
            SmoothingMode previousSmoothingMode = graphics.SmoothingMode;

            if (MaximumPoints < 1)
                MaximumPoints = points.Length - 1;

            float pointWidth = (float)fullArea.Width / MaximumPoints;
            float pointHeight = (float)fullArea.Height / (AutoPoints ? _highestPoint == 0 ? 100 : _highestPoint : 100);

            graphics.FillRectangle(_backGroundBrush, rectangle);

            if (points.Length == 0)
                return;

            float leftPos = fullArea.Width - (points.Length * pointWidth) + pointWidth;
            PointF lastPosition = new(leftPos, fullArea.Height - (pointHeight * points[0]));

            if (graphics.SmoothingMode != SmoothingMode.AntiAlias)
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                for (int i = 0; i < points.Length; i++)
                {
                    PointF currentPosition = new(leftPos, fullArea.Height - (pointHeight * points[i]));

                    using (GraphicsPath gp = new(FillMode.Winding))
                    {
                        gp.AddLine(lastPosition, currentPosition);
                        gp.AddLine(lastPosition.X, lastPosition.Y, lastPosition.X, fullArea.Height);
                        gp.AddLine(lastPosition.X, fullArea.Height, currentPosition.X, fullArea.Height);
                        gp.AddLine(currentPosition.X, fullArea.Height, currentPosition.X, currentPosition.Y);
                        graphics.DrawPath(_secondaryPen, gp);
                        graphics.FillPath(_secondaryBrush, gp);
                    }

                    graphics.DrawLine(_primaryPen, lastPosition, currentPosition);
                    lastPosition = currentPosition;
                    leftPos += pointWidth;
                }

                graphics.DrawString(GraphName, Font, _textBrush, rectangle, _format);

                graphics.DrawRectangle(_primaryPen, fullArea);
            }
            finally
            {
                if (graphics.SmoothingMode != previousSmoothingMode)
                    graphics.SmoothingMode = previousSmoothingMode;
            }
        }

        #endregion Private Methods
    }
}
