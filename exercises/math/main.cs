using System;
using static System.Console;
using static System.Math;

public static class main{
	public static void Main(){

	double sqrt2 = Sqrt(2.0);
	Write($"sqrt2 = {sqrt2} should equal 2 when sqrt2^2 = {Pow(sqrt2,2)}\n");	
	
	double r5oot2 = Pow(2,1.0/5);
	Write($"5root2 = {r5oot2} and 5root2^5 = {Pow(r5oot2,5)}\n");
	
	double expPi = Exp(PI);
	Write($"exp(pi) = {expPi}\n");
	
	double piE = Pow(PI, Exp(1));
	Write($"pi^e = {piE}\n");

	WriteLine($"gamma(1)        = {sfuns.gamma(1)}");
	WriteLine($"expected result = 1 [(1-1)!]");
	
	WriteLine($"gamma(2)        = {sfuns.gamma(2)}");
	WriteLine($"expected result = 1 [(2-1)!]");
	
	WriteLine($"gamma(3)        = {sfuns.gamma(3)}");
	WriteLine($"expected result = 2 [(3-1)!]");
	
	WriteLine($"gamma(10)        = {sfuns.gamma(10)}");
	WriteLine($"expected result = 362880 [(10-1)!]");
	
	WriteLine($"lngamma(10)      = {sfuns.gamma(10)}");
	WriteLine($"expected result  = {Log(362880)} [ln((10-1)!)]");

	WriteLine($"lngamma(-1) = {sfuns.lngamma(-1)}");
	
	}
}
