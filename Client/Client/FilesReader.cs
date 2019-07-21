using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class FilesReader
    {
        DirectoryInfo direction;

        public FilesReader(string path)
        {
            this.direction = new DirectoryInfo(path);
        }

        public Dictionary<string, string> GetFilesPaths()
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            foreach (FileInfo file in direction.GetFiles())
            {
                files.Add(file.Name, file.FullName);
            }
            return files;
        }

        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
