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
            // Open the input file for reading
            using (var outputWriter = new StreamWriter(destinationFile))
            {
                bool isFirstFile = true;
                foreach (var inputFile in sourceFiles)
                {
                    using (var inputReader = new StreamReader(inputFile))
                    {
                        string line;
                        bool isHeaderLine = true;

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

                            if (line.StartsWith("Complete results are available"))
                            {
                                // Skip the line and set the flag to skip the next line as well
                                continue;
                            }

                            if (isHeaderLine)
                            {
                                // Write header only for the first file
                                if (isFirstFile) outputWriter.WriteLine(line);
                                isHeaderLine = false;
                            }
                            else
                            {
                                outputWriter.WriteLine(line);
                            }
                        }
                        isFirstFile = false;
                    }
                }
            }
        }
    }
}
