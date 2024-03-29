﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public sealed class Machine2DView : Panel
    {
        private readonly Pen _outerPen = new(Color.Black, 1);
        private readonly Pen _locationPen = new(Color.Red, 1);
        private readonly Brush _fillBrush = new SolidBrush(Color.White);

        private bool _lockUpdate = true;
        private Image _gCodeImage = null;
        private Image _zoomImage = null;
        private AxisConfiguration _configuration;
        private float _xPosition;
        private float _yPosition;

        private bool _mouseOverControl = false;

        public Machine2DView()
        {
            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.None;
        }

        public void LoadGCode(IGCodeAnalyses _gCodeAnalyses)
        {
            if (IsDisposed)
                return;

            _gCodeImage = new Bitmap(MachineSize.Width + 1, MachineSize.Height + 1);
            using Graphics g = Graphics.FromImage(_gCodeImage);
            using Pen layerPen = new(Color.Black, 1);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            PointF latestPos = new(XDirectionWithInversion(0f), YDirectionWithInversion(0f));

            foreach (IGCodeLine line in _gCodeAnalyses.AllLines(out int _))
            {
                IGCodeCommand gCommand = line.Commands.Find(c => c.Command.Equals('G'));
                IGCodeCommand xCommand = line.Commands.Find(c => c.Command.Equals('X'));
                IGCodeCommand yCommand = line.Commands.Find(c => c.Command.Equals('Y'));
                IGCodeCommand iCommand = line.Commands.Find(c => c.Command.Equals('I'));
                IGCodeCommand jCommand = line.Commands.Find(c => c.Command.Equals('J'));
                IGCodeCommand rCommand = line.Commands.Find(c => c.Command.Equals('R'));

                PointF newLocation = latestPos;

                if (xCommand == null && yCommand == null)
                    continue;

                if (xCommand != null)
                    newLocation.X = XDirectionWithInversion((float)xCommand.CurrentX);

                if (yCommand != null)
                    newLocation.Y = YDirectionWithInversion((float)yCommand.CurrentY);

                bool radiusDrawn = false;

                if (gCommand != null)
                {
                    switch (gCommand.CommandValue)
                    {
                        case 2:
                        case 3:
                            float radius = CalculateRadius((double)iCommand.CommandValue, (double)jCommand.CommandValue, rCommand);
                            DrawArcBetweenTwoPoints(g, layerPen, latestPos, newLocation, radius, gCommand.CommandValue.Equals(3));
                            radiusDrawn = true;
                            break;
                    }
                }

                if (!radiusDrawn)
                    g.DrawLine(layerPen, latestPos, newLocation);

                latestPos = newLocation;
            }

            UpdateImage(false);
        }

        public void UnloadGCode()
        {
            _gCodeImage?.Dispose();
            _gCodeImage = null;
            UpdateImage(false);
        }

        public AxisConfiguration Configuration
        {
            get => _configuration;

            set
            {
                if (_configuration == value)
                    return;

                _configuration = value;
                UpdateImage(false);
            }
        }

        public float XPosition
        {
            get => _xPosition;

            set
            {
                if (_xPosition == value)
                    return;

                _xPosition = value;
                UpdateImage(false);
            }
        }
        public float YPosition
        {
            get => _yPosition;

            set
            {
                if (_yPosition == value)
                    return;

                _yPosition = value;
                UpdateImage(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _outerPen.Dispose();
            _locationPen.Dispose();
            _fillBrush.Dispose();
            _gCodeImage?.Dispose();
            MachineImage?.Dispose();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateImage(false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _mouseOverControl = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseOverControl = false;

            if (ZoomPanel != null)
            {
                ZoomPanel.BackgroundImage?.Dispose();
                ZoomPanel.BackgroundImage = null;
                _zoomImage?.Dispose();
                _zoomImage = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_gCodeImage == null || ZoomPanel == null || !_mouseOverControl)
                return;

            int ZoomSize = Math.Min(45, MachineSize.Width / 4);
            Point mouseLocation = PointToClient(Cursor.Position);
            Rectangle sourceLocation = new(mouseLocation.X - ZoomSize, mouseLocation.Y - ZoomSize, ZoomSize * 2, ZoomSize * 2);

            _zoomImage ??= new Bitmap(ZoomPanel.ClientRectangle.Width, ZoomPanel.ClientRectangle.Height);

            BackgroundImage.CopyRegionIntoImage(sourceLocation, ref _zoomImage, ZoomPanel.ClientRectangle);
            ZoomPanel.BackgroundImage = null;
            ZoomPanel.BackgroundImage = _zoomImage;
        }

        public bool LockUpdate
        {
            get => _lockUpdate;

            set
            {
                if (value && !_lockUpdate)
                    UpdateImage(true);

                _lockUpdate = value;
            }
        }

        public Panel ZoomPanel { get; set; }

        public Image MachineImage { get; private set; }

        public Rectangle MachineSize { get; set; }

        public event EventHandler ImageUpdated;

        private static float CalculateRadius(double offsetX, double offsetY, IGCodeCommand rCommand)
        {
            if (rCommand != null)
                return (float)rCommand.CommandValue;

            return (float)Math.Sqrt((offsetX * offsetX) + (offsetY * offsetY));
        }
        private static void DrawArcBetweenTwoPoints(Graphics g, Pen pen, PointF a, PointF b, float radius, bool flip = false)
        {
            if (flip)
            {
                (a, b) = (b, a);
            }

            // get distance components
            double x = b.X - a.X, y = b.Y - a.Y;
            // get orientation angle
            double θ = Math.Atan2(y, x);
            // length between A and B
            double l = Math.Sqrt((x * x) + (y * y));

            if (2 * radius >= l)
            {
                // find the sweep angle (actually half the sweep angle)
                double sweepAngle = Math.Asin(l / (2 * radius));
                // triangle height from the chord to the center
                double h = radius * Math.Cos(sweepAngle);
                // get center point. 
                // Use sin(θ)=y/l and cos(θ)=x/l
                PointF C = new(
                    (float)(a.X + (x / 2) - (h * (y / l))),
                    (float)(a.Y + (y / 2) + (h * (x / l))));


                // Conversion factor between radians and degrees
                const double to_deg = 180 / Math.PI;

                // Draw arc based on square around center and start/sweep angles
                g.DrawArc(pen, C.X - radius, C.Y - radius, 2 * radius, 2 * radius,
                    (float)((θ - sweepAngle) * to_deg) - 90, (float)(2 * sweepAngle * to_deg));
            }
        }

        private void UpdateImage(bool force)
        {
            if (_lockUpdate && !force)
                return;

            MachineImage = DrawMachine();
            BackgroundImage = MachineImage.ResizeImage(Width, Height, true, false);
            ImageUpdated?.Invoke(this, EventArgs.Empty);
        }

        private Image DrawMachine()
        {
            Image machineImage = new Bitmap(MachineSize.Width + 1, MachineSize.Height + 1);
            using Graphics g = Graphics.FromImage(machineImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(_fillBrush, MachineSize);

            if (_gCodeImage != null)
                g.DrawImage(_gCodeImage, 0, 0);

            g.DrawRectangle(_locationPen, XDirectionWithInversion(XPosition) - 3, YDirectionWithInversion(YPosition) - 3, 6, 6);
            g.FillRectangle(new SolidBrush(Color.Black), XDirectionWithInversion(XPosition) - 2, YDirectionWithInversion(YPosition) - 2, 4, 4);

            return machineImage;
        }

        private float XDirectionWithInversion(float position)
        {
            switch (Configuration)
            {
                case AxisConfiguration.ReverseX:
                case AxisConfiguration.ReverseXandY:
                case AxisConfiguration.ReverseXandZ:
                case AxisConfiguration.ReversAll:
                    return position;

                default:
                    return MachineSize.Width - position;
            }
        }

        private float YDirectionWithInversion(float position)
        {
            switch (Configuration)
            {
                case AxisConfiguration.ReverseY:
                case AxisConfiguration.ReverseXandY:
                case AxisConfiguration.ReverseYandZ:
                case AxisConfiguration.ReversAll:
                    return MachineSize.Height - position;

                default:
                    return position;
            }
        }
    }
    public static class ImageExtensions
    {
        public static void CopyRegionIntoImage(this Image srcBitmap, Rectangle srcRegion, ref Image destBitmap, Rectangle destRegion)
        {
            using Graphics grD = Graphics.FromImage(destBitmap);
            grD.FillRectangle(new SolidBrush(Color.White), destRegion);
            grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
        }

        public static Image ResizeImage(this Image image, int maximumWidth, int maximumHeight, bool enforceRatio, bool addPadding)
        {
            EncoderParameters encoderParameters = new(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            int canvasWidth = maximumWidth;
            int canvasHeight = maximumHeight;
            int newImageWidth = maximumWidth;
            int newImageHeight = maximumHeight;
            int xPosition = 0;
            int yPosition = 0;

            if (enforceRatio)
            {
                double ratioX = maximumWidth / (double)image.Width;
                double ratioY = maximumHeight / (double)image.Height;
                double ratio = ratioX < ratioY ? ratioX : ratioY;
                newImageHeight = (int)(image.Height * ratio);
                newImageWidth = (int)(image.Width * ratio);

                if (addPadding)
                {
                    xPosition = (int)((maximumWidth - (image.Width * ratio)) / 2);
                    yPosition = (int)((maximumHeight - (image.Height * ratio)) / 2);
                }
                else
                {
                    canvasWidth = newImageWidth;
                    canvasHeight = newImageHeight;
                }
            }

            Bitmap thumbnail = new(canvasWidth, canvasHeight);
            Graphics graphic = Graphics.FromImage(thumbnail);

            if (enforceRatio && addPadding)
            {
                graphic.Clear(Color.White);
            }

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.DrawImage(image, xPosition, yPosition, newImageWidth, newImageHeight);

            return thumbnail;
        }
    }
}
