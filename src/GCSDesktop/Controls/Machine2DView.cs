using System;
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
        private AxisConfiguration _configuration; 
        private float _xPosition; 
        private float _yPosition; 
        
        public Machine2DView() 
        { 
            DoubleBuffered = true; 
        }

        public void LoadGCode(IGCodeAnalyses _gCodeAnalyses)
        {
            _gCodeImage = new Bitmap(MachineSize.Width + 1, MachineSize.Height + 1);
            using Graphics g = Graphics.FromImage(_gCodeImage);
            using Pen layerPen = new Pen(Color.Black, 1);

            PointF latestPos = new PointF(XDirectionWithInversion(0f), YDirectionWithInversion(0f));

            foreach (IGCodeLine line in _gCodeAnalyses.Lines(out int _))
            {
                IGCodeCommand gCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('G'));
                IGCodeCommand xCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('X'));
                IGCodeCommand yCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('Y'));
                IGCodeCommand iCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('I'));
                IGCodeCommand jCommand = line.Commands.FirstOrDefault(c => c.Command.Equals('J'));

                PointF newLocation = latestPos;

                if (xCommand == null && yCommand == null)
                    continue;

                if (xCommand != null)
                    newLocation.X = XDirectionWithInversion((float)xCommand.CurrentX);

                if (yCommand != null)
                    newLocation.Y = YDirectionWithInversion((float)yCommand.CurrentY);

                //if (iCommand != null && jCommand != null)
                //{
                //    //g.DrawArc(new Pen(Color.Red, 1f), (float)latestPos.X, (float)latestPos.Y, (float)newLocation.X, (float)newLocation.Y, (float)iCommand.CommandValue, (float)jCommand.CommandValue);
                //}
                //else
                //{ 
                //if (gCommand == null || gCommand.CommandValue.Equals(1))
                //{
                    g.DrawLine(layerPen, latestPos, newLocation);
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

        public Image MachineImage { get; private set; }

        public Rectangle MachineSize { get; set; }

        public event EventHandler ImageUpdated; 
        
        private void UpdateImage() 
        { 
            MachineImage = DrawMachine(); 
            BackgroundImage = MachineImage.ResizeImage(Width, Height, true, true); 
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

                default: return position; 
            } 
        }
    }
    public static class ImageExtensions 
    { 
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
