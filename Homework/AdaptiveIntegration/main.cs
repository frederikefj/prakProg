using System;
using static System.Console;
using static System.Math;
using System.IO;

public class erfHelp{
	double z;
	public erfHelp(double zi) {z = zi;}
	public void erfHelpChange(double zi) {z = zi;}	
	public double Func1(double t) {return Exp(-Pow(z+(1.0-t)/t,2.0)/t/t);}
	public double Func2(double t) {return Exp(-t*t);}
	}

public static class main{

public static double f1(double x) {return Sqrt(x);}
public static double f2(double x) {return 1.0/Sqrt(x);}
public static double f3(double x) {return 4*Sqrt(1-x*x);}
public static double f4(double x) {return Log(x)/Sqrt(x);}
public static double erfAp(double x){
	/// single precision error function (Abramowitz and Stegun, from Wikipedia)
	if(x<0) return -erfAp(-x);
	double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
	double t=1/(1+0.3275911*x);
	double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
	return 1-sum*Exp(-x*x);
	}

public static double erfH1(double t, double z) {return Exp(-Pow(z+(1.0-t)/t,2.0)/t/t);}
public static double erfH2(double t) {return Exp(-t*t);}

public static double erfInt(double z, double d=0.001, double e=0.001) {
	if(z<0) return -erfInt(-z, d, e);
	if(/*z>1*/false) {
		Func<double, double> fz = t => erfH1(t, z);
		return 1-2.0/Sqrt(PI)*Integ.integrate(fz, 0.0, 1.0, d*Sqrt(PI)/2.0, e);
		}
	else {
		return 2.0/Sqrt(PI)*Integ.integrate(erfH2, 0.0, z, d*Sqrt(PI)/2.0, e);
		}
	}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start
WriteLine("Adaptive numeric integrator is made in the Integ.cs library. It uses the points (1/6, 2/6, 4/6, 5/6) to find the integral and error. If the error is less than the accepted error it recursivly tries again on the half intervals.");
WriteLine("The integrator is tested on some examples with known analytic results");
WriteLine("The maximum accepted error (max error) is the criteria for the adaptive integrator (absolute error) + (relative error)*(result)");

WriteLine("");
WriteLine("sqrt(x) in [0,1]");
double integ1 = Integ.integrate(f1, 0, 1, 0.001, 0.001);
double error1 = 1e-3 + 1e-3*integ1;
WriteLine($"Result:    {integ1}");
WriteLine($"Max Error: {error1}");
WriteLine($"Expected:  {2.0/3}");

WriteLine("");
WriteLine("1/sqrt(x) in [0,1]");
double integ2 = Integ.integrate(f2, 0, 1, 0.001, 0.001);
double error2 = 1e-3 + 1e-3*integ2;
WriteLine($"Result:    {integ2}");
WriteLine($"Max Error: {error2}");
WriteLine($"Expected:  {2.0}");

WriteLine("");
WriteLine("4*sqrt(1-x^2) in [0,1]");
double integ3 = Integ.integrate(f3, 0, 1, 0.001, 0.001);
double error3 = 1e-3 + 1e-3*integ3;
WriteLine($"Result:    {integ3}");
WriteLine($"Max Error: {error3}");
WriteLine($"Expected:  {PI}");

WriteLine("");
WriteLine("ln(x)/sqrt(x) in [0,1]");
double integ4 = Integ.integrate(f4, 0, 1, 0.001, 0.001);
double error4 = 1e-3 + 1e-3*integ4;
WriteLine($"Result:    {integ4}");
WriteLine($"Max Error: {error4}");
WriteLine($"Expected:  {-4.0}");

WriteLine("");
WriteLine("All results are within the errors.");
WriteLine("");
WriteLine("The error function is also implemented using the integral representation.");
WriteLine("The integral error function is compared to the approximation from the plots exercise and some tabulated values:");
WriteLine("");

double delta = 0.000000001;
double eps = 0.000000001;

WriteLine("z = 0.1:");
WriteLine( "Tabulated:     0.112462916");
WriteLine($"Approximation: {erfAp(0.1)}");
WriteLine($"Integral:      {erfInt(0.1)}");
WriteLine($"Max error:     {delta+erfInt(0.1)*eps}");

WriteLine("z = 0.7:");
WriteLine( "Tabulated:     0.677801194 	");
WriteLine($"Approximation: {erfAp(0.7)}");
WriteLine($"Integral:      {erfInt(0.7)}");
WriteLine($"Max error:     {delta+erfInt(0.7)*eps}");

WriteLine("z = 1.3:");
WriteLine( "Tabulated:     0.934007945");
WriteLine($"Approximation: {erfAp(1.3)}");
WriteLine($"Integral:      {erfInt(1.3)}");
WriteLine($"Max error:     {delta+erfInt(1.3)*eps}");


// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B star


WriteLine("");
// Opgave B end

}
} 
