using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// This class acts as a placeholder for reading photo information response from the Flickr REST API in JSON format.
    /// </summary>
    class FlickrPhotoDetailsResponse
    {
        public PhotoDetails photo;
        public string stat;
    }

    public class PhotoDetails
    {
        public Owner owner;
        public Title title;
        public Description description; 
        public Visibility visibility;
        public DateInfo dates;
    }

    public class Owner
    {
        public string nsid;
        public string username;
    }

    public class Title
    {
        public string _content;
    }

    public class Description
    {
        public string _content;
    }

    public class Visibility
    {
        public bool ispublic;
        public bool isfriend;
        public bool isfamily;
    }

    public class DateInfo
    {
        public string taken; 
    }
}
