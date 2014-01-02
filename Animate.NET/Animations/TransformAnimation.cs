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
    public abstract class TransformAnimation<T> : Animation
        where T : Transform, new()
    {
        public T SelectedTransform { get; private set; }

        protected TransformAnimation(FrameworkElement Element, TimeSpan AnimationLength)
            : base(Element, AnimationLength) { }

        protected virtual void OnElementPrepared() { }

        protected virtual PropertyPath PrepareElement(FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            if (element.RenderTransform == null)
                element.RenderTransform = new TransformGroup();

            SelectedTransform = new T();

            if (element.RenderTransform is TransformGroup)
            {
                var group = (TransformGroup)element.RenderTransform;
                group.Children.Add(SelectedTransform);

                OnElementPrepared();

                return new PropertyPath(String.Format("(UIElement.RenderTransform).(TransformGroup.Children)[{0}]", 
                    group.Children.Count - 1));
            }
            else
            {
                var RenderTransform = element.RenderTransform;
                var group = new TransformGroup();

                group.Children.Add(RenderTransform);
                group.Children.Add(SelectedTransform);

                element.RenderTransform = group;

                OnElementPrepared();

                return new PropertyPath(String.Format("(UIElement.RenderTransform).(TransformGroup.Children)[{0}]", 
                    group.Children.Count - 1));
            }

            throw new InvalidOperationException("Could not get prepare element for " + typeof(T).Name);
        }
    }
}