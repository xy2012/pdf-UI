﻿#pragma checksum "C:\Users\v-xiafe\Desktop\foxitSDK\pdf\renderPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "41DA16ED43AA91E692E8BABBFE485988"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pdf
{
    partial class renderPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.LeftPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 2:
                {
                    this.RightPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element3 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 26 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element3).Click += this.Newpdf_Click;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element4 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 29 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element4).Click += this.closepdf_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.word = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.Searchword = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 21 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.Searchword).Click += this.Searchword_Click;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element7 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 22 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element7).Click += this.Previous_Click;
                    #line default
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element8 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 23 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element8).Click += this.Next_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.currtbook = (global::Windows.UI.Xaml.Controls.ColumnDefinition)(target);
                }
                break;
            case 10:
                {
                    this.scroll_pdf = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 11:
                {
                    this.currentNotebook = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 12:
                {
                    this.image = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 63 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.image).PointerPressed += this.GetBeginLocation;
                    #line 63 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.image).PointerReleased += this.GetEndLocation;
                    #line 63 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.image).PointerMoved += this.GetMoveLocation;
                    #line default
                }
                break;
            case 13:
                {
                    this.display = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 14:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element14 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 80 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element14).Click += this.Click_BTN_RotateLeft;
                    #line default
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element15 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 81 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element15).Click += this.Click_BTN_RotateRight;
                    #line default
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element16 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 82 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element16).Click += this.Click_BTN_ActualSize;
                    #line default
                }
                break;
            case 17:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element17 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 83 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element17).Click += this.Click_BTN_FitWidth;
                    #line default
                }
                break;
            case 18:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element18 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 84 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element18).Click += this.Click_BTN_FitHeight;
                    #line default
                }
                break;
            case 19:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element19 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 88 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element19).Click += this.Click_BTN_PrePage;
                    #line default
                }
                break;
            case 20:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element20 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 89 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element20).Click += this.Click_BTN_NextPage;
                    #line default
                }
                break;
            case 21:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element21 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 90 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element21).Click += this.Click_BTN_ZoomIn;
                    #line default
                }
                break;
            case 22:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element22 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 91 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element22).Click += this.Click_BTN_ZoomOut;
                    #line default
                }
                break;
            case 23:
                {
                    this.note = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 92 "..\..\..\renderPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.note).Click += this.Like_Click;
                    #line default
                }
                break;
            case 24:
                {
                    this.feedback = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

