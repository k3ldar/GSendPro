using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormsApp4
{
    public static class GraphicsExtensions
    {
        public static void DrawTriangle(this Graphics graphics, PointF p, SolidBrush brush, float size, bool invert)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            PointF[] points;

            if (invert)
            {
                points = new PointF[]
                {
          p,
          new PointF(p.X - size, p.Y + (size * (float)Math.Sqrt(3))),
          new PointF(p.X + size, p.Y + (size * (float)Math.Sqrt(3)))
                };
            }
            else
            {
                p = new PointF(p.X, p.Y + (size * 2));
                points = new PointF[]
                {
          p,
          new PointF(p.X - size, p.Y - (size * (float)Math.Sqrt(3))),
          new PointF(p.X + size, p.Y - (size * (float)Math.Sqrt(3)))
                };
            }

            graphics.FillPolygon(brush, points);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF bounds, int radiusTop, int radiusBottom)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            using GraphicsPath path = RoundedRect(bounds, radiusTop, radiusTop, radiusBottom, radiusBottom);
            graphics.DrawPath(pen, path);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            using GraphicsPath path = RoundedRect(bounds, cornerRadius, cornerRadius, cornerRadius, cornerRadius);
            graphics.DrawPath(pen, path);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            using GraphicsPath path = RoundedRect(bounds, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            graphics.DrawPath(pen, path);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF bounds, int radiusTop, int radiusBottom)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using GraphicsPath path = RoundedRect(bounds, radiusTop, radiusTop, radiusBottom, radiusBottom);
            graphics.FillPath(brush, path);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using GraphicsPath path = RoundedRect(bounds, cornerRadius, cornerRadius, cornerRadius, cornerRadius);
            graphics.FillPath(brush, path);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using GraphicsPath path = RoundedRect(bounds, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            graphics.FillPath(brush, path);
        }

        public static GraphicsPath RoundedRect(RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            int diameter1 = radiusTopLeft * 2;
            int diameter2 = radiusTopRight * 2;
            int diameter3 = radiusBottomRight * 2;
            int diameter4 = radiusBottomLeft * 2;

            RectangleF arc1 = new(bounds.Location, new Size(diameter1, diameter1));
            RectangleF arc2 = new(bounds.Location, new Size(diameter2, diameter2));
            RectangleF arc3 = new(bounds.Location, new Size(diameter3, diameter3));
            RectangleF arc4 = new(bounds.Location, new Size(diameter4, diameter4));
            GraphicsPath path = new();

            // top left arc  
            if (radiusTopLeft == 0)
            {
                path.AddLine(arc1.Location, arc1.Location);
            }
            else
            {
                path.AddArc(arc1, 180, 90);
            }

            // top right arc  
            arc2.X = bounds.Right - diameter2;
            if (radiusTopRight == 0)
            {
                path.AddLine(arc2.Location, arc2.Location);
            }
            else
            {
                path.AddArc(arc2, 270, 90);
            }

            // bottom right arc  

            arc3.X = bounds.Right - diameter3;
            arc3.Y = bounds.Bottom - diameter3;
            if (radiusBottomRight == 0)
            {
                path.AddLine(arc3.Location, arc3.Location);
            }
            else
            {
                path.AddArc(arc3, 0, 90);
            }

            // bottom left arc 
            arc4.X = bounds.Right - diameter4;
            arc4.Y = bounds.Bottom - diameter4;
            arc4.X = bounds.Left;
            if (radiusBottomLeft == 0)
            {
                path.AddLine(arc4.Location, arc4.Location);
            }
            else
            {
                path.AddArc(arc4, 90, 90);
            }

            path.CloseFigure();
            return path;
        }
    }
}
