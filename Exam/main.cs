using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class qrand{
static double corput(int n, int b) {
	double q=0, bk = 1.0/b;
	while(n>0){q+= (n % b)*bk; n /= b; bk /= b;}
	return q;
	}

public static void halton(int n, int startBase, int d, vector x) {
	int[] bases = {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67};
	int maxd=bases.Length; if(d >= maxd) throw new Exception($"d={d} is larger than {bases.Length}, add more primes to bases.");
	for(int i=startBase; i<d+startBase;i++) x[i-startBase]=corput(n, bases[i]); 
	}
}

public class points{
matrix x;
int n;
public points(int d,int n0, int startBase = -1) {
	n = n0;
	x = new matrix(d,n);
	if(startBase==-1) {
		var random = new Random();
		for(int i=0; i<n; i++) {
			for(int j=0; j<d; j++) {
				x[i][j] = random.NextDouble();
			}
		}
	}
	else {
		for(int i=0; i<n; i++) {
			qrand.halton(i+1, startBase, d, x[i]);
			}
	
	}
}

public double distance(vector a) {
	if(a.size != x[0].size) throw new ArgumentException($"points.dist: vector(d={a.size}) does not match dimension of points(d={x[0].size})");
	double minDist = (x[0]-a).norm();
	for(int i=1; i<n; i++) {
		double iDist = (x[i]-a).norm();
		if(iDist < minDist) minDist = iDist; 
		}	
	return minDist;
	}
		
}

public static class main{
public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start
var points1 = new points(2, 30, startBase:0);
vector a = new vector(2);
double da = points1.distance(a);
a.print();
WriteLine($"distance from a to closest point: {da}");
WriteLine($"{System.Time}");

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start


// Opgave B end
WriteLine("");
}
}
