using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HazelCE.Commands
{
    class mainCmds
    {
        startupScreen startup = new startupScreen();
        publicDeclarations decs = new publicDeclarations();

        public void cmds()
        {
            LezaHLib.Functions functions = new LezaHLib.Functions();
            string input = functions.read();
            if (input.StartsWith("echo"))
            {
                Console.WriteLine(input.Replace("echo ", null));
            }
            else if (input.StartsWith("cls"))
            {
                Console.Clear();
                startup.startup();
            }
            else if (input.StartsWith("exit"))
            {
                Console.Clear();
                decs.exit = true;
            }
            else if (input.StartsWith("nvim"))
            {
                Console.WriteLine("Coming soon...");
            }
            else if (input.StartsWith("newfile"))
            {
                string fileName = input.Replace("newfile ", null);
                if (fileName == "newfile")
                {
                    Console.WriteLine("Error: Please specify a name");
                }
                else
                {
                    StreamWriter file = File.CreateText($@"{Environment.CurrentDirectory}\{fileName}");
                    file.Close();
                }
            }
            else if (input.StartsWith("cd"))
            {
                if (input == "cd .." || input == "cd..")
                {
                    try
                    {
                        decs.movedDirectory = true;
                        string previousFolder = Path.GetDirectoryName(Environment.CurrentDirectory);
                        Environment.CurrentDirectory = previousFolder;
                        Console.WriteLine(Environment.CurrentDirectory);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                else
                {
                    try
                    {
                        decs.movedDirectory = true;
                        string nextFolder = Path.Combine(Environment.CurrentDirectory, input.Replace("cd ", null));
                        Environment.CurrentDirectory = nextFolder;
                        Console.WriteLine(Environment.CurrentDirectory);
                    }
                    catch
                    {
                        Console.WriteLine("Folder not found");
                    }

                }
            }
        }
    }
}
