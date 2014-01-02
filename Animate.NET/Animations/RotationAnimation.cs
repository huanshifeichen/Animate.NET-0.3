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
    public class RotationAnimation : TransformAnimation<RotateTransform>
    {
        public double Degrees { get; set; }
        public Point Center { get; set; }
        public RotationType RotationType { get; set; }

        public RotationAnimation(FrameworkElement Element, TimeSpan AnimationLength, Point Center, double Degrees, 
            RotationType RotationType)
            : base(Element, AnimationLength)
        {
            this.Degrees = Degrees;
            this.Center = Center;
            this.RotationType = RotationType;
        }

        protected override void OnElementPrepared()
        {
            SelectedTransform.CenterX = Center.X;
            SelectedTransform.CenterY = Center.Y;
        }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            var propertyPath = PrepareElement(element).Combine(new PropertyPath("(RotateTransform.Angle)"));
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation();
            Storyboard.SetTargetProperty(animation, propertyPath);
            storyboard.Children.Add(animation);
            return storyboard;
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
            if(storyboard == null)
                throw new ArgumentNullException("storyboard");

            var animation = (DoubleAnimation)storyboard.Children[0];

            if (RotationType == RotationType.By)
                animation.By = Degrees;
            else
                animation.To = Degrees;

            animation.Duration = AnimationLength;
        }
    }
}