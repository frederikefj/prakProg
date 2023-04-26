using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{
	// u''=-u is converted to v=u' ->  (u',v') = (v, -u)
	public static vector f1(double x, vector y) {
		vector dydx = new vector(2);
		dydx[0] = y[1];
		dydx[1] = -y[0];
		return dydx;
		}

	public static vector f0(double x, vector y) {
		vector dydx = new vector(2);
		dydx[0] = y[1];
		dydx[1] = -0.25*y[1]-5*Sin(y[0]);
		return dydx;	
		}

	public static vector fp(double x, vector y, double e) {
		vector dydx = new vector(2);
		dydx[0] = y[1];
		dydx[1] = 1 - y[0] + e*y[0]*y[0];
		return dydx;
		}

	public static vector fp0(double x, vector y) {return fp(x, y, 0.0);}
	public static vector fp001(double x, vector y) {return fp(x, y, 0.01);}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

double x1 = 0;
double[] y1I = {1,0};
double h = 0.1;
vector y1 = new vector(y1I);
vector dydx = f1(x1, y1);
WriteLine("Testing the RK12 stepper:");
WriteLine($"y={{{y1[0]},{y1[1]}}}, dydx={{{dydx[0]},{dydx[1]}}}");
(vector y2, vector Ey2) = ODE.rkstep12(f1, x1, y1, h);
WriteLine($"Taking a step of size {h} from y to y2={{{y2[0]},{y2[1]}}}");
WriteLine($"The errors are Ey2={{{Ey2[0]},{Ey2[1]}}}");
WriteLine($"");

WriteLine($"Running the driver with RK12 method on u''=-u from 0 to 10");
WriteLine($"Like before y0 = {{{y1[0]},{y1[1]}}}");
(genlist<double> x, genlist<vector> y) = ODE.driver(f1, x1, y1, 10);

string toWrite = $"";
int n = x.size;
for(int i=0; i<n; i++) {
	toWrite += $"{x[i]}\t{y[i][0]}\n";
	}
File.WriteAllText("out.f1y.data", toWrite);

WriteLine("A graf of y(x) is shown in Aplot.svg");
WriteLine("The graf should follow a cos(x) curve which it does.");
WriteLine("");
WriteLine("To further test the program an example from scipy.integrate's documentation is reproduced");
WriteLine("A pendulum with friction is descriped by the ODE:");
WriteLine("θ''=-b*θ' - c*sin(θ)");
WriteLine("Using b=0.25, c=5 and starting the pendulum at θ'=0, θ=π-0.1");

double x0S = 0;
double[] y0SI = {PI-0.1,0};
double b0 = 10;
vector y0S = new vector(y0SI);
(genlist<double> x0, genlist<vector> y0) = ODE.driver(f0, x0S, y0S, b0); 

string toWrite0 = $"";
string toWrite0D = $"";
int n0 = x0.size;
for(int i=0; i<n0; i++) {
	toWrite0 += $"{x0[i]}\t{y0[i][0]}\n";
	toWrite0D += $"{x0[i]}\t{y0[i][1]}\n";
	}

File.WriteAllText("out.ODE.data", toWrite0);
File.WriteAllText("out.ODED.data", toWrite0D);

WriteLine("Solution from 0 to 10 shown in the plot \"ODE.svg\"");
WriteLine("The plot looks like the one from scipy's documentation");

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start

WriteLine("Driver2 takes a genlist of x (optional) and only gives y at those values.");
WriteLine("Driver2 also investigates the tolorance/error seperatly for each component of y.");
WriteLine("First driver2 is tested using the ODE from A u''=-0.25u'-5sin(u):");

genlist<double> xlist = new genlist<double>();
genlist<vector> ylist = new genlist<vector>();

for(int i=0; i<=10; i++) xlist.add(1.0*i);

vector final_y = ODE.driver2(f0, x0S, y0S, b0, xlist: xlist, ylist: ylist);

string toWrite3 = $"";
for(int i=0; i<xlist.size; i++) {
	toWrite3 += $"{xlist[i]}\t{ylist[i][0]}\n";
	}
File.WriteAllText("out.ODE2.data", toWrite3);

WriteLine($"final at x=10 y={final_y}");
WriteLine($"The values at 0,1,...,10 are plotted in \"ODE2.svg\" on top of the solution found in A.");
WriteLine("");
WriteLine($"The driver is also tested on the equation of equatorial motion of a planet around a star:");
WriteLine("    u''(φ) + u(φ) = 1 + εu(φ)^2");
WriteLine("");
Write("Where u is 1/r, r is radius, φ is the angle, ε is the relativistic correction");

double bp = 3*2*PI;
double x0p = 0;
double[] y0piI = {1, 0};
vector y0pi = new vector(y0piI); // y0, function p, exercise B i
double[] y0piiI = {1, -0.5};
vector y0pii = new vector(y0piiI); // y0, function p, exercise B ii and iii
				   //
genlist<double> xlistp = new genlist<double>();
double np = 300;
for(int i=0; i<=np; i++) xlistp.add(bp/np*i);
genlist<vector> ylistp1 = new genlist<vector>();
genlist<vector> ylistp2 = new genlist<vector>();
genlist<vector> ylistp3 = new genlist<vector>();

vector ynp1 = ODE.driver2(fp0, x0p, y0pi, bp, xlist: xlistp, ylist: ylistp1);
vector ynp2 = ODE.driver2(fp0, x0p, y0pii, bp, xlist: xlistp, ylist: ylistp2);
vector ynp3 = ODE.driver2(fp001, x0p, y0pii, bp, xlist: xlistp, ylist: ylistp3);

string toWritep1 = $"";
string toWritep2 = $"";
string toWritep3 = $"";
for(int i=0; i<xlistp.size; i++) {
	toWritep1 += $"{xlistp[i]}\t{ylistp1[i][0]}\n";
	toWritep2 += $"{xlistp[i]}\t{ylistp2[i][0]}\n";
	toWritep3 += $"{xlistp[i]}\t{ylistp3[i][0]}\n";
	}
File.WriteAllText("out.Newton1.data", toWritep1);
File.WriteAllText("out.Newton2.data", toWritep2);
File.WriteAllText("out.General.data", toWritep3);

WriteLine("The plot \"Newton.svg\" shows 2 classical orbits (ε=0) while the plot \"General.svg\" show an orbit with relativistic effects (ε=0.01).");
WriteLine("3 orbits (φ: 0 -> 6π) are shown in both plots. The newtonian orbits remain constant while the relativistic orbit precesses, which is the expected result.");

WriteLine("");
// Opgave B end
}
}
