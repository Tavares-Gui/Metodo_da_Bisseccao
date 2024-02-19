using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IA
{
    public class Root
    {
        private static double x0 { get; set; }
        private static double x2 { get; set; }
        private static double middle { get; set; }
        public static int InterCount { get; set; }

        public static double Bisection(Func<double, double> function, double a, double b, double tol = 1e-4, int maxIter = 1000)
        {
            x2 = 0;
            
            for (int i = 0; i < maxIter; i++)
            {
                middle = (a + b) / 2;
                x0 = function(a);
                x2 = function(middle);

                if (x2 * x0 > 0)
                    a = middle;
                else
                    b = middle;

                InterCount++;
            }

            return (a + b) / 2;
        }
    }
}