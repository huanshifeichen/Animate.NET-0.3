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
    public class SizeAnimation : Animation
    {
        public double Width;
        public double Height;

        public SizeAnimation(FrameworkElement Element, TimeSpan AnimationLength, double Width, double Height)
            : base(Element, AnimationLength)
        {
            this.Width = Width;
            this.Height = Height;
        }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            var storyboard = new Storyboard();

            var WidthAnimation = new DoubleAnimationUsingKeyFrames();
            var HeightAnimation = new DoubleAnimationUsingKeyFrames();

            Storyboard.SetTargetProperty(WidthAnimation, new PropertyPath("(FrameworkElement.Width)"));
            Storyboard.SetTargetProperty(HeightAnimation, new PropertyPath("(FrameworkElement.Height)"));

            storyboard.Children.Add(WidthAnimation);
            storyboard.Children.Add(HeightAnimation);

            WidthAnimation.KeyFrames.Add(new SplineDoubleKeyFrame()
            {
                KeySpline = new KeySpline()
                {
                    ControlPoint1 = new Point(0.528, 0),
                    ControlPoint2 = new Point(0.142, 0.847)
                }
            });

            HeightAnimation.KeyFrames.Add(new SplineDoubleKeyFrame()
            {
                KeySpline = new KeySpline()
                {
                    ControlPoint1 = new Point(0.528, 0),
                    ControlPoint2 = new Point(0.142, 0.847)
                }
            });

            return storyboard;
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
            if (storyboard == null)
                throw new ArgumentNullException("storyboard");

            var WidthAnimation = storyboard.Children[0] as DoubleAnimationUsingKeyFrames;
            var HeightAnimation = storyboard.Children[1] as DoubleAnimationUsingKeyFrames;

            WidthAnimation.KeyFrames[0].Value = Width;
            WidthAnimation.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(AnimationLength);
            HeightAnimation.KeyFrames[0].Value = Height;
            HeightAnimation.KeyFrames[0].KeyTime = KeyTime.FromTimeSpan(AnimationLength);
        }



        protected override void OnStoryboardCompleted()
        {
            
            base.OnStoryboardCompleted();
        }
    }
}