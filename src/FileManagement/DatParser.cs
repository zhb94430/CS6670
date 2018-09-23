using System;
using System.IO;

namespace src
{
    //TODO Finish Parser
    //     Unit Test

    public class DatParser
    {
        // Arrays to store the points
        private float[,] PArray;

        // Curves Class stores a list of parsed curves
        private Curves curves;

        public DatParser(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                this.Parse(File.OpenText(fileLocation));
            }
            else
            {
                Console.WriteLine("Invalid Path");
            }
        }

        private void Parse(StreamReader r)
        {
            int numberOfLines = -1;
            int arrayLength = 0;
            int currentIndex = 0;

            while(r.Peek() >= 0 && numberOfLines != 0)
            {
                string currentLine = r.ReadLine();

                // # Comment
                if (currentLine.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                // Parse the line
                string[] parsed = currentLine.Split(' ');

                // Number of lines
                if (parsed.Length == 1)
                {
                    numberOfLines = int.Parse(parsed[0]);
                }

                // Number of Points & Points Type
                else
                {
                    // Record amount of points and init array
                    if (parsed[0].Equals("Q") || parsed[0].Equals("P"))
                    {
                        arrayLength = int.Parse(parsed[1]);

                        PArray = new float[arrayLength, 2];
                        currentIndex = 0;
                    }

                    // Record Points
                    else if (currentIndex < arrayLength)
                    {
                        // Q Points
                        if (parsed.Length == 4)
                        {
                            float x = float.Parse(parsed[1]) / float.Parse(parsed[3]);
                            float y = float.Parse(parsed[2]) / float.Parse(parsed[3]);

                            PArray[currentIndex, 0] = x;
                            PArray[currentIndex, 1] = y;
                            currentIndex++;
                        }

                        // P Points
                        else if (parsed.Length == 3)
                        {
                            float x = float.Parse(parsed[1]);
                            float y = float.Parse(parsed[2]);

                            PArray[currentIndex, 0] = x;
                            PArray[currentIndex, 1] = y;
                            currentIndex++;
                        }
                    }

                    // Generate Bezier Line
                    else if (currentIndex == arrayLength)
                    {
                        Console.WriteLine("Generating Bezier Line with ");
                        Console.WriteLine(arrayLength);

                        curves.AddBezier(new Bezier(PArray));

                        currentIndex = 0;
                        arrayLength = 0;
                        PArray = new float[0, 0];
                    }
                }
            }
        }

        public Curves GetResult()
        {
            return curves;
        }
    }
}
