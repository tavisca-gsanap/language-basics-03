using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise3
{
    class Frequency
    {
        public int[] FrequencyCalculator(int[] a, int x)
        {
            //int count = 0;
            IList<int> b = new List<int>();
            int k = 0;
            for (int i = 0; i < a.Length; i++)
                if (a[i] == x)
                    b.Add(i);
            return b.ToArray();
        }
    }
}
