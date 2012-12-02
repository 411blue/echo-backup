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
        }

        private static void privateInit()
        {
            writer = new StreamWriter(filename, true);
            Logger.Log("Opened log file: " + filename);
        }

        /// <summary>
        /// Logs a string to the log file. Prepends the log entry with the date and time followed by a space and appends with a newline.
        /// </summary>
        /// <param name="s">The string to be logged</param>
        public static void Log(String s) {
            String dateString = DateTime.Now.ToString();
            writer.WriteLine(dateString + " " + s);
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
