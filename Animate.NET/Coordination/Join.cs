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

namespace Animator.Coordination
{
    public class Join<T>
    {
        private int CallerCount;
        private int CallsRemaining;

        public event Action<T> Completed;
        public List<Action<T>> Call;
        public List<bool> CallsMade;

        public Join(int CallerCount, Action<T> Completed)
        {
            this.CallerCount = CallerCount;
            this.Completed = Completed;

            CallsRemaining = CallerCount;

            Call = new List<Action<T>>(CallerCount);
            CallsMade = new List<bool>(CallerCount);
            for (int i = 0; i < CallerCount; i++)
            {
                int x = i;
                Call.Add(p => CallInternal(p, Call.IndexOf(Call[x])));
                CallsMade.Add(false);
            }
        }

        protected void CallInternal(T Parameter, int Index)
        {
            CallsMade[Index] = true;
            CallsRemaining--;

            if (AllCallsMade && Completed != null)
                Completed(Parameter);
        }

        protected bool AllCallsMade
        {
            get
            {
                foreach (var CallMade in CallsMade)
                {
                    if (!CallMade)
                        return false;
                }
                return true;
            }
        }
    }
}