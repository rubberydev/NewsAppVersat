using System;
using Prism.Navigation.Xaml;
using Xamarin.Forms;
using XamarinPrismTemplateForMac.DbModels;
using XamarinPrismTemplateForMac.Services;
using XamarinPrismTemplateForMac.Views;

namespace XamarinPrismTemplateForMac.Models
{
    public class CustomCell : ViewCell
    {
        
        public CustomCell()
        {
            
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
            var tapGestureRecognizer_to_view_detail = new TapGestureRecognizer();
            tapGestureRecognizer_to_view_detail.NumberOfTapsRequired = 1;
            tapGestureRecognizer_to_view_detail.Tapped += async(s, e) =>
            {
                var rss_feed_object = s as StackLayout;
                RSSFeedObject bindingContext = (RSSFeedObject)rss_feed_object.BindingContext;
                await Application.Current.MainPage.Navigation.PushAsync(new StreamDetailPage(bindingContext));
            };

            var tapGestureRecognizer_to_save_new = new TapGestureRecognizer();
            tapGestureRecognizer_to_save_new.NumberOfTapsRequired = 2;
            var image_channel = new Image()
            {
                Source = "news_icon.jpeg",
                HeightRequest = 50,
                MinimumWidthRequest = 70
            };

            
            
            tapGestureRecognizer_to_save_new.Tapped += async (s, e) => {
                // save news

                var response = await Application.Current.MainPage.DisplayAlert("!!", "Esta seguro que quiere guardar esta noticia para leerla despues?", "Si","No");

                if (!response)return;

                DbService dbService = new DbService();
                var news_to_save = s as StackLayout;
                RSSFeedObject rssFeedObject = (RSSFeedObject)news_to_save.BindingContext;
                RSSNewsDbModel rss_feed_to_save = new RSSNewsDbModel();
                rss_feed_to_save.Title = rssFeedObject.Title;
                rss_feed_to_save.Date = rssFeedObject.Date;
                rss_feed_to_save.Link = rssFeedObject.Link;
                await dbService.InsertNewsAsync(rss_feed_to_save);
                await Application.Current.MainPage.DisplayAlert("!!", "noticia guardada exitosamente", "Aceptar");
            };

            verticalStack.GestureRecognizers.Add(tapGestureRecognizer_to_view_detail);
            verticalStack.GestureRecognizers.Add(tapGestureRecognizer_to_save_new);
            
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

