using System;
using System.Diagnostics;
using LezaHLib;
using LezaHLib.OS;
using System.IO;

namespace HazelCE
{
    class Program
    {
        static void Main(string[] args)
        {
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
