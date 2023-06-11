using System;
using System.Diagnostics;
using LezaHLib;
using LezaHLib.OS;
using System.IO;
using System.Text.Json;

namespace HazelCE
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralInfo info = new GeneralInfo();
            startupScreen startup = new startupScreen();
            publicDeclarations decs = new publicDeclarations();
            Commands.mainCmds cmds = new Commands.mainCmds();
            if (!Directory.Exists("plugins"))
            {
                Directory.CreateDirectory("plugins");
            }
            if (!Directory.Exists("HazelCE"))
            {
                Directory.CreateDirectory("HazelCE");
            }
            if(!File.Exists("version.json")){
                var versionJson = new versionJson
                {
                    updateStream = info.ver.updateStream,
                    ver = info.ver.ver,
                    build = info.ver.build
                };
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(versionJson, options);
                File.WriteAllText("version.json", json);
            }
            startup.startup();
            while(decs.exit == false)
            {
                LezaHLib.Functions functions = new LezaHLib.Functions();
                cmds.cmds(functions.read());
                if(cmds.input == "exit"){   Console.Clear();  break;  }
            }
        }
    }
}
