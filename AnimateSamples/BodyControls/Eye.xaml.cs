using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Animator;

namespace AnimateSamples.BodyControls
{
    public partial class Eye : UserControl
    {
        public Eye()
        {
            InitializeComponent();
        }

        public void Blink()
        {
            // TODO: figure out from the VisualState what control it belongs to
            Closed.Animate(this, 0.2.seconds())
                .WhenComplete(a => HalfOpen.Animate(this, 0.2.seconds()).Begin())
                .Begin();
        }
    }
}