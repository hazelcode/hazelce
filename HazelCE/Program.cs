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
        public static void Main(string[] args)
        {
            // Create version.json file
            GeneralInfo info = new GeneralInfo();
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

            // Check for updates
            var UpdateCheckerTask = UpdateSystem.CheckForUpdate();
            var UpdateCkeckerResult = UpdateCheckerTask.Result;
            startupScreen startup = new startupScreen();
            publicDeclarations decs = new publicDeclarations();
            Commands.mainCmds cmds = new Commands.mainCmds();

            // Create directories
            if (!Directory.Exists("plugins"))
            {
                Directory.CreateDirectory("plugins");
            }
            if (!Directory.Exists("HazelCE"))
            {
                Directory.CreateDirectory(path: "HazelCE");
            }
            
            // Show startup texts
            startup.startup();

            // Check exit command
            while(decs.exit == false)
            {
                LezaHLib.Functions functions = new LezaHLib.Functions();
                cmds.cmds(functions.read());
                if(cmds.input == "exit"){   Console.Clear();  break;  }
            }
        }
    }
}
