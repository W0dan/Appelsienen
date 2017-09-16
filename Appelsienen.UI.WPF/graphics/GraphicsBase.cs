using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Appelsienen_MVP_WPF.graphics
{
    abstract class GraphicsBase
    {
        protected Point CalculateMidpoint(Point a, Point b)
        {
            double x = Math.Abs(a.X - b.X);
            double y = Math.Abs(a.Y - b.Y);

            if (a.X < b.X)
                x += a.X;
            else
                x += b.X;

            if (a.Y < b.Y)
                y += a.Y;
            else
                y += b.Y;

            return new Point(x, y);
        }
    }
}
