using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// AppStateInformation class is used to store the app state such as page, and the parameter passed by the App to reload the state once app restarts. 
    /// </summary>
    public class AppStateInformation
    {
        public bool AppSuspended { get; set; }
        public Type Page { get; set; }
        public object Parameter { get; set; }
    }
}
