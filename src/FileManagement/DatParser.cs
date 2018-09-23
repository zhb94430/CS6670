using System;
using System.IO;

namespace src
{
    //TODO Finish Parser
    //     Unit Test

    public class DatParser
    {
        // Arrays to store the points
        float[][] PArray;

        Bezier b = new Bezier(PArray);

        public DatParser(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                Parse(new StreamReader(path));
            }
            else
            {
                Console.WriteLine("Invalid Path for DatParser");
            }
        }

        private void Parse(StreamReader r)
        {
            while(r.Peek() >= 0)
            {
                string currentLine = r.ReadLine();

                // # Comment
                if (currentLine.StartsWith("#"))
                {
                    continue;
                }

                // Parse the line
                string[] parsed = currentLine.Split(" ");

                // Number of lines
                if (parsed.Length == 1)
                {

                }

                // Number of Points & Points Type
                else
                {
                    // Record amount of points and init array
                    if (parsed[0].Equals("Q") || parsed[0].Equals("P"))
                    {

                    }
                    // Record Points
                    else
                    {
                        // Q Points
                        if (parsed.Length == 4)
                        {

                        }

                        // P Points
                        else if (parsed.Length == 3)
                        {

                        }
                    }
                }
            }
        }
    }
}
