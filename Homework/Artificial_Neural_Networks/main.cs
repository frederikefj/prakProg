using System;
using static System.Console;
using static System.Math;
using System.IO;

class ann{
int n;
Func<double, double> f = x => x*Exp(-x*x);
public vector p;
public ann(int ni) {
	n = ni;
	p = new vector(3*n);
	for(int i = 0; i<n; i++) {
		p[i]=0;
		p[i+n] = 0.5;
		p[i+2*n] = 0.2;
		}
	}

public double response(double x) {
	double res = 0;
	for(int i=0; i<n; i++) {
		res+=f((x-p[i])/p[i+n])*p[i+2*n];
		}
	return res;
	}

public double derivative(double x) {
	double de = 0;
	for(int i=0; i<n; i++) {
		
		}
	return de;
	}

vector x; vector y; int N;
double cost(vector pi) {
	p = pi.copy();
	double sum = 0;
	for(int i=0; i<N; i++) {
		sum += Pow(response(x[i])-y[i], 2);
		}
	return sum;
	}

public void train(vector xi, vector yi) {
	x = xi.copy(); y = yi.copy(); N = x.size;	
	vector pmin = Mini.qnewton(cost, p.copy());
	p = pmin;
	}

public void trainG(vector xi, vector yi, double time) {
	x = xi.copy(); y = yi.copy(); N = x.size;	
	vector pmin = Mini.qnewton(cost, p.copy());
	p = pmin;
}

public void gwaveletP0(vector xi, vector yi) {	
	x = xi.copy(); y = yi.copy(); N = x.size;
	
		// Choseing good starting conditions
	vector p0 = new vector(3*n);
	double a = x[0]; double b = x[N-1];
	for(int i=0; i<N; i++) {
		if(x[i]<a) a=x[i]; if(x[i]>b) b=x[i];
		}
	for(int i=0; i<n; i++) {
		double ii = i;
		p0[i] = (b-a)*(ii/n+0.5/n)+a; // Spreading the guassians equaly
		p0[i+n] = (b-a)/(2*n); // Spread set to slightly larger than area around center
		p0[i+2*n] = 0; 
		}
	p = p0.copy();
	}

public void gaussP0(vector xi, vector yi) {
	x = xi.copy(); y = yi.copy(); N = x.size;
	
		// Choseing good starting conditions
	vector p0 = new vector(3*n);
	double a = x[0]; double b = x[N-1];
	for(int i=0; i<N; i++) {
		if(x[i]<a) a=x[i]; if(x[i]>b) b=x[i];
		}
	for(int i=0; i<n; i++) {
		double ii = i;
		p0[i] = (b-a)*(ii/n+0.5/n)+a; // Spreading the guassians equaly
		p0[i+n] = (b-a)/(2*n); // Spread set to slightly larger than area around center
		p0[i+2*n] = 0; 
		int Ni = 0;
			// Setting guassian top to avarge of points around center
		for(int j=0; j<N; j++) {
			if(Abs(x[j]-p0[i])<=(b-a)/(2*n)) {
				p0[i+2*n] += y[j];
				Ni++;
				}
			}
		p0[i+2*n] /= Ni;
		}
	p = p0.copy();

	}

}

class main{

public static double g(double x) {
	return Cos(5*x-1)*Exp(-x*x);
	//return Cos(x*x+3)+Sqrt(Abs(x));
	}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start


int n = 9;
WriteLine($"Fitting to the function g with a neural network with {n} neurons.");
WriteLine("  g(x) = cos(5x-1)exp(-x^2) for x in [-1,1]");
var ann1 = new ann(n);

int N = 100;
double a = -1; double b = 1;
vector x0 = new vector(N);
vector y0 = new vector(N);
for(int i=0; i<N; i++) {
	x0[i] = i*(b-a)/(N-1)+a;
	y0[i] = g(x0[i]);
	}

ann1.gwaveletP0(x0, y0);

string toWriteData = $"";
string toWriteGuess = $"";
int M = 200;
for(int i=0; i<M; i++) {
	double xi = (b-a)*i/(M-1)+a;
	toWriteData += $"{xi}\t{g(xi)}\n";
	toWriteGuess += $"{xi}\t{ann1.response(xi)}\n";
	}
File.WriteAllText("out.ann1Data.data", toWriteData);
File.WriteAllText("out.ann1Guess.data", toWriteGuess);

ann1.train(x0, y0);

string toWriteFit = $"";
for(int i=0; i<M; i++) {
	double xi = (b-a)*i/(M-1)+a;
	toWriteFit += $"{xi}\t{ann1.response(xi)}\n";
	}

File.WriteAllText("out.ann1Fit.data", toWriteFit);

WriteLine("Results shown in \"ann1.svg\"");

//ann1.p.print();

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start



// Opgave B end
WriteLine("");
}
}
