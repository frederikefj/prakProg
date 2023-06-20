using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{

public static vector f1(vector x) {
	vector f1x = new vector(2);
	f1x[0] = 2*(x[0]-1) + 400*(x[0]*x[0]-x[1])*x[0];
	f1x[1] = 200*(x[1]-x[0]*x[0]);
	return f1x;
	}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

vector xs = new vector(2);
xs[0] = -8; xs[1] = 10;

WriteLine("Finding the minimum of Rosenbrock's valley function:");
WriteLine("   f(x,y) = (1-x)^2 + 100(y-x^2)^2");
WriteLine("By finding the root of its gradient");
WriteLine($"starting x: ({xs[0]}, {xs[1]}), starting f: ({f1(xs)[0]}, {f1(xs)[1]})");
vector xmin = Roots.newton(f1, xs);
WriteLine("");
xmin.print();

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B star


// Opgave B end
WriteLine("");

}
} 
