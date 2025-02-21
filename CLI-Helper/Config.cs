namespace CLIHelper
{
    /// <summary>
    /// Preferred Argument Prefix Style. Windows - /, POSIX : -- or -, Atuomatic - selects based on current OS.
    /// </summary>
    public enum PreferredArgumentPrefixStyle { Windows, POSIX, Automatic}
    
    public static class Config
    {
        /// <summary>
        /// Preferred prefix style
        /// </summary>
        public static PreferredArgumentPrefixStyle PrefixStyle = PreferredArgumentPrefixStyle.Automatic;
        /// <summary>
        /// Example command string for help
        /// </summary>
        public static string? HelpExample = null;
        /// <summary>
        /// License information
        /// </summary>
        public static string License = "";
        /// <summary>
        /// Version of software
        /// </summary>
        public static string Version = "";
        /// <summary>
        /// Full Name of Software
        /// </summary>
        public static string FullName = "";
        /// <summary>
        /// Header for Help - shows at top of Help String
        /// </summary>
        public static string? HelpHeader = null;
        /// <summary>
        /// Footer for Help - shows at bottom of Help String
        /// </summary>
        public static string? HelpFooter = null;

        /// <summary>
        /// Gets long prefix based on configuration (-- or /)
        /// </summary>
        public static string LongPrefix
        {
            get
            {
                if (PrefixStyle == PreferredArgumentPrefixStyle.Windows ||
                    (PrefixStyle == PreferredArgumentPrefixStyle.Automatic &&
                    Environment.OSVersion.Platform == PlatformID.Win32NT))
                {
                    return "/";
                }
                return "--";
            }
        }

        /// <summary>
        /// Gets long prefix based on configuration (- or /)
        /// </summary>
        public static string ShortPrefix
        {
            get
            {
                if (PrefixStyle == PreferredArgumentPrefixStyle.Windows ||
                    (PrefixStyle == PreferredArgumentPrefixStyle.Automatic &&
                    Environment.OSVersion.Platform == PlatformID.Win32NT))
                {
                    return "/";
                }
                return "-";
            }
        }
    }
}
