using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backend {
    public class Logger {
        private StreamWriter writer;
        private String filename = "log.log";
        private String defaultFileName = Environment.CurrentDirectory + "\\" + "log.log";
        
        public Logger(string path) {
            writer = new StreamWriter(path + "\\" + filename, true);
            this.Log(" Opened log file.");
        }

        public Logger()
        {
            writer = new StreamWriter(filename, true);
            this.Log(" Opened log file.");
        }

        public void Log(String s) {
            String dateString = DateTime.Now.ToString();
            writer.WriteLine(dateString + " " + s);
            this.Flush();
        }

        public void Flush() {
            writer.Flush();
        }

        public void Close() {
            writer.Close();
        }
    }
}
