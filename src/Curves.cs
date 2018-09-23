using System;
using System.Collections.Generic;

namespace src
{
    public class Curves
    {
        private List<Bezier> bezierList;

        public Curves()
        {
            bezierList = new List<Bezier>();
        }

        public void AddBezier(Bezier b)
        {
            bezierList.Add(b);
        }
    }
}
