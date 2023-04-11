using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{
public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

double[] x = {0, 1, 2, 3, 6, 9, 10, 13, 15, 18, 19};
double[] y = {0, 10, -20, -25, -5, 30, 40, 5, 10, -5, -50};

WriteLine("using test points:");
Write($"y = "); for(int i=0; i<x.Length; i++) Write($"{x[i]} ");
Write($"\ny = "); for(int i=0; i<y.Length; i++) Write($"{y[i]} ");
Write("\n\n");

double zTest = 7.25;
int iTest = Spline.binsearch(x, zTest);
double fzTest = Spline.linterp(x, y, zTest);
double fzIntegTest = Spline.linterpInteg(x, y, zTest);

WriteLine($"Testing binsearch on z = {zTest}, result = {iTest} | z should be between {x[iTest]} and {x[iTest+1]}");
WriteLine($"Testing linterp with z, f(z) = {fzTest} | f(z) should be between {y[iTest]} and {y[iTest+1]}");
WriteLine($"Testing linterpInteg with z, result = {fzIntegTest}");

// Write data to file

string toWrite = $"";
for (int i=0; i<x.Length; i++) {
	toWrite += $"{x[i]}\t{y[i]}\n";
	}
File.WriteAllText("out.points.data", toWrite);

string toWritefz = $"";
string toWriteInteg = $"";
int plotPoints = 300; //one more point for the endpoints
for (int i=0; i<plotPoints+1; i++) {
	double z = x[0] + i*(x[x.Length-1]-x[0])/plotPoints;
	double fz = Spline.linterp(x,y,z);
	double fzInteg = Spline.linterpInteg(x,y,z);

	toWritefz += $"{z}\t{fz}\n";
	toWriteInteg += $"{z}\t{fzInteg}\n";
	}

File.WriteAllText("out.fz.data", toWritefz);
File.WriteAllText("out.Integ.data", toWriteInteg);

WriteLine("\nPlot of points, interpolation and integration is shown in \"Aplot.svg\"");

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start

vector vx = new vector(x);
vector vy = new vector(y);
var Q = new Spline.qspline(vx, vy);

string toWriteBfz = $"";
string toWriteBInteg = $"";
string toWriteBDiff = $"";
for (int i=0; i<plotPoints+1; i++) {
	double z = x[0] + i*(x[x.Length-1]-x[0])/plotPoints;
	double fz = Q.evaluate(z);
	double fzInteg = Q.integral(z);
	double fzDiff = Q.derivative(z);

	toWriteBfz += $"{z}\t{fz}\n";
	toWriteBInteg += $"{z}\t{fzInteg}\n";
	toWriteBDiff += $"{z}\t{fzDiff}\n";

	}

File.WriteAllText("out.Bfz.data", toWriteBfz);
File.WriteAllText("out.BInteg.data", toWriteBInteg);
File.WriteAllText("out.BDiff.data", toWriteBDiff);

WriteLine("Quadratic interpolation on the same points as in A is shown in \"Bplot.svg\".");
WriteLine("Derivative and Integral is also shown in the plot.");
WriteLine("The condition used to resolve the final degree of freedom is c(1) = c(n-1).");
WriteLine("");
WriteLine("To further test the program it is used on some simple datasets and compared to manually calculated results:");
WriteLine("\nx={1,2,3,4,5}, y={1,1,1,1,1}:");
WriteLine("Manually calculated: b={0,0,0,0}, c={0,0,0,0}");
WriteLine("Results from code:");
double[] x1I = {1,2,3,4,5};
double[] y1I = {1,1,1,1,1};
vector x1 = new vector(x1I);
vector y1 = new vector(y1I);
var Q1 = new Spline.qspline(x1, y1);
Q1.printbc();

WriteLine("\nx={1,2,3,4,5}, y={1,2,3,4,5}:");
WriteLine("Manually calculated: b={1,1,1,1}, c={0,0,0,0}");
WriteLine("Results from code:");
double[] x2I = {1,2,3,4,5};
double[] y2I = {1,2,3,4,5};
vector x2 = new vector(x2I);
vector y2 = new vector(y2I);
var Q2 = new Spline.qspline(x2, y2);
Q2.printbc();

WriteLine("\nx={1,2,3,4,5}, y={1,4,9,16,25}:");
WriteLine("Manually calculated: b={2,4,6,8}, c={1,1,1,1}");
WriteLine("Results from code:");
double[] x3I = {1,2,3,4,5};
double[] y3I = {1,4,9,16,25};
vector x3 = new vector(x3I);
vector y3 = new vector(y3I);
var Q3 = new Spline.qspline(x3, y3);
Q3.printbc();

// Opgave B end
WriteLine("");
}
}
