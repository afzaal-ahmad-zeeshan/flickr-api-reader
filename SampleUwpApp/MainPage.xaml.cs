using SampleUwpApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleUwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame MainFrame { get; set; }
        public static Tuple<Type, object> PageInformation { get; set; }
        private static string lastSearchKeyword = ""; 

        public MainPage()
        {
            this.InitializeComponent();
            MainFrame = mainFrame;

            Navigate(typeof(SearchPage));
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var appStateInformation = (Application.Current as App).AppStateInformation;
            if (appStateInformation.AppSuspended)
            {
                // App was suspended
                if(appStateInformation.Page != typeof(SearchPage))
                {
                    // Load that page
                    Navigate(appStateInformation.Page, appStateInformation.Parameter);
                } else
                {
                    Navigate(typeof(SearchPage));
                }
            } 
            base.OnNavigatedTo(e);
        }

        // Function to handle the navigation in the MainPage.
        public static void GoBack()
        {
            if(MainFrame.CanGoBack)
            {
                if(MainFrame.CurrentSourcePageType == typeof(ResultsPage))
                {
                    // Load the home page.
                    Navigate(typeof(SearchPage));

                    // Clear the history
                    MainFrame.BackStack.Clear();
                    MainFrame.ForwardStack.Clear();
                } else
                {
                    // Just go back.
                    MainFrame.GoBack();

                    if(MainFrame.CurrentSourcePageType == typeof(ResultsPage))
                    {
                        // If now the results page is being shown, save the history.
                        updatePageInformation(new Tuple<Type, object>(typeof(ResultsPage), lastSearchKeyword));
                    }
                }
            } else
            {
                // Close the app because maybe it was a request from hardware back button on a mobile device.
                App.Current.Exit();
            }

            changeButtonVisibility();
        }

        // Show/Hide the back button.
        private static void changeButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                                MainFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        public static void Navigate(Type page, object parameter = null)
        {
            // Save the search keyword.
            if(page == typeof(ResultsPage))
            {
                lastSearchKeyword = parameter as string; 
            }

            MainFrame.Navigate(page, parameter);
            changeButtonVisibility();

            // Save the page information
            updatePageInformation(new Tuple<Type, object>(page, parameter));
        }

        private static void updatePageInformation(Tuple<Type, object> value)
        {
            PageInformation = value;
        }
    }
}
