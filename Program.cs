using IA;

double MyFunction(double x)
{
    return x * x;
}

double MyDer(double x)
{
    return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
}

double sol;
var date = DateTime.Now;

date = DateTime.Now;
sol = Root.Bisection(MyFunction, -10.0, 10.0);
Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
sol = Root.FalsePosition(MyFunction, -10.0, 10.0);
Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
sol = Root.Newton(MyFunction, MyDer, 10.0);
Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");
