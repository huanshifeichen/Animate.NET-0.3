﻿#pragma checksum "C:\Source\Advance\Animate.NET\AnimateSamples\BodyControls\FriendlyGuy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D14B3C6403C0179E93A15A4136FFAA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AnimateSamples.BodyControls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace AnimateSamples.BodyControls {
    
    
    public partial class FriendlyGuy : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal AnimateSamples.BodyControls.ManBody manBody1;
        
        internal AnimateSamples.BodyControls.FriendlyGuyHead Head;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/AnimateSamples.SL;component/BodyControls/FriendlyGuy.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.manBody1 = ((AnimateSamples.BodyControls.ManBody)(this.FindName("manBody1")));
            this.Head = ((AnimateSamples.BodyControls.FriendlyGuyHead)(this.FindName("Head")));
        }
    }
}

