﻿#pragma checksum "..\..\Prices.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0010E79FA08B44BC94631C490452FEEBCA5ADE2E35F7A3CFF427AF14E0BAFD21"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Proiect;
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


namespace Proiect {
    
    
    /// <summary>
    /// Prices
    /// </summary>
    public partial class Prices : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_coafor;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_manicure;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_pedicure;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_beauty;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_massage;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Prices.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewServicii;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Proiect;component/prices.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Prices.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.bt_coafor = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\Prices.xaml"
            this.bt_coafor.Click += new System.Windows.RoutedEventHandler(this.bt_coafor_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bt_manicure = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\Prices.xaml"
            this.bt_manicure.Click += new System.Windows.RoutedEventHandler(this.bt_manicure_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.bt_pedicure = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Prices.xaml"
            this.bt_pedicure.Click += new System.Windows.RoutedEventHandler(this.bt_pedicure_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bt_beauty = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Prices.xaml"
            this.bt_beauty.Click += new System.Windows.RoutedEventHandler(this.bt_beauty_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bt_massage = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Prices.xaml"
            this.bt_massage.Click += new System.Windows.RoutedEventHandler(this.bt_massage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.listViewServicii = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            
            #line 51 "..\..\Prices.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

