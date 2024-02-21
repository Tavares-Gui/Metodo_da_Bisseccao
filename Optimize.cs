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
}