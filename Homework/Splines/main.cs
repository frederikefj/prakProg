using System;
using static System.Console;
using static System.Math;
using System.IO;
/*
public static class Spline{

public static int binsearch(double[] x, double z) {
	if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: z outside range of bins");
	int i=0, j=x.Length-1;
	while (j-i>1) {
		int mid=(i+j)/2; //returns an int if (i+j)/2 is a fraction
		if(z>x[mid]) i = mid; else j=mid;
		}
	return i;
	}

public static double linterp(double[] x, double[] y, double z) {
	int i = binsearch(x, z);
	double dx = x[i+1] - x[i]; if(!(dx>0)) throw new Exception($"linterp: x[{i}] and x[{i+1}] are to close (0)");
	double dy = y[i+1] - y[i];
	return y[i] + dy/dx*(z-x[i]);
	}

public static double linterpInteg(double[] x, double[] y, double z) {
	int i = binsearch(x, z);
	double integral = 0;
	int j = 0;
	if(x.Length-1==i) j = i; else j = i-1;

	for(int k=0; k<=j; k++) {
		double yMid = (y[k+1] + y[k])/2;
		double xWidth = x[k+1] - x[k];
		integral += yMid*xWidth;
		}

	if(x.Length-1!=i) {
		double xWidth = z - x[i];
		double dx = x[i+1] - x[i];
	       	if(!(dx>0)) throw new Exception($"linterpInteg: x[{i}] and x[{i+1}] are to close (0)");
		double dy = y[i+1] - y[i];
		integral += xWidth*(y[i] + 0.5*dy/dx*xWidth);
		}
	return integral;

	}

public class qspline {
	vector x, y, b, c;
	public qspline(vector xs, vector ys) {
		x = xs.copy(); y = ys.copy();
		int m = xs.size-1;
		vector p = new vector(m);
		c = new vector(m);
		b = new vector(m);
		for(int i=0; i<m; i++) {
			double dy = y[i+1]-y[i];
			double dx = x[i+1]-x[i];
			if(!(dx>0)) throw new Exception($"linterpInteg: x[{i}] and x[{i+1}] are to close (0)");
			p[i] = dy/dx;
			}
		c[0] = 0;
		for(int i=1; i<m; i++) { //Forward recursion with c[0] = 0
			c[i] = 1.0/(x[i+1]-x[i])*(p[i]-p[i-1]-c[i-1]*(x[i]-x[i-1]));
			}
		c[m-1] = 0.5*c[m-1];
		for(int i=m-2; i>=0; i--) { //backwards recursion with c[m-1] = 0.5*c[m-1]
			c[i] = 1.0/(x[i+1]-x[i])*(p[i+1]-p[i]-c[i+1]*(x[i+2]-x[i+1]));
			}

		for(int i=0; i<m; i++) b[i] = p[i] - c[i]*(x[i+1]-x[i]);
		}

	public double evaluate(double z) {
		int i = binsearch(x, z);
		return y[i] + b[i]*(z-x[i]) + c[i]*(z-x[i])*(z-x[i]);
		}
	
	public double derivative(double z) {	
		int i = binsearch(x, z);
		return b[i] + 2*c[i]*(z-x[i]);
		}
	
	public double integral(double z) {
		int i = binsearch(x, z);
		double integ = 0;
		for(int j=0; j<i; j++) {
			integ += y[j]*(x[j+1]-x[j]) + b[j]/2*Pow((x[j+1]-x[j]),2)+c[j]/3*Pow((x[j+1]-x[j]),3);
			}	
		integ += y[i]*(z-x[i]) + b[i]/2*Pow((z-x[i]),2)+c[i]/3*Pow((z-x[i]),3);
		return integ;
		}

	public void printbc() {
		string bPrint = $"b = {{{b[0]}";
		for(int i=1; i<b.size; i++) bPrint += $", {b[i]}";
		bPrint += $"}}";
		WriteLine(bPrint);

		string cPrint = $"c = {{{c[0]}";
		for(int i=1; i<c.size; i++) cPrint += $", {c[i]}";
		cPrint += $"}}";
		WriteLine(cPrint);
		}
	}

}
*/

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
