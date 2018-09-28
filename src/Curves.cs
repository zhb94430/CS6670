using System;
using System.Collections.Generic;
using System.Linq;

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

        public void DeleteBezier(Bezier b)
        {
            bezierList.Remove(b);
        }

        public List<Bezier> GetBezierList()
        {
            return bezierList;
        }

        public void SetBezierList(List<Bezier> beziers)
        {
            bezierList = beziers;
        }

        public void Append(Curves c)
        {
            bezierList.AddRange(c.bezierList);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Curves;

            if (item == null)
            {
                return false;
            }

            return this.bezierList.SequenceEqual<Bezier>(item.bezierList);
        }

        public override int GetHashCode()
        {
            return this.bezierList.GetHashCode();
        }
    }
}
