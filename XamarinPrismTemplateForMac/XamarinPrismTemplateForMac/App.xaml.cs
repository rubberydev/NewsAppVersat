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

        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        

        protected override async void OnInitialized()
        {
            InitializeComponent();

             SingletonGlobalVariables.GetInstance();

             await NavigationService.NavigateAsync("NavigationPage/MainPage");

            Xamarin.Forms.Application.Current.MainPage = new Xamarin.Forms.NavigationPage(new MainPage())
            {
                BarTextColor = Color.FromRgb(255, 255, 255),
                BarBackgroundColor = Color.FromRgb(255, 87, 51)
            };
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
