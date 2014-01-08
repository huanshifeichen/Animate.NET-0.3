using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Demo.WPF.Views
{
    /// <summary>
    /// TestRemoveStory.xaml 的交互逻辑
    /// </summary>
    public partial class TestRemoveStory : Window
    {
        public TestRemoveStory()
        {
            InitializeComponent();
            
        }

        private void SizeAnimation_Click(object sender, RoutedEventArgs e)
        {
            //ellipseOne.ResizeTo(200, 200, 2.seconds()).Begin();
            //ellipseOne.ResizeTo(ellipseOne.ActualWidth, 300, 2.seconds()).Begin();
            ellipseOne.ResizeBy(2, 2, 2.seconds()).Begin();
            ellipseOne.ResizeBy(.5, .5, 2.seconds()).Begin();
        }

        private double GetTbInput()
        {
            string content = tb1.Text;
            double dInput = double.Parse(content);
            return dInput;
        }



        private void SizeAniByInput_Click(object sender, RoutedEventArgs e)
        {
            double dInput = GetTbInput();
            ellipseOne.ResizeHeight(dInput, 2.seconds()).Begin();
        }

        private void PositionAni_Click(object sender, RoutedEventArgs e)
        {
            double dInput = GetTbInput();
            ellipseOne.MoveBy(dInput, dInput, 1.5.seconds()).Begin() ;
        }

        private void ToggleOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipseOne.FadeToggle(1.5.seconds()).Begin();
        }

        private void RotateEllipse_Click(object sender, RoutedEventArgs e)
        {
            ellipseOne.RenderTransformOrigin = new Point(.5, .5);

            var rotateAni = ellipseOne.RotateBy(new Point(), 90, 2.seconds());
            var fadeAni =ellipseOne.FadeToggle(1.5.seconds());

            Animate.Group(rotateAni,
                fadeAni,
                Animate.Wait(1.seconds(),
                ellipseOne.FadeIn(.5.seconds()))
                ).Add(ellipseOne.ResizeBy(1.5,1.5,2.seconds()))
                .Begin();


        }

        private void GCCollect_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}
