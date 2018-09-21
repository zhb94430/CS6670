using System;
using System.IO;

namespace Application
{
    //TODO Finish Parser
    //     Unit Test

    public class DatParser
    {
        // Arrays to store the points
        float[][] PArray;

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
            //Read the first two lines



            while(r.Peek() >= 0)
            {
                string currentLine = r.ReadLine();


                // # Comment
                if (currentLine)
                {

                }

                // Q: Rational Control Points
                // Init QArray

                // P: Control Points
                // Init PArray

                // Rest: Points, Store Accordingly
            }
        }
    }
}
