using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks.Dataflow;

namespace IA
{
    public class Root
    {
        private static double middle { get; set; }
        public static double bis { get; set; }
        private static double x0 { get; set; }
        private static double x1 { get; set; }
        private static double x2 { get; set; }
        private static double y0 { get; set; }
        private static double y1 { get; set; }
        public static double xp { get; set; }
        public static double x { get; set; }

        public static double Bisection(Func<double, double> function, double a, double b, double aTol = 1e-4, double rTol = 1e-4, int maxIter = 1000)
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

                if (Math.Abs(x2) < aTol)
                    break;

                if (x2 - x0 < 2.0 * rTol)
                    break;
            }

            return middle;
        }

        public static double FalsePosition(Func<double, double> function, double a, double b)
        {
            x0 = a;
            x1 = b;

            y0 = function(x0);
            y1 = function(x1);

            x2 = (-y0) * ((x1 - x0) / (y1 - y0)) + x0;

            bis = Bisection(function, x2, x1);

            return bis;
        }

        public static double Newton(
            Func<double, double> function, Func<double, double> der,
            double x0, double aTol = 1e-4, int maxIter = 10000
        )
        {
            xp = x0;

            for (int i = 0; i < maxIter; i++)
            {
                var fp = function(xp);
                xp -= fp / der(xp);

                if (Math.Abs(function(xp)) < aTol)
                    break;
            }


            return xp;
        }
    }
}