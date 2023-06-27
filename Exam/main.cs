using System;
using static System.Console;
using static System.Math;
using System.IO;

public class globalMini {

public static vector quasiRandomMin(Func<vector,
	       	double> f,
	       	vector a,
	       	vector b,
		double time = 10,
		double acc = 0.001,
		string localMinimizer = "qnewton") {
	int d = a.size; if(b.size != d) throw new ArgumentException($"a(dim: {a.size}) and b(dim: {b.size}) does not have the same dimension");
	vector x = new vector(d);
	vector x0 = 0.5*(a+b);
	double fmin = f(x0);

	var t0 = DateTime.UtcNow;
	int i = 0;
	while(DateTime.UtcNow - t0 < TimeSpan.FromSeconds(time)) {
		qrand.halton(i, 0, d, x);
		for(int j=0; j<d; j++) {x[j]=x[j]*(b[j]-a[j])+a[j];}
		double fi = f(x);
		if(fi<fmin) {fmin = fi; x0 = x.copy();}
		i++;
		}

	vector min = new vector(d);	
	if(localMinimizer == "qnewton") {
		min = Mini.qnewton(f, x0, acc);
	}
	if(localMinimizer == "simplex") {
		var random = new Random();
		matrix P = new matrix(d, d+1);
		// randomly distributes d+1 points around the current global minimum (x0)
	       	for(int j=0; j<d+1; j++) {
			for(int k=0; k<d; k++) {
				double r = random.NextDouble();
				P[j][k] = (b[k]-a[k])/Pow(i, 1.0/d)*(2*r-1)/2 + x0[k];
			}
		}
		P.print();
		min = Mini.simplex(f, P, acc);
	}
	return min;
	}

} // globalMini class: end

class main{

public static int f1N = 0;
public static double f1(vector x) {
	int n = x.size;
	double fx = 0;
	for(int i = 0; i<n; i++) {
		for(int j=1; j<i+2; j++) {
			fx+=-j*Exp(-8*Pow(x[i]-j,2) );
			}
		}
	f1N++;
	return fx;}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");

// Opgave A start
vector a1 = new vector(4);
vector b1 = a1.copy();
b1[0] = 5; b1[1] = 5; b1[2] = 5; b1[3] = 5;
double time1 = 1;

WriteLine("For a simple test of the global optimizer a sum of narrow guassian functions is used.");
WriteLine("The negatice gaussian functions are narrow enough that the global minimum is about");
WriteLine("at the \"peak\" of the largest guassian in each dimension. x: (1, 2, 3, 4)");
WriteLine("");

vector min1 = globalMini.quasiRandomMin(f1, a1, b1, time1, 1e-6, localMinimizer: "simplex");
WriteLine($"The function is sampled for {time1} seconds and the quasi-Newton minimizer");
WriteLine($"was given an accuarcy of 1e-3.");
WriteLine($"Minimum is found at x: ({min1[0]},{min1[1]}, {min1[2]}, {min1[3]})");
WriteLine($"");


var points1 = new points(2, 50);
vector a2 = new vector(2);
vector b2 = a2.copy();
b2[0] = 1; b2[1] = 1;
double time2 = 1;
WriteLine("Another function with a large number of local minimum is minus");
WriteLine("the distance from the nearest point in a group of random numbers");
WriteLine("For points y1->yn it would be f(x)=-max|x-yi| for x in [0,1]^2");
WriteLine("");

vector min2 = globalMini.quasiRandomMin(points1.distance, a2, b2, time2);
WriteLine($"The function is sampled for {time2} seconds and the quasi-Newton minimizer");
WriteLine($"was given an accuarcy of 1e-3.");
WriteLine($"Minimum is found at x: ({min2[0]},{min2[1]})");
WriteLine($"A plot of the points is shown in \"points.svg\"");


// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start


// Opgave B end
WriteLine("");
}
}
