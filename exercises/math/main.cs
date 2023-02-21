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

	Write($"gamma(1) = {sfuns.gamma(1)}\n");
	Write($"gamma(11) = {sfuns.gamma(11)}\n");
	
	}
}
