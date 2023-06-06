using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelCE
{
    class startupScreen
    {
        publicDeclarations decs = new publicDeclarations();

        public void startup()
        {
            GeneralInfo info = new GeneralInfo();
            OSDetector os = new OSDetector();
            Console.Title = $"HazelCE {info.ver.updateStream} {info.ver.ver} (build {info.ver.build})";
            string EnvUsr = Environment.UserName;
            OperatingSystem osEnv = Environment.OSVersion;
            Version ver = osEnv.Version;
            Console.WriteLine($"HazelCE {info.ver.updateStream} {info.ver.ver} | " + EnvUsr);
            Console.WriteLine(os.OSName() + " " + ver.Major + " " + LezaHLib.OS.Functions.GetArchitecture());
            try
            {
                if (decs.movedDirectory == false)
                {
                    Environment.CurrentDirectory = "HazelCE";
                }
            } catch
            {
                if (Environment.CurrentDirectory != "HazelCE")
                {
                    decs.movedDirectory = true;
                }
            }
            
            Console.WriteLine(Environment.CurrentDirectory);
        }
    }
}
