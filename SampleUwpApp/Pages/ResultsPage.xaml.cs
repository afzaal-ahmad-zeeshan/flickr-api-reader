using SampleUwpApp.Models;
using SampleUwpApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleUwpApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultsPage : Page
    {
        private string parameter = null;
        private bool pageReloaded;

        public ResultsPage()
        {
            this.InitializeComponent();

            pageReloaded = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(pageReloaded)
            {
                pageReloaded = false;
            }

            // (Re)Load the content of the page.
            if (e.Parameter != null)
            {
                if(parameter != e.Parameter as string)
                {
                    parameter = e.Parameter as string;
                    resultsHeader.Text = $"Showing results for keyword(s) \"{parameter}\".";

                    // Reload
                    var items = new FlickrResponseStore(parameter, 40);
                    gridView.ItemsSource = items;
                }
            }
            else
            {
                resultsHeader.Text = "Results page.";
            }
            base.OnNavigatedTo(e);
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gridView = sender as GridView;

            if(gridView.SelectedIndex != -1)
            {
                var item = gridView.SelectedItem as FlickrResponseViewModel;
                MainPage.Navigate(typeof(PhotoPreviewPage), item);
                gridView.SelectedIndex = -1;
            }
        }
    }
}
