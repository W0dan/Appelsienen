using CL.Entity;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Appelsienen_MVP_WPF.graphics
{
    class ScoreChart : GraphicsBase
    {
        public DrawingImage CreateChart(List<ScoreSet> scoreSetList)
        {

            // Create the Axles:
            GeometryGroup axlesGroup = new GeometryGroup();

            axlesGroup.Children.Add(new LineGeometry(new Point(10, 520), new Point(10, 10)));
            axlesGroup.Children.Add(new LineGeometry(new Point(10, 520), new Point(470, 520)));

            GeometryDrawing axlesDrawing = new GeometryDrawing();
            axlesDrawing.Geometry = axlesGroup;
            axlesDrawing.Pen = new Pen(Brushes.DarkGray, 1);
            // Axles

            // Create the graphs:
            var minimaGroup = new GeometryGroup();
            var maximaGroup = new GeometryGroup();
            var gemiddeldeGroup = new GeometryGroup();
            var marksGroup = new GeometryGroup();
            // used for the painting
            var paintGroup = new GeometryGroup();
            var paintFigure = new PathFigure();
            var paintSegmentCollection = new PathSegmentCollection();
            // -painting

            var maximaPoint = new Point(0, 0);
            var minimaPoint = new Point(0, 0);
            var gemiddeldePoint = new Point(0, 0);

            List<Point> minimaPoints = new List<Point>();

            bool pointExists = false;

            int yDiff = 520;
            int xPos = 10;
            foreach (ScoreSet scoreSet in scoreSetList)
            {
                var thisMaximaPoint = new Point(xPos, yDiff - (double)scoreSet.Maximum * 50);
                var thisMinimaPoint = new Point(xPos, yDiff - (double)scoreSet.Minimum * 50);
                var thisGemiddeldePoint = new Point(xPos, yDiff - (double)scoreSet.Gemiddelde * 50);

                var gemiddeldeMarkSize = new Size(2, 2);
                marksGroup.Children.Add(new RectangleGeometry(new Rect(new Point(xPos - 1, thisGemiddeldePoint.Y - 1), gemiddeldeMarkSize)));
                marksGroup.Children.Add(new LineGeometry(new Point(xPos - 5, thisMaximaPoint.Y), new Point(xPos + 5, thisMaximaPoint.Y)));
                marksGroup.Children.Add(new LineGeometry(new Point(xPos - 5, thisMinimaPoint.Y), new Point(xPos + 5, thisMinimaPoint.Y)));
                marksGroup.Children.Add(new LineGeometry(new Point(xPos, thisMaximaPoint.Y), new Point(xPos, thisMinimaPoint.Y)));

                if (pointExists)
                {
                    maximaGroup.Children.Add(new LineGeometry(maximaPoint, thisMaximaPoint));
                    minimaGroup.Children.Add(new LineGeometry(minimaPoint, thisMinimaPoint));
                    gemiddeldeGroup.Children.Add(new LineGeometry(gemiddeldePoint, thisGemiddeldePoint));
                }
                else
                {
                    //painting
                    paintFigure.StartPoint = thisMinimaPoint;
                }
                // painting
                var lineSegment = new LineSegment();
                lineSegment.Point = thisMaximaPoint;
                paintSegmentCollection.Add(lineSegment);
                // -painting

                minimaPoints.Add(thisMinimaPoint);

                maximaPoint = thisMaximaPoint;
                minimaPoint = thisMinimaPoint;
                gemiddeldePoint = thisGemiddeldePoint;

                pointExists = true;

                xPos += 50;
            }

            // painting
            for (int i = minimaPoints.Count - 1; i >= 0; i--)
            {
                var lineSegment = new LineSegment
                {
                    Point = minimaPoints[i]
                };
                paintSegmentCollection.Add(lineSegment);
            }
            paintFigure.Segments = paintSegmentCollection;
            var paintFigureCollection = new PathFigureCollection();
            paintFigureCollection.Add(paintFigure);
            var paintGeometry = new PathGeometry();
            paintGeometry.Figures = paintFigureCollection;
            paintGroup.Children.Add(paintGeometry);
            // -painting

            var maximaDrawing = new GeometryDrawing();
            var minimaDrawing = new GeometryDrawing();
            var gemiddeldeDrawing = new GeometryDrawing();
            var marksDrawing = new GeometryDrawing();
            var paintDrawing = new GeometryDrawing();
            maximaDrawing.Geometry = maximaGroup;
            minimaDrawing.Geometry = minimaGroup;
            gemiddeldeDrawing.Geometry = gemiddeldeGroup;
            marksDrawing.Geometry = marksGroup;
            paintDrawing.Geometry = paintGroup;
            maximaDrawing.Pen = new Pen(Brushes.LightGray, 1);
            minimaDrawing.Pen = new Pen(Brushes.LightGray, 1);
            gemiddeldeDrawing.Pen = new Pen(Brushes.Black, 1);
            marksDrawing.Pen = new Pen(Brushes.DarkGoldenrod, 1);
            paintDrawing.Pen = new Pen(Brushes.LightGray, 1);
            // -graphs

            paintDrawing.Brush = Brushes.LightGray;

            var chartDrawing = new DrawingGroup();

            chartDrawing.Children.Add(axlesDrawing);
            chartDrawing.Children.Add(paintDrawing);
            chartDrawing.Children.Add(maximaDrawing);
            chartDrawing.Children.Add(minimaDrawing);
            chartDrawing.Children.Add(gemiddeldeDrawing);
            chartDrawing.Children.Add(marksDrawing);

            return new DrawingImage(chartDrawing);

        }

    }
}
