using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class Integ{

public static double integrate(Func<double, double> f, double a, double b,
				double d=0.001, double e = 0.001, double f2=Double.NaN, double f3=Double.NaN)
{
	double h = b-a;
	if(Double.IsNaN(f2)) {f2=f(a+2*h/6); f3=f(a+4*h/6); }
	double f1 = f(a+h/6), f4 = f(a+5*h/6);
	double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
	double q = ( f1+f2+f3+f4)/4*(b-a);
	double err = Abs(Q-q);
	if (err <= d+e*Abs(Q)) return Q;
	else return integrate(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2)+integrate(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
}

}
