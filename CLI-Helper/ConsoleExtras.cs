namespace CLIHelper
{
    public static class ConsoleExtras
    {
        public static string ReadLinesUntilEOF(bool showInfo = false)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && !Console.IsInputRedirected)
            {
                if (showInfo) Console.WriteLine("Press CTRL+Z to input \"end of file\" character");
                string input = "";
                while (!Console.KeyAvailable) ;
                var tmpChar = Console.ReadKey();
                while (tmpChar.KeyChar != 0x1A)
                {
                    input += tmpChar.KeyChar;
                    if (tmpChar.KeyChar == '\r')
                    {
                        input += "\n";
                        Console.WriteLine();
                    }
                    while (!Console.KeyAvailable) ;
                    tmpChar = Console.ReadKey();
                }
                Console.CursorLeft--;
                Console.WriteLine("^Z");
                return input;
            }
            else
            {
                if (showInfo) Console.WriteLine("Press CTRL+D to finish");
                var result = Console.In.ReadToEnd();
                if (Console.IsInputRedirected)
                {
                    return result.TrimEnd('\r', '\n');
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
