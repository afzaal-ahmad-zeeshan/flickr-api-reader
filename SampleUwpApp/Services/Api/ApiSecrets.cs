using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Services.Api
{
    /// <summary>
    /// A class containing the secret and confidential information such as the API keys and other information. In real world scenario, they will be stored in even more private place, such as encrypted files etc.
    /// </summary>
    class ApiSecrets
    {
        public static string ApiKey => "a3cd79ba7cd941e96b9006b2a643539d";
    }
}
