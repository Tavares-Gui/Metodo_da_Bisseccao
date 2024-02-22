using System.Runtime.CompilerServices;
using IA;

double MyFunction(double x)
{
    return (x - 1) * (x - 1) + Math.Sin(x * x * x);
}

double MyFunctionDims(double[] x)
{
    // return x[0] * x[0] + x[1] * x[1];
    // return (x[0] + 2 * x[1] - 7) * (x[0] + 2 * x[1] - 7) + (2 * x[0] + x[1] - 5) * (2 * x[0] + x[1] - 5);

    double sum = 0;
    int n = x.Length - 1;

    for (int i = 0; i < n; i++)
    {
        sum += 100 * (Math.Pow(x[i + 1] - (x[i] * x[i]), 2) + Math.Pow(x[i] - 1, 2));
    }

    return sum;
}

// double MyDer(double x)
// {
//     return x / (2.0 * Math.Sqrt(Math.Abs(x))) + (Math.Sqrt(Math.Abs(x)) - 5);
// }

double sol;
double[] solVal;
var date = DateTime.Now;

// date = DateTime.Now;
// sol = Root.Bisection(MyFunction, -10.0, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");


// date = DateTime.Now;
// sol = Root.FalsePosition(MyFunction, -10.0, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");


// date = DateTime.Now;
// sol = Root.Newton(MyFunction, MyDer, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");


// date = DateTime.Now;
// sol = Root.Newton(MyFunction, double(double x) => Diff.Differentiate(MyFunction, x), 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");


// date = DateTime.Now;
// sol = Optimize.Newton(MyFunction, 1);

// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

double[] val = {10, 10};
date = DateTime.Now;
solVal = Optimize.GradientDescent(MyFunctionDims, val, 1e-5, 1e-9);

Console.WriteLine($"x: {solVal[0]} | y: {solVal[1]} | Time: {(DateTime.Now - date).TotalMilliseconds}");
