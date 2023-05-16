using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class Integ{

public static double integrate(Func<double, double> f, double a, double b,
				double d=0.001, double e = 0.001, double f2=Double.NaN, double f3=Double.NaN)
	{
	double h = b-a;
	if(Double.IsNaN(f2)) {f2=f(a+2*h/6); f3=f(a+4*h/6);}
	double f1 = f(a+h/6), f4 = f(a+5*h/6);
	double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
	double q = ( f1+f2+f3+f4)/4*(b-a);
	double err = Abs(Q-q);
	if (err <= d+e*Abs(Q)) return Q;
	else return integrate(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2)+integrate(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
	}


public static (double, int) integrateCount(Func<double, double> f, double a, double b,
				double d=0.001, double e = 0.001, double f2=Double.NaN, double f3=Double.NaN)
	{
	int c = 0;
	double h = b-a;
	if(Double.IsNaN(f2)) {f2=f(a+2*h/6); f3=f(a+4*h/6); c+=2;}
	double f1 = f(a+h/6), f4 = f(a+5*h/6); c+=2;
	double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
	double q = ( f1+f2+f3+f4)/4*(b-a);
	double err = Abs(Q-q);
	if (err <= d+e*Abs(Q)) return (Q, c);
	(double integ1, int c1) = integrateCount(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2);
	(double integ2, int c2) = integrateCount(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
	return (integ1+integ2, c1 + c2 + c);
	}

public static double integrateChenshaw(Func<double, double> f, double a, double b,
				double d=0.001, double e = 0.001, double f2=Double.NaN, double f3=Double.NaN)
	{
	Func<double, double> fcs = t => f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
	return integrate(fcs, 0, PI, d, e);
	}

public static (double, int) integrateClenshawCount(Func<double, double> f, double a, double b,
				double d=0.001, double e = 0.001, double f2=Double.NaN, double f3=Double.NaN)
	{
	Func<double, double> fcs = t => f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
	(double integi, int counti) = integrateCount(fcs, 0, PI, d, e);
	return (integi, counti);
	}

}
