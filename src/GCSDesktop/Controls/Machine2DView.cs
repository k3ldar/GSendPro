using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    internal class Machine2DView : Panel
    {
        private readonly Pen _outerPen = new Pen(Color.Black, 1); 
        private readonly Pen _locationPen = new Pen(Color.Red, 1); 
        private readonly Brush _fillBrush = new SolidBrush(Color.White);

        private Image _gCodeImage = null;
        private Image _zoomImage = null;
        private AxisConfiguration _configuration; 
        private float _xPosition; 
        private float _yPosition;

        private bool _mouseOverControl = false;

        public Machine2DView() 
        { 
            DoubleBuffered = true; 
        }

        public void LoadGCode(IGCodeAnalyses _gCodeAnalyses)
        {
            _gCodeImage = new Bitmap(MachineSize.Width + 1, MachineSize.Height + 1);
            using Graphics g = Graphics.FromImage(_gCodeImage);
            using Pen layerPen = new Pen(Color.Black, 1);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            PointF latestPos = new PointF(XDirectionWithInversion(0f), YDirectionWithInversion(0f));

            foreach (IGCodeLine line in _gCodeAnalyses.Lines(out int _))
            {
                IGCodeCommand gCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('G'));
                IGCodeCommand xCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('X'));
                IGCodeCommand yCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('Y'));
                IGCodeCommand iCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('I'));
                IGCodeCommand jCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('J'));
                IGCodeCommand rCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('R'));

                PointF newLocation = latestPos;

                if (xCommand == null && yCommand == null)
                    continue;

                if (xCommand != null)
                    newLocation.X = XDirectionWithInversion((float)xCommand.CurrentX);

                if (yCommand != null)
                    newLocation.Y = YDirectionWithInversion((float)yCommand.CurrentY);

                //bool radiusDrawn = false;

                //if (gCommand != null)
                //{
                //    switch (gCommand.CommandValue)
                //    {
                //        case 2:
                //        case 3:
                //            float radius = CalculateRadius(latestPos, newLocation, iCommand, jCommand, rCommand);
                //            DrawArcBetweenTwoPoints(g, layerPen, latestPos, newLocation, radius, gCommand.CommandValue.Equals(3));
                //            radiusDrawn = true;
                //            break;
                //    }
                //}

                //if (!radiusDrawn)
                    g.DrawLine(layerPen, latestPos, newLocation);

                //if (iCommand != null && jCommand != null)
                //{
                //    //g.DrawArc(new Pen(Color.Red, 1f), (float)latestPos.X, (float)latestPos.Y, (float)newLocation.X, (float)newLocation.Y, (float)iCommand.CommandValue, (float)jCommand.CommandValue);
                //}
                //else
                //{ 
                //if (gCommand == null || gCommand.CommandValue.Equals(1))
                //{

                //}
                //else if (gCommand.CommandValue == 2 || gCommand.CommandValue == 3)
                //{

                //    if (iCommand != null && jCommand != null)
                //    {
                //        //Arc2d arc = new g3.Arc2d(new g3.Vector2d(iparam, jparam), new g3.Vector2d(xstartpos, ystartpos), new g3.Vector2d(x, y));

                //        g.DrawArc(new Pen(Color.Red, 1f), (float)newLocation.X, (float)newLocation.Y, (float)latestPos.X, (float)latestPos.Y, (float)iCommand.CommandValue, (float)jCommand.CommandValue);
                //    }
                //}

                latestPos = newLocation;
            }

            UpdateImage();
        }

        //private float CalculateRadius(PointF startPos, PointF endPos, IGCodeCommand iCommand, IGCodeCommand jCommand, IGCodeCommand rCommand)
        //{
        //    if (rCommand != null)
        //    {
        //        return (float)rCommand.CommandValue;
        //    }

        //    //PointF centerCircle = new PointF(startPos.X / 2, startPos.Y / 2);
        //    double dx = (double)startPos.X - (double)iCommand.CommandValue;
        //    double dy = (double)startPos.Y - (double)jCommand.CommandValue;

        //    //double circleRadius = Math.Sqrt((dx * dx) + (dy * dy));
        //    return (float)(dx + dy);//circleRadius;
        //}

        public void UnloadGCode()
        {
            _gCodeImage?.Dispose();
            _gCodeImage = null;
            UpdateImage();
        }

        public AxisConfiguration Configuration 
        { 
            get => _configuration; 

            set 
            { 
                if (_configuration == value) 
                    return; 

                _configuration = value; 
                UpdateImage(); 
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
                UpdateImage(); 
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
                UpdateImage(); 
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
            UpdateImage(); 
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
                ZoomPanel.BackgroundImage.Dispose();
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
            Rectangle sourceLocation = new Rectangle(mouseLocation.X - ZoomSize, mouseLocation.Y - ZoomSize, ZoomSize * 2, ZoomSize * 2);

            if (_zoomImage == null)
                _zoomImage = new Bitmap(ZoomPanel.ClientRectangle.Width, ZoomPanel.ClientRectangle.Height);

            BackgroundImage.CopyRegionIntoImage(sourceLocation, ref _zoomImage, ZoomPanel.ClientRectangle);
            ZoomPanel.BackgroundImage = null;
            ZoomPanel.BackgroundImage = _zoomImage;
            Trace.WriteLine($"Image Updated ({e.X},{e.Y} - {sourceLocation}");
        }

        public Panel ZoomPanel { get; set; }

        public Image MachineImage { get; private set; }

        public Rectangle MachineSize { get; set; }

        public event EventHandler ImageUpdated;

        //private void DrawArcBetweenTwoPoints(Graphics g, Pen pen, PointF a, PointF b, float radius, bool flip = false)
        //{
        //    if (flip)
        //    {
        //        PointF temp = b;
        //        b = a;
        //        a = temp;
        //    }

        //    // get distance components
        //    double x = b.X - a.X, y = b.Y - a.Y;
        //    // get orientation angle
        //    double θ = Math.Atan2(y, x);
        //    // length between A and B
        //    double l = Math.Sqrt((x * x) + (y * y));

        //    if (2 * radius >= l)
        //    {
        //        // find the sweep angle (actually half the sweep angle)
        //        double sweepAngle = Math.Asin(l / (2 * radius));
        //        // triangle height from the chord to the center
        //        double h = radius * Math.Cos(sweepAngle);
        //        // get center point. 
        //        // Use sin(θ)=y/l and cos(θ)=x/l
        //        PointF C = new PointF(
        //            (float)(a.X + (x / 2) - (h * (y / l))),
        //            (float)(a.Y + (y / 2) + (h * (x / l))));

        //        g.DrawLine(Pens.DarkGray, C, a);
        //        g.DrawLine(Pens.DarkGray, C, b);

        //        // Conversion factor between radians and degrees
        //        const double to_deg = 180 / Math.PI;

        //        // Draw arc based on square around center and start/sweep angles
        //        g.DrawArc(pen, C.X - radius, C.Y - radius, 2 * radius, 2 * radius,
        //            (float)((θ - sweepAngle) * to_deg) - 90, (float)(2 * sweepAngle * to_deg));
        //    }
        //}

        private void UpdateImage() 
        { 
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

            g.DrawRectangle(_outerPen, MachineSize); 
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
                    return MachineSize.Width - position; 

                default: 
                    return position; 
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
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.FillRectangle(new SolidBrush(Color.White), destRegion);
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        public static Image ResizeImage(this Image image, int maximumWidth, int maximumHeight, bool enforceRatio, bool addPadding)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders(); EncoderParameters encoderParameters = new EncoderParameters(1); 
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
                    canvasWidth = newImageWidth; canvasHeight = newImageHeight; 
                } 
            }

            Bitmap thumbnail = new Bitmap(canvasWidth, canvasHeight);
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
