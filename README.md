# Ollama to LM Studio Bridge (.NET Windows Forms)

A native .NET Windows Forms application converted from the Python symlink script that exposes Ollama GGUF model files to LM Studio without duplicating gigabytes of storage.

## Features
- **Dedicated Configuration Screen**: Browse and persist your Ollama models and LM Studio models directories with one click.
- **Persistent Settings**: Saves to `%APPDATA%\OllamaLmStudioBridge\settings.json` and automatically restores on launch.
- **Auto-Detection**: Automatically detects `%OLLAMA_MODELS%` environment variable or default `~/.ollama/models` and `~/.lmstudio/models`.
- **Symlink & Hardlink Support**:
  - **Symbolic Link**: Standard Windows link (requires Administrator or Windows Developer Mode).
  - **Hard Link**: Works without Administrator privileges as long as Ollama and LM Studio reside on the same drive partition (e.g., `C:\`).
- **Selective Linking**: Filter, search, and select individual models with checkboxes, or link all at once.
- **Live Diagnostics Console**: Real-time colored event log for monitoring scanning, digest parsing, and symlink creation.
- **High-DPI Ready**: PerMonitorV2 manifest enabled for crisp rendering on 4K and multi-monitor setups.

## Prerequisites
- Windows 10 (version 1703+) or Windows 11
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher (or [.NET Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0/runtime))
- Visual Studio 2022 (Community, Professional, or Enterprise) OR the `dotnet` CLI

## How to Build & Run

### Option A: Using .NET CLI (Fastest)
1. Open PowerShell or Command Prompt in this folder.
2. Build the project:
   ```powershell
   dotnet build OllamaLmStudioBridge/OllamaLmStudioBridge.csproj
   ```
3. Run the application:
   ```powershell
   dotnet run --project OllamaLmStudioBridge/OllamaLmStudioBridge.csproj
   ```

### Option B: Publish Single-File Executable
Generate a standalone, single-file `.exe` that can be run on any Windows machine:
```powershell
dotnet publish OllamaLmStudioBridge/OllamaLmStudioBridge.csproj -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -o ./publish
```
The resulting executable will be in `./publish/OllamaLmStudioBridge.exe`.

### Option C: Using Visual Studio 2022
1. Open `OllamaLmStudioBridge.sln`.
2. Select **Release** or **Debug** configuration.
3. Press **F5** to start debugging.
