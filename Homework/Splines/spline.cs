using System;
using static System.Console;
using static System.Math;
using System.IO;

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

