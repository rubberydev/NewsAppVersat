using System;
using Prism.Navigation.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.DbModels;
using XamarinPrismTemplateForMac.Helpers;
using XamarinPrismTemplateForMac.Services;
using XamarinPrismTemplateForMac.Views;

namespace XamarinPrismTemplateForMac.Models
{
    public class CustomCell : ViewCell
    {
        private SingletonGlobalVariables singletonGlobalVariables;

        private bool isVisibleLegendToSaveNews;

        public CustomCell()
        {
            this.singletonGlobalVariables = SingletonGlobalVariables.GetInstance();
            this.isVisibleLegendToSaveNews = !singletonGlobalVariables.NavigatedFromSavedNewsOption;

            var youTubeFeed = new Label();
            youTubeFeed.LineBreakMode = LineBreakMode.WordWrap;
            youTubeFeed.SetBinding(Label.TextProperty, "Title");
            youTubeFeed.FontSize = 16;
            var youTubeFeed2 = new Label();
            youTubeFeed2.LineBreakMode = LineBreakMode.WordWrap;
            youTubeFeed2.SetBinding(Label.TextProperty, "Date");
            youTubeFeed2.FontSize = 10;

            var verticalStack = new StackLayout();
            verticalStack.Children.Add(youTubeFeed);
            verticalStack.Children.Add(youTubeFeed2);

            var label_action = new Label()
            {
                Text = "toca dos veces para verla despues...",
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                IsVisible = this.isVisibleLegendToSaveNews
            };

            verticalStack.Children.Add(label_action);
            var tapGestureRecognizer_to_view_detail = new TapGestureRecognizer();
            tapGestureRecognizer_to_view_detail.NumberOfTapsRequired = 1;
            tapGestureRecognizer_to_view_detail.Tapped += async (s, e) =>
            {
                if (this.singletonGlobalVariables.NavigatedFromSavedNewsOption)
                {
                    var current = Connectivity.NetworkAccess;
                    string optionSelected = string.Empty;
                    if (current == NetworkAccess.None)
                    {
                        optionSelected = await Xamarin.Forms.Application.Current.MainPage.DisplayActionSheet("Error de red", "Recargar!", "Cancelar!", "Señor usuario si desea continuar y ver la noticia habillite la conexion a internet y luego seleccione la acción Recargar!");
                    }

                    if (optionSelected == "Recargar!")
                    {
                        Xamarin.Forms.Application.Current.MainPage = new Xamarin.Forms.NavigationPage(new StreamPage(false))
                        {
                            BarTextColor = Color.FromRgb(255, 255, 255),
                            BarBackgroundColor = Color.FromRgb(255, 87, 51)
                        };
                    }
                    else if (optionSelected == "Cancelar!" || string.IsNullOrEmpty(optionSelected))
                        return;

                    var rss_feed_object = s as StackLayout;
                    RSSFeedObject bindingContext = (RSSFeedObject)rss_feed_object.BindingContext;
                    await Application.Current.MainPage.Navigation.PushAsync(new StreamDetailPage(bindingContext));
                }
            };
                var image_channel = new Image()
                {
                    Source = "news_icon.jpeg",
                    HeightRequest = 50,
                    MinimumWidthRequest = 70
                };


                var tapGestureRecognizer_to_save_new = new TapGestureRecognizer();
                //avoid double tap when user list local news
                if (!singletonGlobalVariables.NavigatedFromSavedNewsOption)
                {
                    tapGestureRecognizer_to_save_new.NumberOfTapsRequired = 2;



                    tapGestureRecognizer_to_save_new.Tapped += async (s, e) => {
                        // save news

                        var response = await Application.Current.MainPage.DisplayAlert("!!", "Esta seguro que quiere guardar esta noticia para verla despues?", "Si", "No");

                        if (!response) return;

                        DbService dbService = new DbService();
                        var news_to_save = s as StackLayout;
                        RSSFeedObject rssFeedObject = (RSSFeedObject)news_to_save.BindingContext;
                        RSSNewsDbModel rss_feed_to_save = new RSSNewsDbModel();
                        rss_feed_to_save.Title = rssFeedObject.Title;
                        rss_feed_to_save.Date = rssFeedObject.Date;
                        rss_feed_to_save.Link = rssFeedObject.Link;
                        try
                        {
                            await dbService.InsertNewsAsync(rss_feed_to_save);
                            await Application.Current.MainPage.DisplayAlert("!!", "noticia guardada exitosamente", "Aceptar");
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("ERROR", "La noticia no pudo ser guardada ", "Aceptar");

                        }

                    };
                    verticalStack.GestureRecognizers.Add(tapGestureRecognizer_to_save_new);
                }
                verticalStack.GestureRecognizers.Add(tapGestureRecognizer_to_view_detail);


                var horizontalStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { image_channel, verticalStack },
                    MinimumHeightRequest = 100,
                    Padding = 10,
                    Margin = 5
                };

                View = horizontalStack;
            }
         }
    }


