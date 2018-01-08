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
    /// This API reader reads the search results from the Flickr API, it implements the IDisposable interface to properly close HTTP connections once user has finished the searching process. 
    /// </summary>
    class PhotosSearchApiReader : IDisposable
    {
        private int perPage = 25;
        private int page = 1;
        private HttpClient httpClient;

        public PhotosSearchApiReader(int itemsPerPage)
        {
            this.perPage = itemsPerPage;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.flickr.com/services/rest/");
        }

        // Just to clear a few things out. 
        public void Dispose()
        {
            if(httpClient != null)
            {
                httpClient.CancelPendingRequests();
                httpClient.Dispose();
                httpClient = null;
            }
        }

        // LOad the data.
        public async Task<FlickrPhotoSearchResponse> GetResponseAsync(string keywords)
        {
            // Fetch the response.
            var url = $"?method=flickr.photos.search&api_key={ApiSecrets.ApiKey}&tags={keywords}&page={page}&per_page={perPage}&format=json&nojsoncallback=1";
            var result = await httpClient.GetStringAsync(url);

            // Deserialize it
            var flickrResponse = JsonConvert.DeserializeObject<FlickrPhotoSearchResponse>(result);

            // Move to next page for next time.
            page++;
            return flickrResponse;
        }
    }
}
