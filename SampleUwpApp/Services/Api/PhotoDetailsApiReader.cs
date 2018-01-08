using Newtonsoft.Json;
using SampleUwpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Services.Api
{
    /// <summary>
    /// This API reader class reads the photo information from the Flickr API.
    /// </summary>
    class PhotoDetailsApiReader
    {
        public static async Task<FlickrPhotoDetailsResponse> GetPhotoDetailsAsync(string photoId, string secret)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.flickr.com/services/rest/");

            var url = $"?method=flickr.photos.getInfo&api_key={ApiSecrets.ApiKey}&photo_id={photoId}&secret={secret}&format=json&nojsoncallback=1";
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<FlickrPhotoDetailsResponse>(response);
        }
    }
}
