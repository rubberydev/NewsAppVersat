using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.Services;
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
            //Services
            containerRegistry.Register<IDbService, DbService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }


    }
}
