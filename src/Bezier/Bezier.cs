using System;
using System.Linq;
using System.Collections.Generic;

namespace src
{
    public class Bezier
    {
        // Attributes
        public int numOfPoints;
        public int order;
        public int pointDim;

        public struct BPoint
        {
            public float x;
            public float y;

            public BPoint (float _x, float _y)
            {
                x = _x;
                y = _y;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() ^ y.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is BPoint && this == (BPoint)obj;
            }

            public static bool operator ==(BPoint x, BPoint y)
            {
                return x.x == y.x && x.y == y.y;
            }
            public static bool operator !=(BPoint x, BPoint y)
            {
                return !(x == y);
            }
            public static BPoint operator -(BPoint x, BPoint y)
            {
                return new BPoint(x.x - y.x, x.y - y.y);
            }
        }

        // Data Structures
        private List<BPoint> PArray;

        // Preprocessed Variables


        // Methods
        public Bezier(List<BPoint> _PArray)
        {
            PArray = _PArray;
            numOfPoints = PArray.Count;
            order = numOfPoints - 1;
            pointDim = 2;

            PrepareVariables();
        }

        public List<BPoint> GetPoints()
        {
            return PArray;
        }

        public void ReplacePointWith(BPoint oldP, BPoint newP)
        {
            int i = PArray.IndexOf(oldP);

            if (i != -1)
            {
                PArray[i] = newP;
            }
            else
            {
                Console.WriteLine("Cannot find old point in ReplacePointWith");
            }
        }

        public void InsertPoint(BPoint p)
        {
            int i = PArray.IndexOf(p);

            if (i != -1)
            {
                if (i+1 == PArray.Count)
                {
                    PArray.Add(new BPoint(p.x + 1, p.y + 1));
                }
                else
                {
                    PArray.Insert(i + 1, PArray[i + 1] - PArray[i]);
                }
            }
            else
            {
                Console.WriteLine("Cannot find point in InsertPoint");
            }
        }

        public void DeletePoint(BPoint p)
        {
            PArray.Remove(p);
        }

        // Return the x,y coordiante of the point at t
        public BPoint? EvaluateAt(float t)
        {
            // Validate t
            if (t >= 0.0f && t <= 1.0f)
            {
                BPoint result = new BPoint(0.0F, 0.0F);

                List<BPoint> highestOrderResult = PArray;

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

                if (highestOrderResult.Count == 1)
                {
                    result.x = highestOrderResult[0].x;
                    result.y = highestOrderResult[0].y;
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

            var result = this.PArray.SequenceEqual(item.PArray);

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
        private List<BPoint> NextLevel(List<BPoint> currentLevel, float t)
        {
            int currentNumOfPoints = currentLevel.Count;

            List<BPoint> result = new List<BPoint>(new BPoint[currentNumOfPoints-1]);

            for (int i = 0; (i + 1) < currentNumOfPoints; i++)
            {
                BPoint currentPoint = currentLevel[i];
                BPoint nextPoint = currentLevel[i + 1];

                // Interpolate
                result[i] = new BPoint((1.0f - t) * currentPoint.x + t * nextPoint.x, 
                                       (1.0f - t) * currentPoint.y + t * nextPoint.y);
            }


            return result;
        }
    }
}
