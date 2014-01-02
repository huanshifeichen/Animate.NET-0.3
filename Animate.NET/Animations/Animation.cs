using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Animator
{
    public abstract class Animation
    {
        public bool IsStoryboardComplete;
        public FrameworkElement Element;
        protected TimeSpan AnimationLength;

        internal event Action<Animation> StoryboardCompleted;
        public event Action<Animation> AnimationCompleted;
        public event Action<Animation> AnimationStarted;

        protected Animation(FrameworkElement Element, TimeSpan AnimationLength)
        {
            this.Element = Element;
            this.AnimationLength = AnimationLength;
        }

        public static Animation Create(params Animation[] Animations)
        {
            return new GroupAnimation(TimeSpan.Zero, Animations);
        }

        public static Animation Wait(TimeSpan WaitTime, params Animation[] Animations)
        {
            return new GroupAnimation(WaitTime, Animations);
        }

        protected virtual void OnAnimationStarted()
        {
            if (AnimationStarted != null)
                AnimationStarted(this);
        }

        protected virtual void OnAnimationCompleted()
        {
            if (AnimationCompleted != null)
                AnimationCompleted(this);
        }

        protected virtual void OnStoryboardCompleted()
        {
            if (StoryboardCompleted != null)
                StoryboardCompleted(this);

            OnAnimationCompleted();
        }

        protected virtual string ResourceKey
        {
            get { return GetType().FullName; }
        }

        protected abstract Storyboard CreateStoryboard(FrameworkElement element);

        protected abstract void ApplyValues(Storyboard storyboard);

        public virtual Animation Begin()
        {
            var storyboard = CreateStoryboard(Element);

            if (Element != null)
            {
                //Element.Resources.Add(ResourceKey, storyboard);

                foreach (var timeline in storyboard.Children)
                {
                    Storyboard.SetTarget(timeline, Element);
                }
            }

            ApplyValues(storyboard);

            storyboard.Completed += (sender, e) =>
                {
                    IsStoryboardComplete = true;
                    OnStoryboardCompleted();
                };

            //让Storyboard可以停止
#if WPF
            storyboard.Begin(Element, true);
            

#else 
            storyboard.Begin();
#endif
            OnAnimationStarted();

            return this;
        }

        public Animation WhenComplete(Action<Animation> Completed)
        {
            if (IsStoryboardComplete)
                OnAnimationCompleted();
            else
                AnimationCompleted += Completed;
    
            return this;
        }

        public override string ToString()
        {
            return AnimationLength.ToString();
        }
    }
}