using System;
using System.Collections.Generic;
using Xunit;

using src;

namespace srcTest
{
    public class FileManagementTest
    {
        [Fact]

        //TODO Assertion
        public void LoadTest_InvalidAddress()
        {
            DatParser dat = new DatParser("Wrong Address");


        }

        [Fact]
        public void LoadTest_ValidAddress_Simple()
        {
            DatParser dat = new DatParser("../../UnitTestData/Simple.dat");

            Bezier b = new Bezier(new List<Bezier.BPoint> { new Bezier.BPoint(1.0F, 1.0F),
                                                            new Bezier.BPoint(0.707F, 0.707F),
                                                            new Bezier.BPoint(1.0F, 0.0F) });

            Curves correctResult = new Curves();
            correctResult.AddBezier(b);

            Assert.StrictEqual<Curves>(correctResult, dat.GetResult());
        }

        [Fact]
        public void LoadTest_ValidAddress_SimpleRational()
        {
            DatParser dat = new DatParser("../../UnitTestData/SimpleRational.dat");

            Bezier b = new Bezier(new List<Bezier.BPoint> { new Bezier.BPoint( 1.0F, 1.0F ), 
                                                    new Bezier.BPoint( 0.707F, 0.707F ), 
                                                    new Bezier.BPoint( 1.0F, 0.0F ) });

            Curves correctResult = new Curves();
            correctResult.AddBezier(b);

            Assert.StrictEqual<Curves>(correctResult, dat.GetResult());
        }

        [Fact]
        public void LoadTest_ValidAddress_MultipleCurve()
        {
            DatParser dat = new DatParser("../../UnitTestData/apple.dat");

            Bezier b1 = new Bezier(new List<Bezier.BPoint> { new Bezier.BPoint( 3.14286F, 3.68571F ), 
                                                      new Bezier.BPoint( -0.6F, 9.4F ), 
                                                      new Bezier.BPoint( -2.2F, 0.571427F ),
                                                      new Bezier.BPoint( -4.68572F, 0.314284F ),
                                                      new Bezier.BPoint( -2.25714F, 10.1714F ),
                                                      new Bezier.BPoint( -5.8F, 9.2F ),
                                                      new Bezier.BPoint( -10.3714F, 7.37143F ),
                                                      new Bezier.BPoint( -17.8F, -1.25714F ),
                                                      new Bezier.BPoint( -8.54286F, -6.6F ),
                                                      new Bezier.BPoint( 2.97143F, -12.2571F ),
                                                      new Bezier.BPoint( -3.82857F, -5.94286F ),
                                                      new Bezier.BPoint( -5.37143F, -1.28572F ),
                                                      new Bezier.BPoint( 0.885713F, -1.74286F ),
                                                      new Bezier.BPoint( -1.22857F, -11.3143F ),
                                                      new Bezier.BPoint( 3.62857F, -1.65715F )});

            Bezier b2 = new Bezier(new List<Bezier.BPoint> { new Bezier.BPoint( 3.14286F, 3.68571F),
                                                     new Bezier.BPoint( -0.0857147F, 0.771426F),
                                                     new Bezier.BPoint( 3.62857F, -1.65715F) });

            Bezier b3 = new Bezier(new List<Bezier.BPoint> { new Bezier.BPoint( -2.17143F, 5.11429F),
                                                     new Bezier.BPoint( -2.0F, 7.62857F),
                                                     new Bezier.BPoint( -0.514285F, 6.94286F),
                                                     new Bezier.BPoint( 1.57143F, 10.4571F),
                                                     new Bezier.BPoint( 0.457144F, 5.54286F),
                                                     new Bezier.BPoint( -2.17143F, 5.05714F)});

            Curves correctResult = new Curves();
            correctResult.AddBezier(b1);
            correctResult.AddBezier(b2);
            correctResult.AddBezier(b3);

            Assert.StrictEqual<Curves>(correctResult, dat.GetResult());
        }

    }
}
