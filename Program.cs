using IA;

double MyFunction(double x)
{
    return (x - 1) * (x - 1) + Math.Sin(x * x * x);
}

// double MyDer(double x)
// {
//     return x / (2.0 * Math.Sqrt(Math.Abs(x))) + (Math.Sqrt(Math.Abs(x)) - 5);
// }

double sol;
var date = DateTime.Now;
double chute = 1.0;

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

date = DateTime.Now;
sol = Optimize.Newton(MyFunction, chute);

// double oldSol = Optimize.Newton(MyFunction, chute + 1);

// while (sol > oldSol)
// {
//     chute += 5;
//     sol = Optimize.Newton(MyFunction, chute);
//     oldSol = Optimize.Newton(MyFunction, chute);
// }

Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");