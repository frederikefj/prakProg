using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

public static vector f2(vector x) {
	vector f2x = new vector(1);
	f2x[0] = x[0]*x[0]*x[0] + x[0]*x[0] + 1;
	return f2x;
	}

public static vector f0(vector x) {
	vector f0x = new vector(2);
	f0x[0] = 2*x[0]*Exp(x[0]*x[0]+x[1]*x[1]);
	f0x[1] = 2*x[1]*Exp(x[0]*x[0]+x[1]*x[1]);
	return f0x;
	}

public static vector f1(vector x) {
	vector f1x = new vector(2);
	f1x[0] = 2*(x[0]-1) + 400*(x[0]*x[0]-x[1])*x[0];
	f1x[1] = 200*(x[1]-x[0]*x[0]);
	return f1x;
	}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

WriteLine("Testing newton's method with back-tracking linesearch on some simple functions");

WriteLine("");
WriteLine("Finding the root of x^3 + x^2 + 1 starting on the other side of it's local minimum (at x=0)");
vector x2 = new vector(1);
x2[0] =  1;
WriteLine($"starting x: {x2[0]}, starting f: {f2(x2)[0]}");
(vector xmin2, int fCall2) = Roots.newton(f2, x2);
WriteLine($"Number of function evaluations: {fCall2}");
WriteLine($"root: {xmin2[0]}");
WriteLine($"expected result: (-1.4656)");

WriteLine("");
WriteLine("Another simple test could be finding the minimum of exp(x^2+y^2) using the roots of it's gradiant");
vector x0 = new vector(2);
x0[0] =  2; x0[1] = -4;
WriteLine($"starting x: ({x0[0]}, {x0[1]}), starting f: ({f0(x0)[0]}, {f0(x0)[1]})");
(vector xmin0, int fCall0) = Roots.newton(f0, x0);
WriteLine($"Number of function evaluations: {fCall0}");
WriteLine($"root: ({xmin0[0]}, {xmin0[1]})");
WriteLine($"expected result: (0, 0)");

WriteLine("");
vector xs = new vector(2);
xs[0] = -1; xs[1] = 1;
WriteLine("Finding the minimum of Rosenbrock's valley function:");
WriteLine("   f(x,y) = (1-x)^2 + 100(y-x^2)^2");
WriteLine("By finding the root of its gradient");
WriteLine($"starting x: ({xs[0]}, {xs[1]}), starting f: ({f1(xs)[0]}, {f1(xs)[1]})");
(vector xmin, int fCall) = Roots.newton(f1, xs);
WriteLine($"Number of function evaluations: {fCall}");
WriteLine($"root: ({xmin[0]}, {xmin[1]})");
WriteLine($"expected result: (1, 1)");

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B star


// Opgave B end
WriteLine("");

}
} 
