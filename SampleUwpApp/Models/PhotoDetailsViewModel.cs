using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// This class is the ViewModel for the photo details page.
    /// </summary>
    class PhotoDetailsViewModel
    {
        public string Author { get; set; }
        public string PublishedOn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
