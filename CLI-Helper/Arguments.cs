using System.Data;

namespace CLIHelper
{
    public static class Arguments
    {
        private static Dictionary<string, object> argumentsData = [];
        private static Dictionary<string, ArgumentDefinition> argumentsDefinitions = [];
        public static List<string> ExtraArguments { get; private set; } = new List<string>();

        public static List<string[]> GetArgumentsStrings()
        {
            return [.. argumentsDefinitions.Values.Select(v => new string[] { v.ToString(), v.Description })];
        }

        public static void Reset()
        {
            argumentsData.Clear();
            argumentsDefinitions.Clear();
            Config.KeepUnknownAsSingleString = false;
            Config.IgnoreUnknownArguments = false;
            ExtraArguments.Clear();
        }

        public static void ParseArguments(string[] args)
        {
            if (Config.KeepUnknownAsSingleString && Config.IgnoreUnknownArguments)
            {
                throw new Exception("Cannot have both KeepUnknownAsSingleString and IgnoreUnknownArguments set to true");
            }

            List<string> extraArgs = [];

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("--"))
                {
                    arg = arg[2..];
                }
                else if (arg.StartsWith('-') || arg.StartsWith('/'))
                {
                    arg = arg[1..];
                }
                else
                {
                    if (Config.IgnoreUnknownArguments)
                    {
                        continue;
                    }
                    if (Config.KeepUnknownAsSingleString)
                    {
                        extraArgs.Add(args[i]);
                        continue;
                    }
                    throw new Exception($"Unrecognized argument {arg}");
                }

                var selected = argumentsDefinitions.Where(t => t.Value.Matches(arg));
                if (!selected.Any())
                {
                    if (Config.IgnoreUnknownArguments)
                    {
                        continue;
                    }
                    if (Config.KeepUnknownAsSingleString)
                    {
                        extraArgs.Add(args[i]);
                        continue;
                    }
                    throw new Exception($"Unrecognized argument {arg}");
                }

                var argumentName = selected.First().Key;
                var argumentDefinition = argumentsDefinitions[argumentName];
                if (argumentDefinition.Type == ArgumentType.Flag)
                {
                    argumentsData[argumentName] = true;
                }
                else
                {
                    if ((i + 1) >= args.Length)
                    {
                        throw new IndexOutOfRangeException($"Argument {arg} is missing an option");
                    }
                    var option = args[++i];
                    switch (argumentDefinition.Type)
                    {
                        case ArgumentType.String:
                            argumentsData[argumentName] = option;
                            break;
                        case ArgumentType.Integer:
                            {
                                if (!int.TryParse(option, out var number))
                                {
                                    throw new IndexOutOfRangeException($"Argument {arg} has incorrecect option {option} - has to be whole number");
                                }
                                argumentsData[argumentName] = number;
                                break;
                            }
                        case ArgumentType.Double:
                            {
                                if (!double.TryParse(option, out var number))
                                {
                                    throw new IndexOutOfRangeException($"Argument {arg} has incorrecect option {option} - has to be floating point number");
                                }
                                argumentsData[argumentName] = number;
                                break;
                            }
                        case ArgumentType.Boolean:
                            {
                                if (!bool.TryParse(option, out var boolvalue))
                                {
                                    throw new IndexOutOfRangeException($"Argument {arg} has incorrecect option {option} - has to be boolean value ({bool.TrueString}/{bool.FalseString})");
                                }
                                argumentsData[argumentName] = boolvalue;
                                break;
                            }
                    }
                }
            }
            ExtraArguments.AddRange(extraArgs);
        }

        public static void RegisterArgument(string name, ArgumentDefinition argument)
        {
            if (argumentsDefinitions.ContainsKey(name))
            {
                throw new DuplicateNameException($"Argument {name} already registered");
            }
            argumentsDefinitions.Add(name, argument);
            argumentsData.Remove(name);
        }

        public static void RegisterArgument(string name, ArgumentDefinition argument, object defaultValue)
        {
            if (argumentsDefinitions.ContainsKey(name))
            {
                throw new DuplicateNameException($"Argument {name} already registered");
            }
            argumentsDefinitions.Add(name, argument);
            argumentsData[name] = defaultValue;
        }

        public static bool IsArgumentSet(string name)
        {
            return (argumentsData.ContainsKey(name));
        }

        public static object GetArgumentData(string name)
        {
            if (!IsArgumentSet(name))
            {
                throw new Exception($"Argument {name} is not set.");
            }
            return argumentsData[name];
        }

        public static void SetArgumentData(string name, object value)
        {
            argumentsData[name] = value;
        }
    }
}
