using System;
using System.Linq;
using System.Collections;

namespace src
{
    public class Bezier
    {
        // Attributes
        public int numOfPoints;
        public int order;
        public int pointDim;

        // Data Structures
        private float[,] PArray;

        // Preprocessed Variables


        // Methods
        public Bezier(float[,] _PArray)
        {
            PArray = _PArray;
            numOfPoints = PArray.GetLength(0);
            order = numOfPoints - 1;
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
                float[] result = new float[2] {0.0F, 0.0F};

                float[,] highestOrderResult = PArray;

                //for (int i = 0; i < numOfPoints; i++)
                //{
                //    float basis = CalculateBasis(i, t);
                //    float x = PArray[i, 0];
                //    float y = PArray[i, 1];

                //    result[0] += basis * x;
                //    result[1] += basis * y;
                //}

                for (int currentOrder = 0; currentOrder < order; currentOrder++)
                {
                    highestOrderResult = NextLevel(highestOrderResult, t);
                }

                if (highestOrderResult.GetLength(0) == 1)
                {
                    result[0] = highestOrderResult[0, 0];
                    result[1] = highestOrderResult[0, 1];
                }

                return result;
            }
            else
            {
                Console.WriteLine("Invalid t");
                return null;
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

        // Helper Function to evaluate basis
        private float CalculateBasis(int i, float t, int currentOrder)
        {
            int nFact = currentOrder, iFact = i, niFact = currentOrder - i;

            for (int counter = nFact - 1; counter >= 1; counter--)
            {
                nFact *= counter;
            }
            for (int counter = iFact - 1; counter >= 1; counter--)
            {
                iFact *= counter;
            }
            for (int counter = niFact - 1; counter >= 1; counter--)
            {
                niFact *= counter;
            }

            float fact = 1.0F;
            if (!(i == 0 || currentOrder == i))
            {
                fact = (float)nFact / ((float)iFact * (float)niFact);
            }

            float rest = (float)(Math.Pow(t, i) * Math.Pow((1.0F - t), (currentOrder - i)));

            return fact * rest;
        }

        // Helper Function to generate higher level point array
        private float[,] NextLevel(float[,] currentLevel, float t)
        {
            int currentNumOfPoints = currentLevel.GetLength(0);

            float[,] result = new float[currentNumOfPoints-1, pointDim];

            for (int i = 0; (i + 1) < currentNumOfPoints; i++)
            {
                float currentPointX = currentLevel[i, 0];
                float currentPointY = currentLevel[i, 1];

                float nextPointX = currentLevel[i+1, 0];
                float nextPointY = currentLevel[i+1, 1];

                // Interpolate
                result[i, 0] = (1.0f - t) * currentPointX + t * nextPointX;
                result[i, 1] = (1.0f - t) * currentPointY + t * nextPointY;
            }


            return result;
        }
    }
}
