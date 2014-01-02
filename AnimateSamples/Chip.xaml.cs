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

namespace AnimateSamples
{
	public partial class Chip : UserControl
	{
		public Chip()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        public Color InnerColor
        {
            get { return (SmallColoredCircle.Fill as SolidColorBrush).Color; }
            set { SmallColoredCircle.Fill = new SolidColorBrush(value); }
        }

        public Color OuterColor
        {
            get { return (LargeColoredCircle.Fill as SolidColorBrush).Color; }
            set { LargeColoredCircle.Fill = new SolidColorBrush(value); }
        }
	}
}