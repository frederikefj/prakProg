using System;
using static System.Console;
using static System.Math;
using System.IO;

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

WriteLine("\n###############[ A ]###############\n");
// A start

WriteLine("the global optimizer have been implemented in optimization.cs\n");

WriteLine("The function globalMini.quasiRandomMin takes a function, a square");
WriteLine("area and a time. The function uses quasirandom numbers to look for a");
WriteLine("global minimum for the given time and then uses the quasi-random ");
WriteLine("minimizer to find the local minimum around that.\n");

vector a1 = new vector(4);
vector b1 = a1.copy();
b1[0] = 5; b1[1] = 5; b1[2] = 5; b1[3] = 5;
double time1 = 1;

WriteLine("For a simple test of the global optimizer a sum of narrow guassian functions is used.");
WriteLine("The negatice gaussian functions are narrow enough that the global minimum is about");
WriteLine("at the \"peak\" of the largest guassian in each dimension. x: (1, 2, 3, 4)");
WriteLine("");

vector min1 = globalMini.quasiRandomMin(f1, a1, b1, time1, 1e-6);
WriteLine($"The function is sampled for {time1} seconds and the quasi-Newton minimizer");
WriteLine($"was given an accuarcy of 1e-6. ");
WriteLine($"Minimum is found at x: ({min1[0]},{min1[1]}, {min1[2]}, {min1[3]})");
WriteLine($"(The minimum is not excactly at (1,2,3,4) since the guassian functions overlap)");
WriteLine($"");

// A end
WriteLine("");

WriteLine("\n###############[ B ]###############\n");
// B start

WriteLine("An option to use the downhill simplex algoritm as a local minimizer has been added\n");

var points2 = new points(2, 400);
vector a2 = new vector(2);
vector b2 = a2.copy();
b2[0] = 1; b2[1] = 1;
double time2 = 5;
WriteLine("Another function with a large number of local minimum is minus");
WriteLine("the distance from the nearest point (or wall)in a group of random");
WriteLine("numbers");

vector min2 = globalMini.quasiRandomMin(points2.distance, a2, b2, time2, 1e-6, localMinimizer: "simplex");
WriteLine($"The function is sampled for {time2} seconds and the downhill simplex minimizer");
WriteLine($"was given an accuarcy of 1e-6.");
WriteLine($"Minimum is found at x: ({min2[0]},{min2[1]})");
WriteLine($"where f = {points2.distance(min2)}");
WriteLine($"A plot of the points and minimum is shown in \"points.svg\"");

// plot of data
string toWrite2 = "";
for(int i=0; i<points2.n; i++) {
	toWrite2 += $"{points2.x[i][0]}\t{points2.x[i][1]}\n";
}
File.WriteAllText("out.points2.data", toWrite2);
File.WriteAllText("out.points2min.data", $"{min2[0]}\t{min2[1]}");

// circle around minimum
int nCircle = 100;
string toWriteCircle = "";
double maxDist = -points2.distance(min2);
for(int i=0; i<nCircle; i++) {
	double θ = i*2*PI/(nCircle-1);
	toWriteCircle += $"{min2[0]+maxDist*Cos(θ)}\t{min2[1]+maxDist*Sin(θ)}\n";
}

File.WriteAllText("out.points2circle.data", toWriteCircle);


// B end
WriteLine("");
}
}
