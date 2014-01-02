// Samples and graphics (AnimateSamples.SL) are Copyright 2010 Dan Vanderboom
// The Animate.NET library for Silverlight and Windows Phone 7 are offered for your use under the MIT license.
// Find more at http://animatedotnet.codeplex.net

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Animator;

namespace AnimateSamples
{
	public partial class HappyFace : UserControl
	{
		public HappyFace()
		{
			// Required to initialize variables
			InitializeComponent();

            Loaded += new RoutedEventHandler(HappyFace_Loaded);
		}

        private void HappyFace_Loaded(object sender, RoutedEventArgs e)
        {
            RightEyebrowCanvas.SizeChanged += (s1, e1) => RightEyebrow.SetSize(e1.NewSize);
            LeftEyebrowCanvas.SizeChanged += (s2, e2) => LeftEyebrow.SetSize(e2.NewSize);
        }

        public Animation BounceEyebrow(FrameworkElement Eyebrow)
        {
            var MoveAmount = ActualHeight * 0.05;

            return Animate.Group(
                Eyebrow.MoveBy(0, -MoveAmount, 0.2.seconds()),
                Animate.Wait(0.5.seconds(),
                    Eyebrow.MoveTo(0, 0, 0.2.seconds())
                )
            );
        }
	}
}