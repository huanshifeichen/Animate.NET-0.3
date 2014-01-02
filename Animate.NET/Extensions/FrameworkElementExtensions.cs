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

public static class FrameworkElementExtensions
{
    public static void SetSize(this FrameworkElement Element, double Width, double Height)
    {
        Element.Width = Width;
        Element.Height = Height;
    }

    public static void SetSize(this FrameworkElement Element, Size NewSize)
    {
        Element.Width = NewSize.Width;
        Element.Height = NewSize.Height;
    }

    public static void SetPosition(this FrameworkElement Element, double X, double Y)
    {
        Element.SetLeft(X);
        Element.SetTop(Y);
    }

    public static void Center(this FrameworkElement Element)
    {
        var parent = Element.Parent as FrameworkElement;
        if (parent == null)
            throw new ArgumentException("Element.Parent must be a FrameworkElement");

        Element.SetLeft(parent.ActualWidth / 2.0 - Element.Width / 2.0);
        Element.SetTop(parent.ActualHeight / 2.0 - Element.Height / 2.0);
    }

    public static Point GetCenter(this FrameworkElement Element)
    {
        return new Point(Element.Width / 2.0, Element.Height / 2.0);
    }

    public static void Remove(this FrameworkElement Element)
    {
        if (Element == null)
            return;

        var panel = Element.Parent as Panel;
        panel.Children.Remove(Element);
    }

    public static double GetLeft(this FrameworkElement Element)
    {
        return (double)Element.GetValue(Canvas.LeftProperty);
    }

    public static void SetLeft(this FrameworkElement Element, double Left)
    {
        Element.SetValue(Canvas.LeftProperty, Left);
    }

    public static double GetTop(this FrameworkElement Element)
    {
        return (double)Element.GetValue(Canvas.TopProperty);
    }

    public static void SetTop(this FrameworkElement Element, double Top)
    {
        Element.SetValue(Canvas.TopProperty, Top);
    }

    public static TransformType GetTransform<TransformType>(this FrameworkElement Element)
        where TransformType : Transform
    {
        var transform = Element.GetValue(FrameworkElement.RenderTransformProperty) as TransformType;
        if (transform != null)
            return transform;

        var group = Element.GetValue(FrameworkElement.RenderTransformProperty) as TransformGroup;
        if (group != null)
        {
            foreach (var item in group.Children)
            {
                transform = item as TransformType;
                if (transform != null)
                    return transform;
            }
        }

        return null;
    }

    public static RotateTransform GetRotateTransform(this FrameworkElement Element)
    {
        var transform = Element.GetValue(FrameworkElement.RenderTransformProperty) as RotateTransform;
        if (transform != null)
            return transform;

        var group = Element.GetValue(FrameworkElement.RenderTransformProperty) as TransformGroup;
        if (group != null)
        {
            foreach (var item in group.Children)
            {
                transform = item as RotateTransform;
                if (transform != null)
                    return transform;
            }
        }

        return null;
    }

    public static RegistrationPoints Points(this FrameworkElement Element)
    {
        return new RegistrationPoints(Element);
    }

    public static Path CreateBoundary(this Canvas Stage, Path Boundary)
    {
        return null;
    }
}