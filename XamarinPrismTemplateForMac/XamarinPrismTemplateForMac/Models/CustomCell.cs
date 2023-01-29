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

        DbService dbService;

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


            var tapGestureRecognizer_to_view_detail_or_save = new TapGestureRecognizer();

            tapGestureRecognizer_to_view_detail_or_save.Tapped += async (s, e) =>
            {
                var rss_feed_object = s as StackLayout;
                RSSFeedObject bindingContext = (RSSFeedObject)rss_feed_object.BindingContext;
                string userAction = string.Empty;
                if (!singletonGlobalVariables.NavigatedFromSavedNewsOption)
                {
                   userAction = await Xamarin.Forms.Application.Current.MainPage.DisplayActionSheet("Error de red", "Guardar!", "Ver noticia!", "Señor usuario que accion desea ejecutar, seleccione(Ver noticia!para ver ahora mismo)(Guardar! para verla posteriormente) !");

                }

                if (!string.IsNullOrEmpty(userAction) && userAction == "Ver noticia!")
                {

                    if (this.singletonGlobalVariables.NavigatedFromSavedNewsOption)
                    {
                        var current = Connectivity.NetworkAccess;
                        string optionSelected = string.Empty;
                        if (current != NetworkAccess.Internet)
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

                    
                    }

                await Application.Current.MainPage.Navigation.PushAsync(new StreamDetailPage(bindingContext));

                }
                else if(!string.IsNullOrEmpty(userAction) && userAction == "Guardar!")
                {
                    RSSNewsDbModel rss_feed_to_save = new RSSNewsDbModel();
                    rss_feed_to_save.Title = bindingContext.Title;
                    rss_feed_to_save.Date = bindingContext.Date;
                    rss_feed_to_save.Link = bindingContext.Link;
                    try
                    {
                        this.dbService = new DbService();
                        await this.dbService.InsertNewsAsync(rss_feed_to_save);
                        await Application.Current.MainPage.DisplayAlert("!!", "noticia guardada exitosamente", "Aceptar");
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("ERROR", $"La noticia no pudo ser guardada: {ex.Message} ", "Aceptar");

                    }
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new StreamDetailPage(bindingContext));

                }


            };

                var image_channel = new Image()
                {
                    Source = "news_icon.jpeg",
                    HeightRequest = 50,
                    MinimumWidthRequest = 70
                };

                verticalStack.GestureRecognizers.Add(tapGestureRecognizer_to_view_detail_or_save);


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


