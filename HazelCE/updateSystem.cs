using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HazelCE
{
    public class UpdateSystem
    {
        public string Init() {
            using (var client = new WebClient())
            {
                //client.DownloadFile("http://example.com/file/song/a.mpeg", "a.mpeg");
            }
            return "";
        }    
    }
}