﻿#pragma checksum "..\..\AdminMainMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D91C0E99461D8E51C4A939685242BF06407A8898E421B6260BED5EC868C2B093"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AMONIC_Desktop;
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


namespace AMONIC_Desktop {
    
    
    /// <summary>
    /// AdminMainMenu
    /// </summary>
    public partial class AdminMainMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem add_user_btn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem exit_btn;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox office_cb;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid users_grid;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button change_role_btn;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\AdminMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ban_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/AMONIC_Desktop;component/adminmainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdminMainMenu.xaml"
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
            
            #line 11 "..\..\AdminMainMenu.xaml"
            ((AMONIC_Desktop.AdminMainMenu)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.add_user_btn = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\AdminMainMenu.xaml"
            this.add_user_btn.Click += new System.Windows.RoutedEventHandler(this.add_user_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.exit_btn = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\AdminMainMenu.xaml"
            this.exit_btn.Click += new System.Windows.RoutedEventHandler(this.exit_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.office_cb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\AdminMainMenu.xaml"
            this.office_cb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.office_cb_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.users_grid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.change_role_btn = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\AdminMainMenu.xaml"
            this.change_role_btn.Click += new System.Windows.RoutedEventHandler(this.change_role_btn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ban_btn = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\AdminMainMenu.xaml"
            this.ban_btn.Click += new System.Windows.RoutedEventHandler(this.ban_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

