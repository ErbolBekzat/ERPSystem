﻿#pragma checksum "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6A1AF9C4D476ED9CC0277A4B343CC9AF9B7EB14B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ERPSystem.Views.TaskView;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace ERPSystem.Views.TaskView {
    
    
    /// <summary>
    /// GanttRectangle
    /// </summary>
    public partial class GanttRectangle : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RectGrid;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rectangle;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel leftButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TogglePopupLeftButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup LeftPopUp;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel rightButton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TogglePopupRightButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup RightPopUp;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ERPSystem;component/views/taskview/ganttrectangle.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RectGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.rectangle = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 12 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.rectangle.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.rectangle.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.leftButton = ((System.Windows.Controls.StackPanel)(target));
            
            #line 16 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.leftButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.leftButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TogglePopupLeftButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.TogglePopupLeftButton.Click += new System.Windows.RoutedEventHandler(this.TogglePopupLeftButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LeftPopUp = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 6:
            this.rightButton = ((System.Windows.Controls.StackPanel)(target));
            
            #line 32 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.rightButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.rightButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TogglePopupRightButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\..\Views\TaskView\GanttRectangle.xaml"
            this.TogglePopupRightButton.Click += new System.Windows.RoutedEventHandler(this.TogglePopupRightButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RightPopUp = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

