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
	public partial class HappyFaceControlBoard : UserControl
	{
		private Canvas MainStage;
		private HappyFace HappyFace;

		public HappyFaceControlBoard()
		{
			// Required to initialize variables
			InitializeComponent();
		}

		public void SetStage(Canvas Stage)
		{
			var MoveAmount = 50;
			var RotateAmount = 30;
			var SlowAnimateTime = 0.5.seconds();
			var FastAnimateTime = 0.2.seconds();

			MainStage = Stage;

			HappyFace = new HappyFace()
			{
				Height = 185,
				Width = 153,
				Visibility = Visibility.Collapsed
			};
			HappyFace.SetPosition(279, 146);
			MainStage.Children.Add(HappyFace);

			btnMoveUp.Click += (s1, e1) => HappyFace.MoveBy(0, -MoveAmount, SlowAnimateTime).Begin();
			btnMoveDown.Click += (s2, e2) => HappyFace.MoveBy(0, MoveAmount, SlowAnimateTime).Begin();
			btnMoveLeft.Click += (s3, e3) => HappyFace.MoveBy(-MoveAmount, 0, SlowAnimateTime).Begin();
			btnMoveRight.Click += (s4, e4) => HappyFace.MoveBy(MoveAmount, 0, SlowAnimateTime).Begin();
			btnMoveUpLeft.Click += (s1, e1) => HappyFace.MoveBy(-MoveAmount, -MoveAmount, SlowAnimateTime).Begin();
			btnMoveUpRight.Click += (s1, e1) => HappyFace.MoveBy(MoveAmount, -MoveAmount, SlowAnimateTime).Begin();
			btnMoveDownLeft.Click += (s2, e2) => HappyFace.MoveBy(-MoveAmount, MoveAmount, SlowAnimateTime).Begin();
			btnMoveDownRight.Click += (s2, e2) => HappyFace.MoveBy(MoveAmount, MoveAmount, SlowAnimateTime).Begin();

			btnRotateLeft.Click += (s5, e5) => HappyFace.RotateBy(HappyFace.GetCenter(), -RotateAmount, FastAnimateTime).Begin();
			btnRotateRight.Click += (s6, e6) => HappyFace.RotateBy(HappyFace.GetCenter(), RotateAmount, FastAnimateTime).Begin();
			
			btnGrowFace.Click += (s7, e7) => HappyFace.ResizeBy(1.5, 1.5, SlowAnimateTime).Begin();
			btnShrinkFace.Click += (s7, e7) => HappyFace.ResizeBy(0.66, 0.66, SlowAnimateTime).Begin();
		}

		private void btnToggleFaceVisibility_Click(object sender, RoutedEventArgs e)
		{
			HappyFace.Visibility = btnToggleFaceVisibility.Content.Equals("Show Face") ? Visibility.Visible : Visibility.Collapsed;
			btnToggleFaceVisibility.Content = HappyFace.Visibility == Visibility.Collapsed ? "Show Face" : "Hide Face";
		}

		private void btnDance_Click(object sender, RoutedEventArgs e)
		{
			var EyebrowDance = Animate.Group(
				HappyFace.BounceEyebrow(HappyFace.LeftEyebrow),
				Animate.Wait(0.4.seconds(),
					HappyFace.BounceEyebrow(HappyFace.RightEyebrow)
				)
			);

			var WiggleDance = Animate.Group(
				HappyFace.MoveBy(-100, -100, 0.8.seconds()),
				HappyFace.ResizeBy(1.5, 1.5, 0.8.seconds()),
				Animate.Wait(0.8.seconds(),
					HappyFace.RotateBy(HappyFace.GetCenter(), 180, 1.seconds()),
					HappyFace.MoveTo(400, 200, 1.3.seconds()),
					Animate.Wait(1.3.seconds(),
						HappyFace.RotateBy(HappyFace.GetCenter(), -90, 1.seconds()),
						HappyFace.MoveBy(-200, 50, 1.seconds()),
						HappyFace.ResizeBy(0.7, 0.7, 2.seconds())
					)
				)
			);

			Animate.Group(
				EyebrowDance,
				Animate.Wait(0.5.seconds(),
					WiggleDance,
					Animate.Wait(2.5.seconds(),
						EyebrowDance,
						HappyFace.MoveTo(279, 146, 0.5.seconds()),
						HappyFace.RotateTo(HappyFace.GetCenter(), 0, 0.5.seconds()),
						HappyFace.ResizeTo(153, 185, 0.5.seconds())
					)
				)
			).Begin();
		}
	}
}