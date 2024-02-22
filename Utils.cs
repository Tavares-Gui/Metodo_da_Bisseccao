using System.Dynamic;

namespace IA;

public class Utils
{
    public static double Rescale(double x, double min, double max)
        => (max - min) * x + min;
}