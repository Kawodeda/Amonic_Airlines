#pragma checksum "..\..\EditRoleWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8398D51B136AF58CAF7B0ED3D610BCE5C6F3ED44C0068A0FAE39252308E2B5DC"
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
    /// EditRoleWindow
    /// </summary>
    public partial class EditRoleWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox email_tb;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name_tb;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox last_name_tb;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox office_cb;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton user_radio_btn;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton admin_radio_btn;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button apply_btn;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\EditRoleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/AMONIC_Desktop;component/editrolewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditRoleWindow.xaml"
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
            this.email_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.name_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.last_name_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.office_cb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.user_radio_btn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.admin_radio_btn = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.apply_btn = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\EditRoleWindow.xaml"
            this.apply_btn.Click += new System.Windows.RoutedEventHandler(this.apply_btn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cancel_btn = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\EditRoleWindow.xaml"
            this.cancel_btn.Click += new System.Windows.RoutedEventHandler(this.cancel_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

