using System;
using System.Globalization;

namespace NetFrameworkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Start .Net Framework 4.8 app";
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(str);
            int i = 0;

            while (enumerator.MoveNext())
            {
                Console.WriteLine($"Char {++i}: \"{enumerator.Current}\"");
            }

            // Prior analyze before upgrading
            // upgrade-assistant analyze <Path to csproj or sln to analyze>
            // e.g: upgrade-assistant analyze D:\Boots_Up_Program\Exampleprj\\NetFrameworkApp\NetFrameworkApp.csproj
            // Result
            // [08:38:47 INF] Loaded 5 extensions
            // [08:38:48 INF] MSBuild registered from C:\Program Files\dotnet\sdk\5.0.302\
            // [08:38:48 INF] Found Visual Studio v16.10.31321.278 // [C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional]
            // [08:38:48 INF] Found Visual Studio v16.5.30011.22 // [C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools]
            // [08:38:48 INF] Found Visual Studio v15.9.28307.1093 // [C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools]
            // [08:38:49 INF] Recommending executable TFM net5.0 because the project builds to an executable
            // [08:38:51 INF] Reference to .NET Upgrade Assistant analyzer package (Microsoft.DotNet.UpgradeAssistant.Extensions.Default.Analyzers, version 0.2.241603) needs added
            // [08:38:51 INF] Running analyzers on NetFrameworkApp
            // [08:38:51 INF] Identified 0 diagnostics in project NetFrameworkApp

            // Start
            // upgrade-assistant upgrade D:\Boots_Up_Program\Exampleprj\\NetFrameworkApp\NetFrameworkApp.csproj
            // 1. [Next step] Back up project
            // 2. Convert project file to SDK style
            // 3. Clean up NuGet package references
            // 4. Update TFM
            // 5. Update NuGet Packages
            // 6. Add template files
            // 7. Upgrade app config files
            //    a. Convert Application Settings
            //    b. Convert Connection Strings
            //    c. Disable unsupported configuration sections
            // 8. Update source code
            //   =  a. Apply fix for UA0002: Types should be upgraded
            //    b. Apply fix for UA0012: 'UnsafeDeserialize()' does not exist
            //    c. Apply fix for UA0014: .NET MAUI projects should not reference Xamarin.Forms namespaces
            //    d. Apply fix for UA0015: .NET MAUI projects should not reference Xamarin.Essentials namespaces
            // 9. Move to next project

            // Choose a command:
            //   1. Apply next step (Back up project)
            //  2. Skip next step (Back up project)
            //   3. See more step details
            //   4. Configure logging
            //   5. Exit
        }
    }
}
