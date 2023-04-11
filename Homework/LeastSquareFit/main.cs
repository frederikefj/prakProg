using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class Olsfit{
public static (vector, matrix) lsfit(Func<double, double>[] fs, vector x, vector y, vector dy) {
	int n = x.size;
	int m = fs.Length;
	matrix A = new matrix(n, m);
	vector b = new vector(n);
	for (int i=0; i<n; i++) {
		b[i] = y[i]/dy[i];
		for (int j=0; j<m; j++){
			A[j][i] = fs[j](x[i])/dy[i];
			}
		}
	matrix Q = A.copy();
	matrix R = new matrix(m, m);
	QRGS.decomb(Q, R);
	var QTb = Q.transpose()*b;
	var s = QRGS.solveR(R, QTb);

	var inR = new matrix(m, m);
	for (int i=0; i<m; i++) {
		vector i_unit = new vector(m);
		i_unit[i] = 1;
		vector i_col = QRGS.solveR(R, i_unit);
		inR[i] += i_col;
		}
	
	var sigma = inR*inR.transpose();
	return (s, sigma);
	}
}

public static class main{

public static double f1(double x) {return 1.0;}
public static double f2(double x) {return x;}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

double[] Atime = {1, 2, 3, 4, 6, 9, 10, 13, 15};
vector time = new vector(Atime);
double[] Aactivity = {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1};
vector activity = new vector(Aactivity);
double[] AactivityError = {5, 5, 5, 4, 4, 3, 3, 2, 2};
vector activityError = new vector(AactivityError);
vector lnA = new vector(activity.size);
vector lnAError = new vector(activity.size);
for (int i = 0; i<activity.size; i++) {
	lnA[i] = Log(activity[i]);
	lnAError[i] = activityError[i]/activity[i];
	}
Func<double, double>[] fs = {f1, f2};

var Q = Olsfit.lsfit(fs, time, lnA, lnAError);
vector c = Q.Item1;
matrix sigma = Q.Item2;

string toWrite = $"";
int plotPoints = 100;
for (int i=0; i<plotPoints; i++) {
	double t = i*time[time.size-1]/plotPoints;
	double ft = Exp(c[0])*Exp(c[1]*t);
	toWrite += $"{t}\t{ft}\n";
	}
File.WriteAllText("out.fit.data", toWrite);

WriteLine("Solution to Ac=b, c:");
c.print();
double tHalf = Log(2)/(-c[1]);
WriteLine($"\nHalflife of Ra224 from given data: {tHalf} d");
WriteLine($"Halflife of Ra224 from \"nucleardata.nuclear.lu.se\": 3.66(4) d");
WriteLine("Plot of data with fit is shown in decayFit.svg");


// Opgave A end

WriteLine("\n###############[ Opgave B ]###############\n");

WriteLine("Covariance matrix for c:");
sigma.print();
double lambdaError = Sqrt(sigma[1][1]);
double tHalfError = lambdaError*Log(2)/Pow(-c[1],2);
WriteLine($"\nHalflife of Ra224 from data: {tHalf} +- {tHalfError}");
WriteLine("The modern value is not within the esimated uncertainties, (but at least its on the right scale)");
}
}
