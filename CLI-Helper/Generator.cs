using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;

namespace CLIHelper
{
    public static class Generator
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

            var strings = Arguments.GetArgumentsStrings();
            var longest = strings.MaxBy(strings => strings[0].Length)?[0].Length ?? 0;
            foreach (var command in strings)
            {
                builder.AppendLine(command[0].PadRight(longest, ' ') + " - " + command[1]);
            }

            if (Config.HelpExample is not null)
            {
                builder.AppendLine();
                var module = Process.GetCurrentProcess().MainModule;
                if (module is not null)
                {
                    var file = new FileInfo(module.FileName);
                    builder.Append(file.Name);
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
            builder.Append(" (");
            builder.Append(Config.Version);
            builder.AppendLine(")");
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
