using System.Text;

namespace CLIHelper
{
    /// <summary>
    /// Definition of Argument
    /// </summary>
    /// <param name="type">Argument Type</param>
    /// <param name="longName">Long Argument Version (eg. --version)</param>
    /// <param name="shortName">Short Argument Version (eg. -v)</param>
    /// <param name="description">Description of Argument (eg. Shows Version Information)</param>
    /// <param name="hint">Hint for optional parameter of argument</param>
    public class ArgumentDefinition (ArgumentType type, string longName, string shortName, string description, string hint = "")
    {
        /// <summary>
        /// Argument Type
        /// </summary>
        public ArgumentType Type { get; private set; } = type;

        /// <summary>
        /// Long Argument Version (eg. --version)
        /// </summary>
        public string LongName { get; private set; } = longName;
        /// <summary>
        /// Short Argument Version (eg. -v)
        /// </summary>
        public string ShortName { get; private set; } = shortName;
        /// <summary>
        /// Description of Argument (eg. Shows Version Information)
        /// </summary>
        public string Description { get; private set; } = description;
        /// <summary>
        /// Hint for optional parameter of argument
        /// </summary>
        public string Hint { get; private set; } = hint;

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, LongName, ShortName, Description);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ArgumentDefinition other) return false;
            return (other.Type == Type) && (other.LongName == LongName) && (other.ShortName == ShortName) && (other.Description == Description);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Config.LongPrefix);
            builder.Append(LongName);
            builder.Append(", ");
            builder.Append(Config.ShortPrefix);
            builder.Append(ShortName);
            if (Type != ArgumentType.Flag)
            {
                builder.Append(" [");
                builder.Append(Hint);
                builder.Append(']');
            }
            return builder.ToString();
        }

        /// <summary>
        /// Checks if argument matches the string (case insensitive)
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public bool Matches(string argument)
        {
            return (string.Equals(argument, LongName, StringComparison.CurrentCultureIgnoreCase) || string.Equals(argument, ShortName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
