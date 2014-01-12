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
    public class OpacityAnimation : Animation
    {
        public static TimeSpan DefaultDuration = TimeSpan.FromMilliseconds(750);

        public OpacityAnimation(FrameworkElement Element, double Opacity, TimeSpan AnimationLength)
            : base(Element, AnimationLength)
        {
            this.Opactity = Opacity;
        }

        public double Opactity { get; set; }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            var storyboard = new Storyboard();
            var opacityAnimation = new DoubleAnimation();
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(FrameworkElement.Opacity)"));
            storyboard.Children.Add(opacityAnimation);
            return storyboard;
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
            if (storyboard == null)
                throw new ArgumentNullException("storyboard");

            var opacityAnimation = storyboard.Children[0] as DoubleAnimation;

            opacityAnimation.To = Opactity;
            opacityAnimation.Duration = AnimationLength;
        }
    }
}