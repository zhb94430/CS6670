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

        // Methods
        public Bezier(float[,] _PArray)
        {
            PArray = _PArray;
            numOfPoints = PArray.GetLength(0);
            pointDim = PArray.GetLength(1);
        }

        public float[,] GetPoints()
        {
            return PArray;
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


    }
}
