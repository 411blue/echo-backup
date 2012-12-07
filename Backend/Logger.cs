using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backend {
    /// <summary>
    /// A class with a few static methods to facilitate logging.
    /// </summary>
    public static class Logger {
        public enum LogLevel { CRIT, ERROR, WARN, NOTICE, INFO, DEBUG };
        private static LogLevel defaultLogLevel = LogLevel.INFO;
        private static StreamWriter writer;
        private static String filename;
        //this should be changed to include the date and time and shoulb be initialized in init()
        private static String defaultFileName = Environment.CurrentDirectory + "\\" + "log.log";
        
        /*public Logger(string path) {
            writer = new StreamWriter(path + "\\" + filename, true);
            this.Log("Opened log file.");
        }*/

        /*public Logger()
        {
            writer = new StreamWriter(filename, true);
            this.Log("Opened log file.");
        }*/

        /// <summary>
        /// Initializes the logger with a custom path to the log file.
        /// </summary>
        /// <param name="path"></param>
        public static void init(string path)
        {
            filename = path;
            privateInit();
        }

        /// <summary>
        /// Initializes the logger with the default path.
        /// </summary>
        public static void init()
        {
            filename = defaultFileName;
            privateInit();
        }

        private static void privateInit()
        {
            writer = new StreamWriter(filename, true);
            Logger.Log("Opened log file: " + filename);
        }

        public static string getLogFilename()
        {
            return filename;
        }

        /// <summary>
        /// Logs a string to file with the default LogLevel (Logger constant member)
        /// </summary>
        /// <param name="s">The string to be logged</param>
        public static void Log(String s)
        {
            Logger.Log(s, defaultLogLevel);
        }

        public static void Crit(String s)
        {
            Logger.Log(s, LogLevel.CRIT);
        }
        public static void Error(String s)
        {
            Logger.Log(s, LogLevel.ERROR);
        }
        public static void Warn(String s)
        {
            Logger.Log(s, LogLevel.WARN);
        }
        public static void Notice(String s)
        {
            Logger.Log(s, LogLevel.NOTICE);
        }
        public static void Info(String s)
        {
            Logger.Log(s, LogLevel.INFO);
        }
        public static void Debug(String s)
        {
            Logger.Log(s, LogLevel.DEBUG);
        }

        /// <summary>
        /// Logs a string to the log file. Prepends the log entry with the date, time and level followed by a space and appends with a newline.
        /// </summary>
        /// <param name="s">The string to be logged</param>
        /// <param name="level">The level this messaged should be logged as (enum Logger.LogLevel.*)</param>
        public static void Log(String s, LogLevel level)
        {
            String dateString = DateTime.Now.ToString();
            writer.WriteLine(dateString + " " + level + " " + s);
            Logger.Flush();
        }

        public static void Flush() {
            writer.Flush();
        }

        public static void Close() {
            writer.Close();
        }
    }
}