using IA;
using System.Diagnostics;
using System.Runtime.CompilerServices;

double Rosenbrock(double[] x)
{
    double sum = 0;
    int n = x.Length - 1;

    for (int i = 0; i < n; i++)
    {
        sum += 100 * (Math.Pow(x[i + 1] - (x[i] * x[i]), 2) + Math.Pow(x[i] - 1, 2));
    }

    return sum;
}

double Restriction(double[] x)
{
    return -1.0;
}

List<double[]> bounds = new()
{
    new double[] { -10.0, 10.0 },
    new double[] { -10.0, 10.0 }
};

var diffEvolution = new DiffEvolution(Rosenbrock, bounds, Restriction, 200);
var res = diffEvolution.Optimize(1000);

Console.WriteLine($"Res: {res[0]} | {res[1]}");
