﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_Click_1(object sender, RoutedEventArgs e)
        {
            #region 统一打开窗口

            var btn = e.OriginalSource as Button;
            string content = btn.Content.ToString();
            string name = this.GetType().Namespace + ".Views." + content;
            Window win = Activator.CreateInstance(Type.GetType(name)) as Window;
            win.Show();
            #endregion
        }




    }
}
