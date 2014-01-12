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
    public static class VisualStateExtensions
    {
        public static Animation Animate(this VisualState State, Control Control, TimeSpan AnimationLength)
        {
            return new VisualStateAnimation(Control, State, AnimationLength);
        }
    }
}