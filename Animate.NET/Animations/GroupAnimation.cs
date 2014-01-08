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
using System.Collections.Generic;
using System.Linq;
using Animator.Coordination;

namespace Animator
{
    public class GroupAnimation : Animation
    {
        public TimeSpan WaitTime { get; protected set; }
        public Storyboard WaitTimer { get; protected set; }
        public List<Animation> Animations { get; protected set; }

        public GroupAnimation(Action<Animation> Completed, TimeSpan WaitTime, params Animation[] Animations)
            : base(null, WaitTime)
        {
            this.StoryboardCompleted += Completed;
            this.WaitTime = WaitTime;
            this.Animations = new List<Animation>(Animations);
        }

        public GroupAnimation(TimeSpan WaitTime, params Animation[] Animations)
            : this(null, WaitTime, Animations) { }

        private void StartAnimations()
        {
            foreach (var run in Animations)
            {
                run.Begin();
            }
        }

        protected override void ApplyValues(Storyboard storyboard)
        {
            if (storyboard == null)
                throw new ArgumentNullException("storyboard");
        }

        protected override Storyboard CreateStoryboard(FrameworkElement element)
        {
            WaitTimer = new Storyboard();
            WaitTimer.Duration = new Duration(WaitTime);
            WaitTimer.Completed += (sender, e) => StartAnimations();
            return WaitTimer;
        }

        protected override void OnAnimationCompleted()
        {
            base.OnAnimationCompleted();
        }

        protected override void OnStoryboardCompleted()
        {
            var list = new List<Animation>(Animations);
            var join = new Join<Animation>(list.Count, a => OnAnimationCompleted());

            for (int i = 0; i < list.Count; i++)
			{
                int x = i;
                list[x].AnimationCompleted += a =>
                    {
                        int z = x;
                        join.Call[z](list[z]);
                    };
			}
        }

        #region 自定义公共方法
        public GroupAnimation Add(Animation animation)
        {
            Animations.Add(animation);
            return this;
        }
        #endregion
    }
}