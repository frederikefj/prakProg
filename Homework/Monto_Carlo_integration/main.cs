using System;
using static System.Console;
using static System.Math;
using static System.Random;
using System.IO;


public static class main{
/*
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
*/
public static double f0(vector x) {
	double below = 1.0-Cos(x[0])*Cos(x[1])*Cos(x[2]);
	if(below==0) throw new Exception($"function 1/(1-cos(x)cos(y)cos(z)): Devision by zero. vector = ({x[0]},{x[1]},{x[2]})");
	double result = 1.0/(1.0-Cos(x[0])*Cos(x[1])*Cos(x[2]));
	return result;
	}

public static double f1(vector x) {
	return Cos(x[0])*Cos(x[0])+Sin(x[1])*Sin(x[1]);
	}

public static double f2(vector x) {
	if(x[0]*x[0]+x[1]*x[1]<=1) return 1;
	else return 0;
	}
/*
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
*/
public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start
{

WriteLine("multidimentional Monte Carlo-integration is implemented in the MonteCarlo.cs file (library).");
WriteLine("");
WriteLine("Integration of cos^2(x)+cos^2(y) over [0,pi]^2 using 1m points:");

double[] a1I = {0, 0};
double[] b1I = {PI, PI};
vector a1 = new vector(a1I);
vector b1 = new vector(b1I);

(double integ1, double error1) = MonteCarlo.plainmc(f1, a1, b1, 1000000);
WriteLine($"Result: {integ1}, error = {error1}");
WriteLine($"Expected result: pi^2={PI*PI}");

int points = 50;
double scale = 0.2;
double pStart = 100000;
string toWrite = $"";
for(int i=0; i<points+1; i++) {
	double pID = pStart/(Pow(1+i*scale,2));
	int pI = (int)pID;
	(double integi, double errori) = MonteCarlo.plainmc(f1, a1, b1, pI);
	toWrite += $"{1+i*scale}\t{errori}\n";
	}
File.WriteAllText("out.error.data", toWrite);

WriteLine("");
WriteLine("Integration of a unit circle over [-1,1]^2 using 1m points:");

double[] a2I = {-1, -1};
double[] b2I = {1, 1};
vector a2 = new vector(a2I);
vector b2 = new vector(b2I);

(double integ2, double error2) = MonteCarlo.plainmc(f2, a2, b2, 1000000);
WriteLine($"Result: {integ2}, error = {error2}");
WriteLine($"Expected result: pi={PI}");


WriteLine("");
WriteLine("Integration of 1.0/(1.0-cos(x)cos(y)cos(z)) over [0,pi]^3: using 1m points");

double[] aI = {0, 0, 0};
double[] bI = {PI, PI, PI};
vector a = new vector(aI);
vector b = new vector(bI);

(double integ, double error) = MonteCarlo.plainmc(f0, a, b, 1000000);
WriteLine($"Result: {integ/Pow(PI, 3)}, error = {error/Pow(PI,3)}");
WriteLine("Expected result: 1.3932039296856768591842462603255");

WriteLine("\nTo check if the function scales as 1/sqrt(N) the error of the fit is plotted as a function of 1/sqrt(N) in the plot \"Aplot.svg\"");
WriteLine("A linear fit is also plotted to show that the error scales with 1/sqrt(N)");

}
// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start
{

WriteLine("Monte Carlo-integration with psudorandom numbers is implemented in the MonteCarlo.cs file.");
WriteLine("The error is estimated by running the integration 2 times with different bases.");
WriteLine("");
WriteLine("Integration of cos^2(x)+cos^2(y) over [0,pi]^2 using 1m points:");


double[] a1I = {0, 0};
double[] b1I = {PI, PI};
vector a1 = new vector(a1I);
vector b1 = new vector(b1I);

(double result3, double error3) = MonteCarlo.quasiInteg(f1, a1, b1, 1000000);
WriteLine($"result = {result3}, error = {error3}\n");

WriteLine("The error of the psudorandom method is compared to the regular Monte Carlo method using a log-log plot in \"Bplot.svg\"");



int N = 100;
int scale = 1000;
string toWriteQ = $"";
string toWriteM = $"";
for(int i=1; i<N; i++) {	
	(double Qresult, double Qerror) = MonteCarlo.quasiInteg(f1, a1, b1, i*scale);
	(double Mresult, double Merror) = MonteCarlo.plainmc(f1, a1, b1, i*scale);
	//toWrite += $"{i*scale}\t{Merror/Qerror}\n";	
	toWriteQ += $"{i*scale}\t{Qerror}\n";		
	toWriteM += $"{i*scale}\t{Merror}\n";	
	}
File.WriteAllText("out.errorQ.data", toWriteQ);
File.WriteAllText("out.errorM.data", toWriteM);

}
// Opgave B end
WriteLine("");

}
}
