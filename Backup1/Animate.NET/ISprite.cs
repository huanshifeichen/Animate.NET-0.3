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
    public interface ISprite
    {
        UIElement Visual { get; set; }
        Stage Stage { get; }

        Point TransformOrigin { get; set; }

        Point Position { get; set; }
        Point PositionOrigin { get; set; }
        double Left { get; set; }
        double Right { get; set; }
        double Top { get; set; }
        double Bottom { get; set; }

        Size Size { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        double RotateAngle { get; set; }
        double RotateAngleNormal { get; set; }

        Vector2D Velocity { get; set; }

        void Update(TimeSpan ElapsedTime);
    }
}