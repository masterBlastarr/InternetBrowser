using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using Windows.System;
using Player.View.RightPanel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Player.View.Card
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Card : Page
    {
        string ActualURL;
        string ShowUrl;
        public Card()
        {
            this.InitializeComponent();
            CheckButtons();
        }

        private void GoTo(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ActualURL = new UrlOperations().GetUrl(UrlField.Text);
                try
                {
                    Site.Navigate(new Uri(ActualURL));
                }
                catch (System.UriFormatException error)
                {
                    ActualURL = new UrlOperations().GetSearchUrl(UrlField.Text);
                    Site.Navigate(new Uri(ActualURL));
                }
            }
        }

        private void ChangeText(WebView sender, WebViewContentLoadingEventArgs args)
        {
         
            ShowUrl = new UrlOperations().GetUrlToShow(sender.Source.ToString());
            UrlField.Text = ShowUrl;
            CheckButtons();
            
            

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            
            Site.GoBack();
            CheckButtons();
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            Site.GoForward();
            CheckButtons();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            Site.Refresh();
        }
        void CheckButtons()
        {
            if (Site.CanGoBack)
            {
                BackB.IsEnabled = true;
            }
            else if (!Site.CanGoBack)
            {
                BackB.IsEnabled = false;
            }
             if (!Site.CanGoForward)
            {
                ForwardB.IsEnabled = false;
            }
            else if (Site.CanGoForward)
            {
                ForwardB.IsEnabled = true;
            }
        }

        private void Context(object sender, RightTappedRoutedEventArgs e)
        {
            Site.ContextFlyout.ShowAt(sender as FrameworkElement);
            
        }

       



        private void Download(WebView sender, WebViewUnviewableContentIdentifiedEventArgs args)
        {

                new DownloadScript().Download(Site.Source);
            
        }

        private void DownloadWindow(object sender, RoutedEventArgs e)
        {
            RightPanel.Navigate(typeof(DownloadList));
            RightPanel.Visibility = Visibility.Visible;
        }
    }
}
