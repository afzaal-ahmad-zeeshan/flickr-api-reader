using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// This is a ViewModel class reponsible for holding the View information in the response page.
    /// </summary>
    class FlickrResponseViewModel
    {
        public string Id { get; set; }
        public Uri ImageUri { get; set; }
        public Uri SmallImageUri { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string PhotoId { get; set; }
        public string PhotoSecret { get; set; }
    }
}
