// Samples and graphics (AnimateSamples.SL) are Copyright 2010 Dan Vanderboom
// The Animate.NET library for Silverlight and Windows Phone 7 are offered for your use under the MIT license.
// Find more at http://animatedotnet.codeplex.net

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
using System.Threading;
using Animator;
using AnimateSamples.BodyControls;

namespace AnimateSamples
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			Loaded += MainPage_Loaded;
		}

		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			ControlBoard.SetStage(MainStage);
			eyeball.MouseLeftButtonUp += eyeball_MouseLeftButtonUp;
			guy.MouseLeftButtonUp += guy_MouseLeftButtonUp;

			FriendlyGuy.MouseLeftButtonUp += FriendlyGuy_MouseLeftButtonUp;
		}

		private bool action = true;
		void FriendlyGuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (action)
				FriendlyGuy.BobHead.Begin();
			else
				FriendlyGuy.Head.LeftEye.Blink();

			action = !action;
		}

		void Blink(Eye eye)
		{
			// TODO: figure out from the VisualState what control it belongs to
			eye.Closed.Animate(eye, 0.2.seconds())
				.WhenComplete(a => eye.HalfOpen.Animate(eye, 0.2.seconds()).Begin())
				.Begin();
		}

		private bool left = true;
		void guy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (left)
				Blink(guy.LeftEye);
			else
				Blink(guy.RightEye);

			left = !left;
		}

		void eyeball_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Blink(eyeball);
		}

		private Chip CreateChip(ChipColor color, double opacity)
		{
			return new Chip()
			{
				InnerColor = color == ChipColor.Red 
					? Color.FromArgb(0xFF, 0xAE, 0x03, 0x03) 
					: Color.FromArgb(0xFF, 0x00, 0x00, 0x00),
				OuterColor = color == ChipColor.Red 
					? Color.FromArgb(0x7F, 0xFF, 0x00, 0x00) 
					: Color.FromArgb(0x7F, 0x00, 0x00, 0x00),
				Opacity = opacity
			};
		}

		private void btnFadeIn_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 0.0);
			MainStage.Children.Add(RedChip);
			RedChip.Center();

			// animate
			RedChip.FadeIn(0.5.seconds())
				.WhenComplete(a =>
					{
						Thread.Sleep(2000);
						MainStage.Children.Remove(RedChip);
					})
				.Begin();
		}

		private void btnFadeOut_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 1.0);
			MainStage.Children.Add(RedChip);
			RedChip.Center();

			// animate

			var fadeout = RedChip.FadeOut(0.5.seconds());

			Animate.Wait(1.0.seconds(), fadeout)
				.WhenComplete(a => MainStage.Children.Remove(RedChip))
				.Begin();
		}

		private void btnCrossFade_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 1.0);
			MainStage.Children.Add(RedChip);
			RedChip.Center();

			var BlackChip = CreateChip(ChipColor.Black, 0.0);
			MainStage.Children.Add(BlackChip);
			BlackChip.Center();

			// animate
			Animate.Wait(1.seconds(),
				Animate.CrossFade(RedChip, BlackChip, 1.5.seconds())
				)
				.WhenComplete(a =>
					{
						MainStage.Children.Remove(RedChip);
						MainStage.Children.Remove(BlackChip);
					})
				.Begin();
		}

		private void btnRotateBy_Click(object sender, RoutedEventArgs e)
		{
			var BlackChip = CreateChip(ChipColor.Black, 1.0);
			MainStage.Children.Add(BlackChip);
			BlackChip.Center();

			// animate
			Animate.Wait(0.5.seconds(), 
				BlackChip.RotateBy(BlackChip.GetCenter(), -90, 0.5.seconds()))
				.WhenComplete(a => 
					{
						Thread.Sleep(2000);
						MainStage.Children.Remove(BlackChip);
					})
				.Begin();
		}

		private void btnRotateTo_Click(object sender, RoutedEventArgs e)
		{
			var BlackChip = CreateChip(ChipColor.Black, 1.0);
			MainStage.Children.Add(BlackChip);
			BlackChip.Center();

			// animate
			Animate.Wait(0.5.seconds(), 
				BlackChip.RotateTo(BlackChip.GetCenter(), 180, 0.5.seconds()))
				.WhenComplete(a => 
					{
						Thread.Sleep(2000);
						MainStage.Children.Remove(BlackChip);
					})
				.Begin();
		}

		// demonstrate how to create reusable complex animations
		private Animation FadingRotation(FrameworkElement Element, TimeSpan AnimationLength)
		{
			var ThirdTime = TimeSpan.FromSeconds(AnimationLength.Seconds / 3.0);

			return Animate.Group(
				Element.FadeIn(ThirdTime),
				Element.RotateBy(Element.GetCenter(), 360, AnimationLength),
				Animate.Wait(ThirdTime + ThirdTime,
					Element.FadeOut(ThirdTime)
					)
				);
		}

		private void btnGroup_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 0.0);
			MainStage.Children.Add(RedChip);
			RedChip.Center();
			RedChip.SetLeft(RedChip.GetLeft() - 100);

			var BlackChip = CreateChip(ChipColor.Black, 0.0);
			MainStage.Children.Add(BlackChip);
			BlackChip.Center();
			BlackChip.SetLeft(BlackChip.GetLeft() + 100);

			// animate

			Animate.Group(
				FadingRotation(RedChip, 2.5.seconds()),
				Animate.Wait(1.seconds(),
					FadingRotation(BlackChip, 1.seconds())
					)
				)
				.WhenComplete(a => MainStage.Children.Remove(RedChip))
				.Begin();
		}

		private void btnMoveTo_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 1.0);
			MainStage.Children.Add(RedChip);
			RedChip.SetLeft(600);
			RedChip.SetTop(400);

			RedChip.MoveTo(50, 50, 1.2.seconds())
				.WhenComplete(a => 
					{
						Thread.Sleep(1000);
						MainStage.Children.Remove(RedChip);
					})
				.Begin();

			RedChip.MoveTo(50, 50, 1.2.seconds()).Begin();
		}

		private void btnMoveBy_Click(object sender, RoutedEventArgs e)
		{
			var RedChip = CreateChip(ChipColor.Red, 1.0);
			MainStage.Children.Add(RedChip);
			RedChip.Center();

			RedChip.MoveBy(200, 0, 1.seconds())
				.WhenComplete(a =>
				{
					Thread.Sleep(1000);
					MainStage.Children.Remove(RedChip);
				})
				.Begin();
		}

		private void btnResizeBy_Click(object sender, RoutedEventArgs e)
		{
			var rect = new Rectangle()
			{
				Height = 100,
				Width = 300,
				Fill = new SolidColorBrush(Colors.Blue)
			};
			MainStage.Children.Add(rect);
			rect.SetPosition(250, 50);

			rect.ResizeBy(1.0, 4.0, 1.seconds())
				.WhenComplete(a =>
				{
					Thread.Sleep(2000);
					MainStage.Children.Remove(rect);
				})
				.Begin();
		}

		private void btnResizeTo_Click(object sender, RoutedEventArgs e)
		{
			var rect = new Rectangle()
			{
				Height = 250,
				Width = 350,
				Fill = new SolidColorBrush(Colors.Blue)
			};
			MainStage.Children.Add(rect);
			rect.SetPosition(50, 50);

			rect.ResizeTo(150, 150, 1.5.seconds())
				.WhenComplete(a =>
				{
					Thread.Sleep(2000);
					MainStage.Children.Remove(rect);
				})
				.Begin();
		}
	}
}