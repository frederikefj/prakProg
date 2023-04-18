using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.IO;


public static class MonteCarlo{

public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N) {
	int dim = a.size; double V=1; for(int i=0; i<dim; i++) V*=b[i]-a[i];
	double sum=0, sum2=0;
	var x=new vector(dim);
	Random rand = new Random();
	for(int i=0; i<N; i++) {
		for(int k=0; k<dim; k++) x[k] = a[k] + rand.NextDouble()*(b[k]-a[k]);
		double fx=f(x); sum+=fx; sum2+=fx*fx;
		}
	double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
	var result = (mean*V, sigma*V/Sqrt(N));
	return result;

	}

static double corput(int n, int b) {
	double q=0, bk = 1.0/b;
	while(n>0){q+= (n % b)*bk; n /= b; bk /= b;}
	return q;
	}

static void halton(int n, int startBase, int d, vector x) {
	int[] bases = {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67};
	int maxd=bases.Length; if(d >= maxd) throw new Exception($"d={d} is larger than {bases.Length}, add more primes to bases.");
	for(int i=startBase; i<d+startBase;i++) x[i-startBase]=corput(n, bases[i]); 
	}

public static (double, double) quasiInteg(Func<vector,double> f, vector a, vector b, int N) {
	double result1 = 0;
	double result2 = 0;
	int dim = a.size;
	double V = 1; for(int i=0; i<dim; i++) V *= b[i]-a[i];
	vector x = new vector(dim);
	for(int i=0; i<N; i++){
		halton(i, 0, dim, x);
		result1 += 1.0/N*f(x);
		halton(i, dim, dim, x);
		result2 += 1.0/N*f(x);
		}
	
	double mean = 0.5*(result1 + result2);
	double sigma = Sqrt((Pow(result1-mean,2)+Pow(result2-mean,2))/2);
	return (V*mean, V*sigma/Sqrt(2));
	}
}
