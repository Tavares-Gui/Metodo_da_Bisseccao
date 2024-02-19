using IA;

double MyFunction(double x)
{
    return x + 1;
}

var sol = Root.Bisection(MyFunction, -10, 10);
Console.WriteLine(sol);