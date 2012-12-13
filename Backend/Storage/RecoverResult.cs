using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Storage
{
    public class RecoverResult
    {
        
        //path to directory containing recovered files
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        
        //status of recovery operation: true==successful, false==failure
        private bool successful;
        public bool Successful
        {
            get { return successful; }
            set { successful = value; }
        }

        public RecoverResult(string path, bool successful)
        {
            this.path = path;
            this.successful = successful;
        }
    }
}
