using System.IO;
namespace HazelCE
{
    public class TerminalFileReader
    {
        Commands.mainCmds cmds = new Commands.mainCmds();
        public void ReadFile(string fileName) {
            using (StreamReader file = new StreamReader(fileName))
            {
                string line;
                while((line = file.ReadLine()) != null) {
                cmds.cmds(line);
                }
            }
        }
    }
}