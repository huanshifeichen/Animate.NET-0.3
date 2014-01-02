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
    public class VisualStateAnimation : Animation
    {
        public Control Control;
        public VisualState State;
        public Storyboard Storyboard;

        public VisualStateAnimation(Control Control, VisualState State, TimeSpan AnimationLength)
            : base(null, AnimationLength)
        {
            this.Control = Control;
            this.State = State;

            Storyboard = new Storyboard();
            Storyboard.Duration = new Duration(AnimationLength);
        }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            return Storyboard;
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
        }

        protected override void OnAnimationStarted()
        {
            VisualStateManager.GoToState(Control, State.Name, true);
            base.OnAnimationStarted();
        }
    }
}