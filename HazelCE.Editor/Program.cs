using HazelCE.Editor;
using Terminal.Gui;

Application.Init();

try
{
    Application.Run(new CE());
}
finally
{
    Application.Shutdown();
}