using System;
using System.IO;
using System.Linq;

namespace RePlays.Utils {
    public static class Functions {
        static string[] programArgs;

        public static void SetProgramArgs(string[] args) {
            programArgs = args;
        }

        public static string[] GetProgramArgs() {
            return programArgs;
        }

#if DEBUG
        public static string GetSolutionPath() {
            DirectoryInfo? directory = new(AppContext.BaseDirectory);
            while (directory != null && !directory.GetFiles("*.sln").Any()) {
                directory = directory.Parent;
            }
            if (directory != null) {
                return directory.FullName;
            }
            try {
                return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            }
            catch (NullReferenceException) {
                return Directory.GetCurrentDirectory();
            }
        }
#endif

        public static string GetStartupPath() {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static string GetCfgFolder() {
            var cfgDir = Path.Join(GetStartupPath(), @"../cfg/");
            if (!Directory.Exists(cfgDir))
                Directory.CreateDirectory(cfgDir);
            return cfgDir;
        }

        public static string GetResourcesFolder() {
#if (DEBUG)
            return GetSolutionPath() + @"/Resources/";
#elif (RELEASE)
                    return GetStartupPath() + @"/Resources/";
#endif
        }
    }
}