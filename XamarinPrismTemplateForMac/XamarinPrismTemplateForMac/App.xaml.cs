using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.ViewModels;

namespace XamarinPrismTemplateForMac
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        

        protected override async void OnInitialized()
        {
            InitializeComponent();
             await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }


    }
}
