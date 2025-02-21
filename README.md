# CLI-Helper

CLI-Helper is a lightweight, flexible command-line argument parsing library for C# applications. It simplifies handling CLI arguments, supports multiple prefix styles, and provides automatic help and version information generation.

## Features

- Supports **short (`-v`)**, **long (`--version`)**, and **Windows-style (`/version`)** arguments.
- Configurable **argument prefix style** (POSIX, Windows, or Automatic detection).
- Automatic **help message generation**.
- Built-in **version information management**.
- Exception handling for **missing required arguments** or **invalid values**.
- **Default values** for optional arguments.
- **Case-insensitive argument matching**.
- **Help and version string generation based on configuration.**

## Installation

CLI-Helper can be used as a **Git submodule** or added manually to your project.

### Adding as a Submodule
```sh
cd your_project
git submodule add https://github.com/yourusername/CLI-Helper.git CLI-Helper
```

### Adding Manually
1. Clone or download the repository.
2. Add the `CLI-Helper` project to your solution.
3. Reference `CLI-Helper` in your project.

## Usage

### Registering Arguments
```csharp
using CLIHelper;

Arguments.RegisterArgument("version", new ArgumentDefinition(
    ArgumentType.Flag, "version", "v", "Displays the software version"));
```

### Parsing Arguments
```csharp
string[] args = Environment.GetCommandLineArgs();
Arguments.ParseArguments(args);
```

### Checking if an Argument is Set
```csharp
if (Arguments.IsArgumentSet("version"))
{
    Console.WriteLine("Version: " + Config.Version);
}
```

### Retrieving Argument Values
```csharp
string inputFile = (string)Arguments.GetArgumentData("inputfile");
Console.WriteLine("Processing file: " + inputFile);
```

## Configuration

The `Config` class allows customization of argument prefix styles, help messages, and version information.

### Example Configuration
```csharp
Config.PrefixStyle = PreferredArgumentPrefixStyle.Windows;
Config.Version = "1.0.0";
Config.FullName = "CLI-Helper Argument Parser";
Config.License = "GPL-3.0";
```

### Prefix Styles
- **Windows (`/`)**
- **POSIX (`--` or `-`)**
- **Automatic (Based on OS)**

Example:
```csharp
string longPrefix = Config.LongPrefix;  // Returns "--" or "/" depending on settings
```

## Help and Version Information

CLI-Helper can generate help and version strings automatically using the registered arguments and configuration settings.

### Generating Help String
```csharp
Console.WriteLine(Generator.GenerateHelp());
```

This will generate a formatted help string based on the registered arguments and configuration settings.

### Generating Version String
```csharp
Console.WriteLine(Generator.GenerateVersion());
```

This will generate a version string containing the configured software version and license details.

## Error Handling

- If a **required argument is missing**, an exception is thrown with an explanatory message.
- If an **invalid value** is passed for an argument (e.g., `--number abc` where `number` is an integer), an exception is thrown.

## License

CLI-Helper is licensed under **GPL-3.0**.

## Contributions

Contributions are welcome! Feel free to fork the repository and submit pull requests.

