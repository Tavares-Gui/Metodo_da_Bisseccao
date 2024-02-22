namespace IA;

public static class Optimize
{
    public static double Newton(
        Func<double, double> function, double x0,
        double h = 1e-2, double aTol = 1e-4, int maxIter = 10000
    )
    {
        Func<double, double> diffFunction = double (double x) => Diff.Differentiate(function, x, h);
        Func<double, double> diffSecondFunction = double (double x) => Diff.Differentiate(diffFunction, x, h);

        return Root.Newton(diffFunction, diffSecondFunction, x0, aTol, maxIter);
    }

    public static double GradientDescent(
        Func<double, double> function, double x0,
        double lr = 1e-2, double aTol = 1e-4
    )
    {
        double xp = x0;
        double diff = Diff.Differentiate(function, xp);

        while (Math.Abs(diff) > aTol)
        {
            diff = Diff.Differentiate(function, xp);
            xp -= lr * diff;
        }

        return xp;
    }

    public static double[] GradientDescent(
        Func<double[], double> function, double[] x0,
        double lr = 1e-2, double aTol = 1e-4
    )
    {
        var dim = x0.Length;
        var xp = (double[])x0.Clone();
        double[] diff = Diff.Gradient(function, xp);
        double diffNorm;

        do
        {
            diffNorm = 0.0;
            diff = Diff.Gradient(function, xp);

            for (int i = 0; i < dim; i++)
            {
                xp[i] -= lr * diff[i];
                diffNorm += Math.Abs(diff[i]);
            }

            diffNorm /= dim;
        } while (diffNorm > dim * aTol);

        return xp;
    }
}