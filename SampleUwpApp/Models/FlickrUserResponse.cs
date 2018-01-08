using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUwpApp.Models
{
    /// <summary>
    /// This class acts as a placeholder for the user details response from Flickr REST API.
    /// </summary>
    public class FlickrUserResponse
    {
        public Person person;
        public string stat;
    }

    public class Person
    {
        public string id;
        public string nsid;
        public bool ispro;
        public bool can_buy_pro;
        public string iconserver;
        public int iconfarm;
        public string path_alias;
        public Username username;
        public Realname realname;
    }
    
    public class Username
    {
        public string _content;
    }

    public class Realname
    {
        public string _content;
    }
}
