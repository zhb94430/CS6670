using System;
using System.Collections.Generic;
using Xunit;

using src;

namespace srcTest
{
    public class BezierTest
    {
        [Fact]
        public void ConstructorTest()
        {
            List<Bezier.BPoint> input = new List<Bezier.BPoint> { new Bezier.BPoint( 1.0F, 1.0F),
                                               new Bezier.BPoint( 0.707F, 0.707F),
                                               new Bezier.BPoint( 1.0F, 0.0F) };

            Bezier b = new Bezier(input);

            Assert.Equal(input, b.GetPoints());
        }

        [Fact]
        public void EvaluateAtTestSimple()
        {
            List<Bezier.BPoint> input = new List<Bezier.BPoint> { new Bezier.BPoint( 1.0F, 1.0F),
                                               new Bezier.BPoint( 0.707F, 0.707F),
                                               new Bezier.BPoint( 1.0F, 0.0F) };

            Bezier b = new Bezier(input);

            Bezier.BPoint? result = b.EvaluateAt(0.5F);
            Bezier.BPoint? expected = new Bezier.BPoint ( 0.8535F, 0.6035F );

            Assert.Equal(expected, result);
        }
    }
}