using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeCsvs.Data
{
    internal class Repo
    {
        Dictionary<string, List<string>> _files;

        public Repo()
        {
            _files = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> CreateRepo(string folderRootPath)
        {
            var fileLocations = new Dictionary<string, List<string>>();
            var folderFileNames = new List<HashSet<string>>();

            // Collect file paths and file names in each subfolder
            foreach (var folder in Directory.GetDirectories(folderRootPath))
            {
                var fileNames = new HashSet<string>();
                foreach (var file in Directory.GetFiles(folder, "*.csv"))
                {
                    string fileName = Path.GetFileName(file);
                    fileNames.Add(fileName);

                    if (!fileLocations.ContainsKey(fileName))
                    {
                        fileLocations[fileName] = new List<string>();
                    }
                    fileLocations[fileName].Add(file);
                }
                folderFileNames.Add(fileNames);
            }

            // Identify common files
            var commonFiles = folderFileNames
                .Skip(1)
                .Aggregate(new HashSet<string>(folderFileNames.First()), (h, e) => { h.IntersectWith(e); return h; });

            // Filter out non-common files
            var keysToRemove = fileLocations.Keys.Where(k => !commonFiles.Contains(k)).ToList();
            foreach (var key in keysToRemove)
            {
                fileLocations.Remove(key);
            }

            // Convert to the desired structure
            var listOfDicts = new List<Dictionary<string, List<string>>>
            {
                fileLocations
            };

            return fileLocations;
        }
    }
}
