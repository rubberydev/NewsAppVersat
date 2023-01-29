using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.Helpers;
using XamarinPrismTemplateForMac.Services;
using XamarinPrismTemplateForMac.ViewModels;

namespace XamarinPrismTemplateForMac
{
    public partial class App : PrismApplication
    {
        SingletonGlobalVariables singletonGlobalVariables;
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        

        protected override async void OnInitialized()
        {
            InitializeComponent();
            singletonGlobalVariables = SingletonGlobalVariables.GetInstance();
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
