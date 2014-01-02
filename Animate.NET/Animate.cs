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
using Animator;

public static class Animate
{
    public static GroupAnimation Group(params Animation[] Animations)
    {
        return new GroupAnimation(TimeSpan.Zero, Animations);
    }

    public static GroupAnimation Wait(TimeSpan WaitTime, params Animation[] Animations)
    {
        return new GroupAnimation(WaitTime, Animations);
    }

    public static OpacityAnimation Fade(this FrameworkElement Element, double Opacity, TimeSpan AnimationLength)
    {
        return new OpacityAnimation(Element, Opacity, AnimationLength);
    }

    public static OpacityAnimation FadeIn(this FrameworkElement Element, TimeSpan AnimationLength)
    {
        return Element.Fade(1.0, AnimationLength);
    }

    public static OpacityAnimation FadeOut(this FrameworkElement Element, TimeSpan AnimationLength)
    {
        return Element.Fade(0.0, AnimationLength);
    }

    public static GroupAnimation CrossFade(this FrameworkElement FadeOutElement, FrameworkElement FadeInElement, TimeSpan AnimationLength)
    {
        return new GroupAnimation(TimeSpan.Zero,
            FadeOut(FadeOutElement, AnimationLength),
            FadeIn(FadeInElement, AnimationLength)
        );
    }

    public static RotationAnimation RotateTo(this FrameworkElement Element, Point Center, double Degrees, TimeSpan AnimationLength)
    {
        return new RotationAnimation(Element, AnimationLength, Center, Degrees, RotationType.To);
    }

    public static RotationAnimation RotateBy(this FrameworkElement Element, Point Center, double Degrees, TimeSpan AnimationLength)
    {
        return new RotationAnimation(Element, AnimationLength, Center, Degrees, RotationType.By);
    }

    public static Animator.SizeAnimation ResizeTo(this FrameworkElement Element, double Width, double Height, TimeSpan AnimationLength)
    {
        return new Animator.SizeAnimation(Element, AnimationLength, Width, Height);
    }

    public static Animator.SizeAnimation ResizeBy(this FrameworkElement Element, double WidthFactor, double HeightFactor,
        TimeSpan AnimationLength)
    {
        return new Animator.SizeAnimation(Element, AnimationLength, Element.Width * WidthFactor,
            Element.Height * HeightFactor);
    }

    public static PositionAnimation MoveTo(this FrameworkElement Element, double Left, double Top, TimeSpan AnimationLength)
    {
        return new PositionAnimation(Element, AnimationLength, Left, Top);
    }

    public static PositionAnimation MoveBy(this FrameworkElement Element, double X, double Y, TimeSpan AnimationLength)
    {
        return new PositionAnimation(Element, AnimationLength, Element.GetLeft() + X, Element.GetTop() + Y);
    }
}