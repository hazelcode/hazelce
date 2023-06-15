using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelCE
{
    public class GeneralInfo
    {
        public (string updateStream, string ver, int build) ver = ("Alpha", "3", 8);
    }
    public class versionJson{
        public string updateStream { get; set; }
        public string ver { get; set; }
        public int build { get; set; }
    }
}
