using System.Diagnostics;
using System.Text;

namespace CLIHelper
{
    static class Generator
    {
        /// <summary>
        /// Generates Help string using Config and Registered Arguments
        /// </summary>
        /// <returns>Help String</returns>
        public static string GenerateHelp()
        {
            var builder = new StringBuilder();
            if (Config.HelpHeader is not null)
            {
                builder.AppendLine(Config.HelpHeader);
            }
            builder.AppendLine();

            foreach (var command in Arguments.GetArgumentsStrings())
            {
                builder.AppendLine(command);
            }

            if (Config.HelpExample is not null)
            {
                var module = Process.GetCurrentProcess().MainModule;
                if (module is not null)
                {
                    builder.Append(module.FileName);
                    builder.Append(' ');
                }
                builder.AppendLine(Config.HelpExample);
                builder.AppendLine();
            }

            if (Config.HelpFooter is not null)
            {
                builder.AppendLine(Config.HelpFooter);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates Version string using Config and Registered Arguments
        /// </summary>
        /// <param name="thisInfo">If set to true, shows version info about this library</param>
        /// <returns>Version string</returns>
        public static string GenerateVersion(bool thisInfo = true)
        {
            var builder = new StringBuilder();
            builder.Append(Config.FullName);
            builder.Append(' ');
            builder.AppendLine(Config.Version);
            if (!string.IsNullOrWhiteSpace(Config.License))
            {
                builder.AppendLine(Config.License);
            }

            if (thisInfo)
            {
                builder.AppendLine();
                builder.AppendLine("Uses CLI-Helper (0.0.1)");
                builder.AppendLine("Copyright (C) 2025 Oliver Neuschl");
                builder.AppendLine("This software uses GPL 3.0 License");
            }
            
            return builder.ToString();
        }
    }
}
