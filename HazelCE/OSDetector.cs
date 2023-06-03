using System;
using System.Runtime.InteropServices;

namespace HazelCE
{
    public class OSDetector
    {
        public static string isOS() {
            // Detect OS by calling this string
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                return "Windows";
            } else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                return "MacOS";
            } else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)){
                return "Linux";
            } else {
                return "Unknown";
            }
        }
        public static int OSByInt() {
            // Detect OS by calling this string
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                return 1;
            } else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                return 2;
            } else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)){
                return 3;
            } else {
                return 0;
            }
        }
    }
}