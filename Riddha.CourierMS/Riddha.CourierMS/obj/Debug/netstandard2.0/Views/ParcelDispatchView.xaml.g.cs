//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Riddha.CourierMS.Views.ParcelDispatchView.xaml", "Views/ParcelDispatchView.xaml", typeof(global::Riddha.CourierMS.Views.ParcelDispatchView))]

namespace Riddha.CourierMS.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\ParcelDispatchView.xaml")]
    public partial class ParcelDispatchView : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label SelectBranchLable;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Riddha.CourierMS.DependencyServices.TextBoarderlessEntry TrackingNoEntryText;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView DispatchedParcelList;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ParcelDispatchView));
            SelectBranchLable = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "SelectBranchLable");
            TrackingNoEntryText = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Riddha.CourierMS.DependencyServices.TextBoarderlessEntry>(this, "TrackingNoEntryText");
            DispatchedParcelList = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "DispatchedParcelList");
        }
    }
}
