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
    /// This API reader reads user details from Flickr API, provided the NSID (userId) of the user
    /// </summary>
    class UserDetailsApiReader
    {
        public async static Task<FlickrUserResponse> GetUserDetailsAsync(string nsId) 
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.flickr.com/services/rest/");

            // Read the profile
            var url = $"?method=flickr.people.getInfo&api_key={ApiSecrets.ApiKey}&user_id={nsId}&format=json&nojsoncallback=1";
            return JsonConvert.DeserializeObject<FlickrUserResponse>(await client.GetStringAsync(url));
        }
    }
}
