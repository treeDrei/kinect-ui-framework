﻿#pragma checksum "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CD6051149519D30E5D8F4EF8F0100897"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace KinectControll.Demo.View.DebugView {
    
    
    /// <summary>
    /// DebugWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class DebugWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image videoImage;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image depthImage;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label debugOutput;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MainCanvas;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse headEllipse;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse rightEllipse;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse leftEllipse;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectangle1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse controllElipse;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/KinectControll;component/demo/view/debugview/debugwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\..\..\..\Demo\View\DebugView\DebugWindow.xaml"
            ((KinectControll.Demo.View.DebugView.DebugWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.windowLoadedHandler);
            
            #line default
            #line hidden
            return;
            case 2:
            this.videoImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.depthImage = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.debugOutput = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.MainCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.headEllipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 7:
            this.rightEllipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 8:
            this.leftEllipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 9:
            this.rectangle1 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 10:
            this.controllElipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

