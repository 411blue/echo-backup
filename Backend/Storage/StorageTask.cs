using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Storage
{
    public abstract class StorageTask
    {
    }

    public class BackupTask: StorageTask
    {
        //the path (file or directory) that should be backed up
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        //the path to a directory where I should store temporary files (including trailing backslash)
        private string tempPath;
        public string TempPath
        {
            get { return tempPath; }
            set { tempPath = value; }
        }
        //the id of the backup
        private long backupID;
        public long BackupID
        {
            get { return backupID; }
            set { backupID = value; }
        }
        //the level of the backup. 0=full, 1=1st incr, 2=2nd incr, etc.
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        //a number representing the successfulness of the backup
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public BackupTask(string path, string tempPath, long backupID, int level)
        {
            this.path = path;
            this.tempPath = tempPath;
            this.backupID = backupID;
            this.level = level;
            this.status = 0;
        }
    }

    public class RestoreTask: StorageTask
    {
        //the path to the .tgz chunk
        private string archivePath;
        public string ArchivePath
        {
            get { return archivePath; }
            set { archivePath = value; }
        }
        //list of relative paths to files. relative to base of backup
        private List<string> relativeFilenames;
        //dir where files should be stored with original names
        private string outputDir;
        public string OutputDir
        {
            get { return outputDir; }
            set { outputDir = value; }
        }

        public RestoreTask(string archivePath, string outputDir)
        {
            this.archivePath = archivePath;
            this.outputDir = outputDir;
            relativeFilenames = new List<string>();
        }

        public void Add(string s)
        {
            relativeFilenames.Add(s);
        }

        public List<string> RelativeFilenames()
        {
            return relativeFilenames;
        }
    }
}
