using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Properties;

namespace Backend.Storage
{
    public struct FileInChunk
    {
        public string path;
        //the first byte of the file in the chunk
        //nonzero iff this is not the first part of the file in the chunk
        public long fileStart;
        public FileInChunk(string path, long fileStart)
        {
            this.path = path;
            this.fileStart = fileStart;
        }
    }

    public class Chunk
    {
        private int chunkID;
        private long backupID;
        //basePath of the files in this chunk; includes a trailing backslash
        private string basePath;
        private LinkedList<FileInChunk> files;
        //path to the file of this chunk
        private string path;

        public static string PathToChunk(Guid sourceGuid, long backupID, long chunkID)
        {
            // "<backup file storage dir>\<source host>\<backup id>\<sourceGuid>_<backupID>_<chunkID>.tgz"
            return System.IO.Path.Combine(Node.GetBackupDirectory(),sourceGuid.ToString(), sourceGuid.ToString() + '_' + chunkID + ".tgz");
        }
        public static string PathToChunk(PushRequest request)
        {
            return PathToChunk(request.SourceGuid, request.BackupNumber, request.ChunkNumber);
        }
        public static string PathToChunk(PullRequest request)
        {
            return PathToChunk(request.SourceGuid, request.BackupNumber, request.ChunkNumber);
        }

        public Chunk(long backupID, int chunkID, string basePath, string path)
        {
            this.backupID = backupID;
            this.chunkID = chunkID;
            this.basePath = basePath;
            files = new LinkedList<FileInChunk>();
            this.path = path;
        }

        public long BackupID()
        {
            return backupID;
        }
        public int ChunkID()
        {
            return chunkID;
        }
        public string BasePath()
        {
            return basePath;
        }
        public LinkedList<FileInChunk> Files()
        {
            return files;
        }
        public string Path()
        {
            return path;
        }

        public override string ToString()
        {
            return String.Format("BackupID: {0}, ChunkID: {1}, Path: {2}, BasePath: {3}, File Count: {4}", backupID, chunkID, path, basePath, files.Count());
        }

        public void AddLast(FileInChunk f)
        {
            files.AddLast(f);
        }

    }
}
