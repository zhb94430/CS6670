using System;
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
            DatParser dat = new DatParser("../UnitTestData/Simple.dat");

            Bezier b = new Bezier(new float[3,2] { { 1.0F, 1.0F}, { 0.707F, 0.707F}, { 1.0F, 0.0F} });

            Curves correctResult = new Curves();
            correctResult.AddBezier(b);

            Assert.Equal(correctResult, dat.GetResult());
        }

        public void LoadTest_ValidAddress_SimpleRational()
        {
            DatParser dat = new DatParser("../UnitTestData/SimpleRational.dat");

            //Bezier b = new Bezier(new float[3, 2] { { 1.0F, 1.0F }, { 0.707F, 0.707F }, { 1.0F, 0.0F } });

            //Curves correctResult = new Curves();
            //correctResult.AddBezier(b);

            Assert.Equal(correctResult, dat.GetResult());
        }

        public void LoadTest_ValidAddress_MultipleCurve()
        {
            DatParser dat = new DatParser("../UnitTestData/apple.dat");

            //Bezier b = new Bezier(new float[3, 2] { { 1.0F, 1.0F }, { 0.707F, 0.707F }, { 1.0F, 0.0F } });

            //Curves correctResult = new Curves();
            //correctResult.AddBezier(b);

            Assert.Equal(correctResult, dat.GetResult());
        }

    }
}
