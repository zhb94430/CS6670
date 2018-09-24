using System;
using System.Linq;
using System.Collections;

namespace src
{
    //TODO Raster Function
    //     Evaluate


    public class Bezier
    {
        // Attributes
        public int numOfPoints;
        public int pointDim;

        // Data Structures
        private float[,] PArray;

        // Preprocessed Variables


        // Methods
        public Bezier(float[,] _PArray)
        {
            PArray = _PArray;
            numOfPoints = PArray.GetLength(0);
            pointDim = PArray.GetLength(1);

            PrepareVariables();
        }

        public float[,] GetPoints()
        {
            return PArray;
        }

        // Return the x,y coordiante of the point at t
        public float[] EvaluateAt(float t)
        {
            // Validate t
            if (t >= 0.0f && t <= 1.0f)
            {

            }
            else
            {
                Console.WriteLine("Invalid t");
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as Bezier;

            if (item == null)
            {
                return false;
            }

            // https://stackoverflow.com/questions/12446770/how-to-compare-multidimensional-arrays-in-c-sharp

            var result = this.PArray.Rank == item.PArray.Rank 
                      && Enumerable.Range(0, this.PArray.Rank).All(dimension => this.PArray.GetLength(dimension) == item.PArray.GetLength(dimension)) 
                      && this.PArray.Cast<float>().SequenceEqual(item.PArray.Cast<float>());

            return result;
        }

        public override int GetHashCode()
        {
            return this.PArray.GetHashCode();
        }

        // Helper Function to process some fixed variables for the Bezier Curve
        private void PrepareVariables()
        {

        }
    }
}
