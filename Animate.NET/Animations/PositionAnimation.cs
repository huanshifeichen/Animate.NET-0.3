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
    public class PositionAnimation : Animation
    {
        public double Left;
        public double Top;

        public PositionAnimation(FrameworkElement Element, TimeSpan AnimationLength, double Left, double Top)
            : base(Element, AnimationLength)
        {
            this.Left = Left;
            this.Top = Top;
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
            if (storyboard == null)
                throw new ArgumentNullException("storyboard");

            var LeftAnimation = storyboard.Children[0] as DoubleAnimationUsingKeyFrames;
            var TopAnimation = storyboard.Children[1] as DoubleAnimationUsingKeyFrames;

            LeftAnimation.KeyFrames[0].Value = Left;
            LeftAnimation.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(AnimationLength);
            TopAnimation.KeyFrames[0].Value = Top;
            TopAnimation.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(AnimationLength);
        }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            var storyboard = new Storyboard();

            var LeftAnimation = new DoubleAnimationUsingKeyFrames();
            var TopAnimation = new DoubleAnimationUsingKeyFrames();

            Storyboard.SetTargetProperty(LeftAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(TopAnimation, new PropertyPath("(Canvas.Top)"));

            storyboard.Children.Add(LeftAnimation);
            storyboard.Children.Add(TopAnimation);

            LeftAnimation.KeyFrames.Add(new SplineDoubleKeyFrame()
            {
                KeySpline = new KeySpline()
                {
                    ControlPoint1 = new Point(0.528, 0),
                    ControlPoint2 = new Point(0.142, 0.847)
                }
            });

            TopAnimation.KeyFrames.Add(new SplineDoubleKeyFrame()
            {
                KeySpline = new KeySpline()
                {
                    ControlPoint1 = new Point(0.528, 0),
                    ControlPoint2 = new Point(0.142, 0.847)
                }
            });

            return storyboard;
        }
    }
}