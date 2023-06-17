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
        public string input;
        public void cmds(string entry)
        {
            LezaHLib.Functions functions = new LezaHLib.Functions();
            input = entry;
            if (input.StartsWith("echo "))
            {
                Console.WriteLine(input.Replace("echo ", null));
            }
            else if (input.StartsWith("cls") || input.StartsWith("clear"))
            {
                Console.Clear();
                if(echo == true) {startup.startup();}
            }
            else if (entry == "exit")
            {
                Console.WriteLine("The command exit is not intended for HazelCE Scripts.");
            }
            else if (input.StartsWith("newfile"))
            {
                string fileName = input.Replace("newfile ", null);
                if (fileName == "" || fileName.Trim() == "")
                {
                    Console.WriteLine("Error: Please specify a relative or absolute directory");
                }
                else
                {
                    StreamWriter file = File.CreateText($@"{Environment.CurrentDirectory}/{fileName}");
                    file.Close();
                }
            }
            else if (input.StartsWith("cd "))
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
                try{
                    string newDirectory = Path.Combine(Environment.CurrentDirectory, input.Replace("mkdir ", null));
                    if (!Directory.Exists(newDirectory))
                    {
                        Directory.CreateDirectory(newDirectory);
                    } else
                    {
                        if(echo == true) {Console.WriteLine($"{input.Replace("mkdir ", null)} already exists!");}
                    }
                } catch(IOException e) {
                    Console.WriteLine(e.Message);
                }
                
            }
            else if (input.StartsWith("deldir"))
            {
                string delDirectory = Path.Combine(Environment.CurrentDirectory, input.Replace("deldir ", null));
                try{
                    if (Directory.Exists(delDirectory))
                    {
                        Directory.Delete(delDirectory);
                    }
                } catch(IOException e){
                    Console.WriteLine(e.Message);
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
                    Console.WriteLine($"File '{input.Replace("delfile ", null)}' doesn't exist!");
                }
            }
            else if (input.StartsWith("copy")){
                string filter = input.Replace("copy", null);
                if (filter == "" || filter.Trim() == "")
                {
                    Console.WriteLine("Error: Please specify a relative or absolute directory");
                } else {
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
                            File.Copy(sourceFile, destFile);
                            Console.WriteLine($"{sourceFile} copied to {destFile}");
                    } catch(IOException e) {
                        Console.WriteLine($@"

                            {sourceFile}

                            to:

                            {destFile}

                            ");
                            Console.WriteLine(e.Message);
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
            else if(input.StartsWith("llama")){
                Console.WriteLine("llama package manager coming soon");
            }
            else if(input.StartsWith("ls") || input.StartsWith("dir")){
                string[] directories = Directory.GetDirectories(@"./");
                string[] files = Directory.GetFiles(@"./");
                int totalCount = directories.Length + files.Length;
                Console.WriteLine("Files & Directories");
                foreach (string directory in directories)
                {
                    Console.WriteLine("FOLDER || " + directory.Replace(@"./", null));
                }
                foreach(string file in files){
                    Console.WriteLine("FILE   || " + file.Replace(@"./", null));
                }
                Console.WriteLine($"Total: {totalCount} files and directories ({files.Length} files, {directories.Length} directories).");
            }
        }
    }
}