### Dependencies
* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* tModLoader 1.4.4

### Setting Up the Project Environment
Find your ModSources directory.  It should be at one of these locations depending on your Operating System:
- (Windows) `Documents/My Games/Terraria/tModLoader/ModSources`
- (Mac) `~/Library/Application support/Terraria/tModLoader/ModSources`
- (Linux) `~/.local/share/Terraria/tModLoader/ModSources`

Next, follow these instructions to properly build the project:
1. Run `git clone https://github.com/SpiralshapeDev/TBats.git` in the ModSources folder to clone the repository.
2. If you're using Visual Studio 2022 Community, open the `.sln` file in the folder created by Step 1, then either press F6 or select `Build > Build Solution`.  
   Otherwise, run `dotnet build` in `<ModSources>/TBats`.  
   If running that command results in an error mentioning an "exit code 150", install the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) as well.
3. Setup Done!
