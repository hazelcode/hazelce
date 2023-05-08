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
        bool echo = true;
        public void cmds(string entry)
        {
            LezaHLib.Functions functions = new LezaHLib.Functions();
            string input = entry;
            if (input.StartsWith("echo"))
            {
                Console.WriteLine(input.Replace("echo ", null));
            }
            else if (input.StartsWith("cls"))
            {
                Console.Clear();
                if(echo == true) {startup.startup();}
            }
            else if (input.StartsWith("exit"))
            {
                Console.Clear();
                decs.exit = true;
            }
            else if (input.StartsWith("nvim"))
            {
                if(echo == true) {
                    Console.WriteLine("Coming soon...");
                }
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
                    decs.movedDirectory = true;
                        string previousFolder = Path.GetDirectoryName(Environment.CurrentDirectory);
                        Environment.CurrentDirectory = previousFolder;
                        if(echo == true) {Console.WriteLine(Environment.CurrentDirectory);}
                }
                else
                {
                    try
                    {
                        decs.movedDirectory = true;
                        string nextFolder = Path.Combine(Environment.CurrentDirectory, input.Replace("cd ", null));
                        Environment.CurrentDirectory = nextFolder;
                        if(echo == true) {Console.WriteLine(Environment.CurrentDirectory);}
                    }
                    catch
                    {
                        Console.WriteLine("Folder not found");
                    }

                }
            }
            else if (input.StartsWith("mkdir"))
            {
                string newDirectory = Path.Combine(Environment.CurrentDirectory, input.Replace("mkdir ", null));
                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                } else
                {
                    if(echo == true) {Console.WriteLine($"{input.Replace("mkdir ", null)} already exists!");}
                }
            }
            else if (input.StartsWith("deldir"))
            {
                string delDirectory = Path.Combine(Environment.CurrentDirectory, input.Replace("deldir ", null));
                if (Directory.Exists(delDirectory))
                {
                    Directory.Delete(delDirectory);
                }
                else
                {
                    Console.WriteLine($"{input.Replace("deldir ", null)} doesn't exist!");
                }
            }
            else if (input.StartsWith("delfile"))
            {
                string fileName = Path.Combine(Environment.CurrentDirectory, input.Replace("delfile ", null));
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                else
                {
                    Console.WriteLine($"{input.Replace("delfile ", null)} doesn't exist!");
                }
            }
            else if (input.StartsWith("copy")){
                string sourceFile = Path.Combine(Environment.CurrentDirectory, input.Replace("copy ", null));
                string user = Environment.UserName;
                sourceFile = sourceFile.Replace("%USER%", user);
                sourceFile = sourceFile.Replace("%appdata%", $@"C:\Users\{Environment.UserName}\AppData\Roaming");
                sourceFile = sourceFile.Replace("appdata", $@"C:\Users\{Environment.UserName}\AppData");
                sourceFile = sourceFile.Replace("programFiles", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                sourceFile = sourceFile.Replace("programFiles86", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                Console.WriteLine("Copy the file to (include file name):");
                string destFile = Console.ReadLine();
                destFile = destFile.Replace("%USER%", user);
                destFile = destFile.Replace("%appdata%", $@"C:\Users\{Environment.UserName}\AppData\Roaming");
                destFile = destFile.Replace("appdata", $@"C:\Users\{Environment.UserName}\AppData");
                destFile = destFile.Replace("appdata", $@"C:\Users\{Environment.UserName}\AppData");
                destFile = destFile.Replace("programFiles", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                destFile = destFile.Replace("programFiles86", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                try {
                    if(Directory.Exists(destFile)){
                        File.Copy(sourceFile, destFile);
                    } else {
                        string CreateDir = Path.GetDirectoryName(destFile);
                        Directory.CreateDirectory(CreateDir);
                        File.Copy(sourceFile, destFile);
                    }
                } catch {
                    string lastTry = Path.Combine(Environment.CurrentDirectory, destFile);
                    try {
                        if(Directory.Exists(lastTry)){
                            File.Copy(sourceFile, lastTry);
                        } else {
                            string CreateDir2 = Path.GetDirectoryName(lastTry);
                            Directory.CreateDirectory(CreateDir2);
                            File.Copy(sourceFile, lastTry);
                        }
                    } catch {
                        Console.WriteLine($@"
                    
                        {sourceFile}

                        to:

                        {lastTry}

                        ");
                        Console.WriteLine("Can't copy that file. Please check that the source file and the path destination exists.");
                    }
                }
            }
            else if(input.StartsWith(":") || entry.StartsWith(":")){
                TerminalFileReader reader = new TerminalFileReader();
                if(entry.StartsWith(":")){
                    input = entry;
                }
                string file = input.Replace(":", null);
                try {
                    reader.ReadFile(file + ".hazelce");
                } catch {
                    Console.WriteLine($@"Script {file}.hazelce not found");
                }
            }
            else if(input.StartsWith("@echo")){
                if(input == "@echo off"){
                    echo = false;
                } else if(input == "@echo on"){
                    echo = true;
                }
            }
        }
    }
}
