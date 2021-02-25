using System;
using System.IO;

namespace DDFight.Tools
{
    public static class Logger
    {
        private static string config_folder = Environment.GetEnvironmentVariable("LocalAppData") + "\\D&DFightTool\\";

        private static string log_filepath
        {
            get => config_folder + "last_run_log.txt";
        }

        private static StreamWriter writer = null;

        public static void Init()
        {
            if (!Directory.Exists(config_folder))
            {
                Directory.CreateDirectory(config_folder);
            }
            FileStream tmp = File.Create(log_filepath);
            tmp.Close();
            writer = new StreamWriter(log_filepath);
            writer.WriteLine("Start of session");
            writer.Flush();
        }

        public static void Log(string to_log)
        {
            Console.WriteLine(to_log);
            writer.WriteLine(to_log);
            writer.Flush();
        }

        public static void CleanUp()
        {
            writer.Close();
        }
    }
}
