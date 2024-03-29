using System;
using static System.Console;
using static System.Math;

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
} // qrand class end

public class points{
public matrix x;
public int n;
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

// Finding the farthest distance from other points and the sides
public double distance(vector a) {
	if(a.size != x[0].size) throw new ArgumentException($"points.dist: vector(d={a.size}) does not match dimension of points(d={x[0].size})");
	double minDist = (x[0]-a).norm();
	if(a[0] < 0 | a[0] > 1 | a[1]<0 | a[1]>1) {return 0;}
	for(int i=1; i<n; i++) {
		double iDist = (x[i]-a).norm();
		if(iDist < minDist) minDist = iDist; 
		}
	if(a[0] < minDist) minDist = a[0];
	if(1-a[0] < minDist) minDist = 1-a[0];
	if(a[1] < minDist) minDist = a[1];
	if(1-a[1] < minDist) minDist = 1-a[1];
	return -minDist;
	}

} // points class: end
