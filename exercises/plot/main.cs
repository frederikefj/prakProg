using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class main{
static double erf(double x) {
	if(x<0) return -erf(-x);
	double[] a = {0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
	double t=1.0/(1+0.3275911*x);
	double sum = t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));
	return 1-sum*Exp(-x*x);
	}

static double gamma(double x) {
	if(x<0)return PI/Sin(PI*x)/gamma(1-x);
	if(x<9)return gamma(x+1)/x;
	double lngamma = x*Log(x+1.0/(12*x-1.0/x/10)) - x + Log(2*PI/x)/2;
	return Exp(lngamma);
	}

static double lngamma(double x) {
	if(x<=0) throw new ArgumentException("lngamma: x<=0");
	if(x<9) return lngamma(x+1) - Log(x);
	return x*Log(x+1.0/(12*x-1.0/x/10)) - x + Log(2*PI/x)/2;
	}


public static void Main(string[] args){

WriteLine("The error function, gamma function and log-gamma function are plotted along with tabulated values in the plots \"error.svg\", \"gamma.svg\" and \"lngamma.svg\" ");


{ // ### 1. ###
double a = 0;
double b = 3;
int N = 200;
double[] tabX = {0, 0.1, 0.3, 0.5, 0.7, 1, 1.5, 2, 3};
double[] tabY = {0, 
		 0.112462916,
		 0.328626759,
		 0.520499878,
		 0.677801194,
		 0.842700793,
		 0.966105146,
	 	 0.995322265,
		 0.999977910};

string toWriteTab = $"";
for(int i=0; i<tabX.Length; i++){
	toWriteTab += $"{tabX[i]}\t{tabY[i]}\n";
	}
File.WriteAllText("out.erfTab.data", toWriteTab);

string toWrite = $"";
for(int i=0; i<N; i++) {
	double xi = i*(b-a)/(N-1) + a;
	toWrite += $"{xi}\t{erf(xi)}\n";
	}
File.WriteAllText("out.erf.data", toWrite);
}


{ // ### 2. ###
double a = -5;
double b = 5;
int N = 1000;
double[] tabX = {1, 2, 3, 0.5, 3.0/2};
double[] tabY = {1, 
		 1,
		 2,
		 Sqrt(PI),
		 0.5*Sqrt(PI)};

string toWriteTab = $"";
for(int i=0; i<tabX.Length; i++){
	toWriteTab += $"{tabX[i]}\t{tabY[i]}\n";
	}
File.WriteAllText("out.gammaTab.data", toWriteTab);

string toWrite = $"";
for(int i=0; i<N; i++) {
	double xi = i*(b-a)/(N-1) + a;
	toWrite += $"{xi}\t{gamma(xi)}\n";
	}

File.WriteAllText("out.gamma.data", toWrite);

}

{ // ### 2. ###
double a = 0.0001;
double b = 7;
int N = 1000;
double[] tabX = {1, 2, 3, 0.5, 3.0/2, 4, 5, 6};
double[] tabY = {Log(1), 
		 Log(1),
		 Log(2),
		 Log(Sqrt(PI)),
		 Log(0.5*Sqrt(PI)),
		 Log(6),
		 Log(24),
		 Log(121)};

string toWriteTab = $"";
for(int i=0; i<tabX.Length; i++){
	toWriteTab += $"{tabX[i]}\t{tabY[i]}\n";
	}
File.WriteAllText("out.lngammaTab.data", toWriteTab);

string toWrite = $"";
for(int i=0; i<N; i++) {
	double xi = i*(b-a)/(N-1) + a;
	toWrite += $"{xi}\t{lngamma(xi)}\n";
	}

File.WriteAllText("out.lngamma.data", toWrite);

}


}
}
