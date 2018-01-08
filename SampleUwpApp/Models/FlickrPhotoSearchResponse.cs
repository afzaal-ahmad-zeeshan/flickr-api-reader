using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// This class acts as a placeholder to read information regarding the search response from Flickr REST API.
    /// </summary>
    class FlickrPhotoSearchResponse
    {
        public PhotosList photos;
    }

    public class PhotosList
    {
        public int page;
        public int pages;
        public int perpage;
        public int total;
        public List<PartialPhotoDetails> photo;
    }

    public class PartialPhotoDetails
    {
        public string id;
        public string owner;
        public string secret;
        public string server;
        public int farm;
        public string title;
        public bool ispublic;
        public bool isfriend;
        public bool isfamily;
    }
}
