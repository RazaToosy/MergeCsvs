using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeCsvs.Services
{
    internal class CsvManager
    {
        public void ConcatenateCsvFiles(List<string> sourceFiles, string destinationFile)
        {
            bool isFirstFile = true;
            // Open the input file for reading
            using (var outputWriter = new StreamWriter(destinationFile))
            {
                foreach (var inputFile in sourceFiles)
                {
                    using (var inputReader = new StreamReader(inputFile))
                    {
                        string line;

                        while ((line = inputReader.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                // Skip empty lines
                                continue;
                            }

                            if (line.All(c => c == ','))
                            {
                                // Skip empty lines
                                continue;
                            }

                            if (line.Contains("Complete results are available"))
                            {
                                // Skip the line and set the flag to skip the next line as well
                                continue;
                            }

                            if (!isFirstFile && line.Contains("EMIS"))
                            {
                                // Skip the line and set the flag to skip the next line as well
                                continue;
                            }
                            outputWriter.WriteLine(line);
                        }
                    }
                    isFirstFile = false;
                }
            }
        }
    }
}
