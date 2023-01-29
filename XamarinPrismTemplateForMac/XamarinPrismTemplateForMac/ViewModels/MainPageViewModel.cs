using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.Services;
using XamarinPrismTemplateForMac.Views;

namespace XamarinPrismTemplateForMac.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigatedAware
    {
        private INavigationService _navigationService;
        private DelegateCommand _listRssNewsCommand;
        private DelegateCommand _listLocalNewsCommand;

        public DelegateCommand ListLocalNewsCommand => _listLocalNewsCommand ?? (_listLocalNewsCommand = new DelegateCommand(ListLocalNews));

        private async void ListLocalNews() =>
            await Application.Current.MainPage.Navigation.PushAsync(new StreamPage(true));

        public DelegateCommand ListRssNewsCommand => _listRssNewsCommand ?? (_listRssNewsCommand = new DelegateCommand(ListRss));

        private async void ListRss()=>
            await Application.Current.MainPage.Navigation.PushAsync(new StreamPage(false));
        

        public MainPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "CNN en Español...";
            this._navigationService = navigationService;
            //this._navigationService.NavigateAsync(typeof())
        }
    }
}
