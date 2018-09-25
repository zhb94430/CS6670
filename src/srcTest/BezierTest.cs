using System;
using Xunit;

using src;

namespace srcTest
{
    public class BezierTest
    {
        [Fact]
        public void ConstructorTest()
        {
            float[,] input = new float[3, 2] { { 1.0F, 1.0F},
                                               { 0.707F, 0.707F},
                                               { 1.0F, 0.0F} };

            Bezier b = new Bezier(input);

            Assert.Equal(input, b.GetPoints());
        }

        [Fact]
        public void EvaluateAtTestSimple()
        {
            float[,] input = new float[3, 2] { { 1.0F, 1.0F},
                                               { 0.707F, 0.707F},
                                               { 1.0F, 0.0F} };

            Bezier b = new Bezier(input);

            float[] result = b.EvaluateAt(0.5F);
            float[] expected = new float[2] { 0.8535F, 0.6035F };

            Assert.Equal(expected, result);
        }
    }
}