﻿#pragma checksum "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E822FF72F0E2F036BB08C97BB2A36CC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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


namespace AdminEvents.Admin.Teleproceso {
    
    
    /// <summary>
    /// frmTipoCambio
    /// </summary>
    public partial class frmTipoCambio : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTipoCambio;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSuprimir;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuardar;
        
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
            System.Uri resourceLocater = new System.Uri("/AdminEvents;component/admin/teleproceso/frmtipocambio_.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
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
            
            #line 4 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
            ((AdminEvents.Admin.Teleproceso.frmTipoCambio)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 4 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
            ((AdminEvents.Admin.Teleproceso.frmTipoCambio)(target)).Loaded += new System.Windows.RoutedEventHandler(this.frmTipoCambio_Load);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblTipoCambio = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnSuprimir = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
            this.btnSuprimir.Click += new System.Windows.RoutedEventHandler(this.btnSuprimir_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGuardar = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Admin\Teleproceso\frmTipoCambio_.xaml"
            this.btnGuardar.Click += new System.Windows.RoutedEventHandler(this.btnGuardar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

