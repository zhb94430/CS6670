using System;
using System.IO;
using System.Collections.Generic;

namespace src
{
    public class DatParser
    {
        // Arrays to store the points
        private List<Bezier.BPoint> PArray;

        // Curves Class stores a list of parsed curves
        private Curves curves;
        private string fileLocation;

        public DatParser(string _fileLocation)
        {
            curves = new Curves();

            if (File.Exists(_fileLocation))
            {
                fileLocation = _fileLocation;
            }
            else
            {
                fileLocation = null;
                Console.WriteLine("Invalid Path");
            }
        }

        public DatParser(Curves _curves)
        {
            curves = _curves;
        }

        public Curves Parse()
        {
            int numberOfLines = -1;
            int arrayLength = 0;
            int currentIndex = 0;

            StreamReader r = File.OpenText(fileLocation);

            while(r.Peek() >= 0 && numberOfLines != 0)
            {
                string currentLine = r.ReadLine();

                // # Comment
                if (currentLine.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                // Parse the line
                string[] parsed = currentLine.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                // Number of lines
                if (parsed.Length == 1)
                {
                    if (!int.TryParse(parsed[0], out numberOfLines))
                    {
                        if (numberOfLines == 0)
                        {
                            numberOfLines = -1;
                        }
                    }
                }

                // Skip empty lines
                else if (parsed.Length == 0)
                {
                    continue;
                }

                // Number of Points & Points Type
                else
                {
                    // Record amount of points and init array
                    if (parsed[0].Equals("Q") || parsed[0].Equals("P"))
                    {
                        arrayLength = int.Parse(parsed[1]);

                        PArray = new List<Bezier.BPoint>(new Bezier.BPoint[arrayLength]);
                        currentIndex = 0;
                    }

                    // Record Points
                    else if (currentIndex < arrayLength)
                    {
                        // Q Points
                        if (parsed.Length == 3)
                        {
                            float w = float.Parse(parsed[2]);
                            float x = 0.0F;
                            float y = 0.0F;

                            if (w == 0.0F)
                            {
                                x = float.PositiveInfinity;
                                y = float.PositiveInfinity;
                            }
                            else
                            {
                                x = float.Parse(parsed[0]) / w;
                                y = float.Parse(parsed[1]) / w;
                            }

                            PArray[currentIndex] = new Bezier.BPoint(x, y);
                            currentIndex++;
                        }

                        // P Points
                        else if (parsed.Length == 2)
                        {
                            float x = float.Parse(parsed[0]);
                            float y = float.Parse(parsed[1]);

                            PArray[currentIndex] = new Bezier.BPoint(x, y);
                            currentIndex++;
                        }
                    }

                    // Generate Bezier Line
                    if (currentIndex == arrayLength)
                    {
                        Console.WriteLine("Generating Bezier Line with ");
                        Console.WriteLine(arrayLength);

                        curves.AddBezier(new Bezier(PArray));

                        currentIndex = 0;
                        arrayLength = 0;
                        PArray = new List<Bezier.BPoint>();
                    }
                }
            }

            return curves;
        }

        //public string UnParse(Curves c)
        //{

        //}
    }
}
