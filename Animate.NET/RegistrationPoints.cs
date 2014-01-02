using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Animator
{
    public class RegistrationPoints
    {
        public FrameworkElement Element { get; private set; }

        public static DependencyProperty RotateRegistrationPointProperty = DependencyProperty.RegisterAttached("RotateRegistrationPoint",
            typeof(Point), typeof(RegistrationPoints), new PropertyMetadata(new Point(0, 0)));

        public static DependencyProperty PositionRegistrationPointProperty = DependencyProperty.RegisterAttached("PositionRegistrationPoint",
            typeof(Point), typeof(RegistrationPoints), new PropertyMetadata(new Point(0, 0)));

        public RegistrationPoints(FrameworkElement Element)
        {
            this.Element = Element;
        }

        public Point Relative(double x, double y)
        {
            if (x < 0 || x > 1)
                throw new ArgumentException("x must be between 0 and 1");
            if (y < 0 || y > 1)
                throw new ArgumentException("y must be between 0 and 1");

            return new Point(Element.Width * x, Element.Height * y);
        }

        public Point TopLeft
        {
            get { return new Point(0, 0); }
        }

        public Point TopCenter
        {
            get { return new Point(Element.Width / 2, 0); }
        }

        public Point TopRight
        {
            get { return new Point(Element.Width, 0); }
        }

        public Point MiddleLeft
        {
            get { return new Point(0, Element.Height / 2); }
        }

        public Point MiddleCenter
        {
            get { return new Point(Element.Width / 2, Element.Height / 2); }
        }

        // synonym of MiddleCenter
        public Point Center
        {
            get { return MiddleCenter; }
        }

        public Point MiddleRight
        {
            get { return new Point(Element.Width, Element.Height / 2); }
        }

        public Point BottomLeft
        {
            get { return new Point(0, Element.Height); }
        }

        public Point BottomCenter
        {
            get { return new Point(Element.Width / 2, Element.Height); }
        }

        public Point BottomRight
        {
            get { return new Point(Element.Width, Element.Height); }
        }

        // default registration point around which object is rotated
        public Point RotatePoint
        {
            get { return (Point)Element.GetValue(RotateRegistrationPointProperty); }
            set { Element.SetValue(RotateRegistrationPointProperty, value); }
        }

        // default registration point used to locate, move, and scale object
        public Point PositionPoint
        {
            get { return (Point)Element.GetValue(PositionRegistrationPointProperty); }
            set { Element.SetValue(PositionRegistrationPointProperty, value); }
        }
    }
}