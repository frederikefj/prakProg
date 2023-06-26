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
	       	double acc = 0.001) {
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
	

	vector min = Mini.qnewton(f, x0, acc);
	return min;
	}

} // globalMini class: end


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

class ann{
int n;
Func<double, double> f = x => Exp(-x*x);
vector p;
public ann(int ni) {
	n = ni;
	p = new vector(3*n);
	for(int i = 0; i<n; i++) {
		p[i] = 2*i/n-1;
		p[i+n] = 1;
		p[i+2*n] = 0;
		}
	}

public double response(double x) {
	double res = 0;
	for(int i=0; i<n; i++) {
		res+=f((x-p[i])/p[i+n])*p[i+2*n];
		}
	return res;
	}

vector x; vector y;
double cost(vector pi) {
	p = pi.copy();
	int N = x.size;
	double sum = 0;
	for(int i=0; i<N; i++) {
		sum += Pow(response(x[i])-y[i], 2);
		}
	return sum;
	}

public void train(vector xi, vector yi) {
	x = xi.copy(); y = yi.copy();
	vector pmin = Mini.qnewton(cost, p.copy());
	/*
	vector a = new vector(3*n);
	vector b = new vector(3*n);
	for(int i=0; i<n; i++) {
		a[i] = -2; b[i] = -2;
		a[i+n] = 0; b[i+n] = 10;
		a[i+2*n] = 0; b[i+2*n] = 2;
		}
	vector pmin = globalMini.quasiRandomMin(cost, a, b, 20);
	*/
	p = pmin;
	}
}

class main{

public static double g(double x) {return Cos(5*x-1)*Exp(-x*x);}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

var ann1 = new ann(6);
WriteLine($"ann1(2): {ann1.response(2)}");

int N = 100;
double a = -1; double b = 1;
vector x0 = new vector(N+1);
vector y0 = new vector(N+1);
for(int i=0; i<N+1; i++) {
	x0[i] = i*(b-a)/N+a;
	y0[i] = g(x0[i]);
	}
ann1.train(x0, y0);

for(int i=0; i<N; i++) {
	WriteLine($"x={x0[i]}, g(x)={g(x0[i])}, r(x)={ann1.response(x0[i])}");
}

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start



// Opgave B end
WriteLine("");
}
}
