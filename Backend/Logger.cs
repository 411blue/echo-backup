using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Backend {
    class Logger {
        private StreamWriter writer;
        private String filename = "C:\\log.log";

        public Logger() {
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
