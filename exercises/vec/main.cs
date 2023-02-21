using System;
using static System.Console;
using static System.Math;

public static class main{
	public static void Main(){
		var a = new vec(1.0,2.0,3.0);
		var b = 3*a-a*5;
		a.print("a = ");
		b.print("b = ");
		WriteLine($" using ToString a = {a}");
		double adotb = a.dot(b);
		double dotbb = vec.dot(b,b);
		WriteLine($"a.dot(b) = {adotb}");
		WriteLine($"dot(b,b) = {dotbb}");
		WriteLine($"is a=b: {vec.approx(a,b)}, is b=b: {b.approx(b)}");
	}
}
