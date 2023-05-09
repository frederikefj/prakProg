using System;
using static System.Console;
using static System.Math;

public static class main{
	public static void Main(){

	WriteLine("Testing the cmath library:");
	WriteLine("");	

	complex m1 = new complex(-1);
	complex i = new complex(0, 1);
	

	WriteLine($"sqrt(-1)       = {cmath.sqrt(m1)}");
	WriteLine($"expected result: {i}");
	WriteLine($"Aproximatly equal: {i.approx(cmath.sqrt(m1))}");
	WriteLine("");	

	WriteLine($"sqrt(i)        = {cmath.sqrt(i)}");
	complex r2 = new complex(1/Sqrt(2), 1/Sqrt(2));
	WriteLine($"expected result: {r2}");
	WriteLine($"Aproximatly equal: {r2.approx(cmath.sqrt(i))}");
	WriteLine("");	
	
	WriteLine($"exp(i)         = {cmath.exp(i)}");
	complex r3 = new complex(Cos(1), Sin(1));
	WriteLine($"expected result: {r3}");
	WriteLine($"Aproximatly equal: {r3.approx(cmath.exp(i))}");
	WriteLine("");

	WriteLine($"exp(iπ)        = {cmath.exp(i*PI)}");
	complex r4 = new complex(-1, 0);
	WriteLine($"expected result: {r4}");
	WriteLine($"Aproximatly equal: {r4.approx(cmath.exp(i*PI))}");
	WriteLine("");	

	WriteLine($"i^i            = {cmath.pow(i, i)}");
	complex r5 = new complex(Exp(-PI/2), 0);
	WriteLine($"expected result: {r5}");
	WriteLine($"Aproximatly equal: {r5.approx(cmath.pow(i,i))}");
	WriteLine("");	
	
	WriteLine($"log(i)       = {cmath.log(i)}");
	complex r6 = new complex(0, PI/2);
	WriteLine($"expected result: {r6}");
	WriteLine($"Aproximatly equal: {r6.approx(cmath.log(i))}");
	WriteLine("");

	WriteLine($"sin(PI*i)      = {cmath.sin(i*PI)}");
	complex r7 = new complex(0, (Exp(PI)-Exp(-PI))/2);
	WriteLine($"expected result: {r7}");
	WriteLine($"Aproximatly equal: {r7.approx(cmath.sin(i*PI))}");
	WriteLine("");

	}
}
