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
	public partial class FriendlyGuy : UserControl
	{
		public FriendlyGuy()
		{
			InitializeComponent();

			BobHead = Animate.Group(
				Head.MoveBy(0, 5, 0.1.seconds()),
				Animate.Wait(0.1.seconds(), Head.MoveBy(0, -5, 0.2.seconds()),
				Animate.Wait(0.2.seconds(), Head.MoveBy(0, 0, 0.1.seconds())))
				);

			//BobHead.WhenComplete(a => BobHead.Begin());
		}

		public Animation BobHead;
	}
}