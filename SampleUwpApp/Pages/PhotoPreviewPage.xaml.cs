using SampleUwpApp.Models;
using SampleUwpApp.Services.Api;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleUwpApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoPreviewPage : Page
    {
        public PhotoPreviewPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null)
            {
                var parameter = e.Parameter as FlickrResponseViewModel;
                if(parameter != null)
                {
                    imageControl.Source = new BitmapImage(parameter.ImageUri);
                    description.Text = parameter.Description;

                    // Load the user details now.
                    var userDetails = await UserDetailsApiReader.GetUserDetailsAsync(parameter.UserId);
                    var photoDetails = await PhotoDetailsApiReader.GetPhotoDetailsAsync(parameter.PhotoId, parameter.PhotoSecret);

                    // Render the information
                    var model = prepareViewModel(userDetails, photoDetails);
                    title.Text = model.Title;
                    description.Text = model.Description;
                    authoredInfo.Text = $"{model.Author} published this on {model.PublishedOn}.";

                    loadingImageDescription.Text = "Image description";
                } else
                {
                    await new MessageDialog("Photo information is incomplete.", "Cannot load the photo").ShowAsync();
                }
            } else
            {
                await new MessageDialog("Photo information was not passed with the request.", "Cannot load the photo").ShowAsync();
                MainPage.GoBack();
            }
            base.OnNavigatedTo(e);
        }

        private PhotoDetailsViewModel prepareViewModel(FlickrUserResponse userResponse, FlickrPhotoDetailsResponse photoResponse)
        {
            var model = new PhotoDetailsViewModel();
            model.Description = photoResponse.photo.description._content;
            model.Title = photoResponse.photo.title._content;
            model.PublishedOn = DateTime.Parse(photoResponse.photo.dates.taken).ToString("MMMM dd, yyyy 'at' hh:mm tt");
            model.Author = userResponse.person.username._content;

            return model;
        }
    }
}
