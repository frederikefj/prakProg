using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class Mini{

public static vector gradient(Func<vector, double> f,
		vector x,
		double fx) {
	int n = x.size;
	vector dx = new vector(n);
	vector grad = new vector(n);
	for(int i=0; i<n; i++) {
		dx[i] = Max(Abs(x[i]), Pow(2, -13))*Pow(2, -26);
		grad[i] = (f(x+dx) - fx)/dx[i];
		dx[i] = 0;	
		}
	return grad;
	}

public static vector qnewton(
		Func<vector, double> f,
		vector x0,
		double acc = 0.001,
		bool printSteps = false
		)
	{
	int n = x0.size;
	vector x = x0.copy();
	matrix B = new matrix(n, n);
	B.set_identity();
	int steps = 0;
	while(true) {
		double fx = f(x);
		vector gx = gradient(f, x, fx);
		if(gx.norm()<acc) break;
		vector dx = -B*gx;
		double l = 1;
		vector s=dx.copy();
		double fxs=0;	
		while(!(l<1.0/1024)) {
			s = l*dx;
			fxs = f(x+s);
			if(fxs < fx) {
				break;
				}
			l*=0.5;
			}
		if(l<1.0/1024) {B.set_identity();}
		else {
			vector gxs = gradient(f, x+s, fxs);
			vector y = gxs - gx;
			vector u = s - B*y;
			double uTy = u.dot(y);
			if(Abs(uTy)>Pow(2, -26)) {
				matrix dB = matrix.outer(u,u)/uTy;
				B = B + dB;
				}
			}
		//WriteLine($"x: ({x[0]}, {x[1]}), s: ({s[0]}, {s[1]}), f: {fx}");
		x = x + s;
		steps++;
		}

	vector min = x;
	if(printSteps==true) WriteLine($"number of steps to converge: {steps}");
	return min;	
	}
} // class: Mini, end


class main{
public static int test2N = 0;
public static double test2(vector x) {test2N++; 
	return x[0]*x[0]*x[0]*x[0]-7*x[0]*x[0]*x[0]-32*x[0]*x[0]+x[1]*x[1];}

public static int valleyN = 0;
public static double valley(vector x) {valleyN++; 
	return Pow((1-x[0]),2) + 100*Pow((x[1]-x[0]*x[0]),2);}

public static int HimmelblauN = 0;
public static double Himmelblau(vector x) {HimmelblauN++; 
	return Pow(x[0]*x[0] + x[1] - 11,2)+Pow(x[0] + x[1]*x[1] - 7,2);}

public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start

WriteLine("Testing the quasi newton minimizer with some different functions:\n");

WriteLine("Finding a minimum of f(x,y)=x^4-7y^3-32x^2+y^2");
WriteLine("which has minima at (-2.16,0) and (7.41,0)");
vector x02 = new vector(2);
x02[0] = 10; x02[1] = -4;
WriteLine($"starting at ({x02[0]}, {x02[1]})");
vector min2 = Mini.qnewton(test2, x02, 1e-3, printSteps: true);
WriteLine($"Minimum is found at x=({min2[0]}, {min2[1]}) with accuracy: 1e-3");
WriteLine($"The number of function evaluations used was {test2N}");
WriteLine("");

WriteLine("Finding the minimum of Rosenbrock's valley function");
WriteLine("f(x,y)=(1-x)^2+100(y-x^2)^2");
WriteLine("which has a single minima at (1,1)");
vector x0R = new vector(2);
x0R[0] = -2; x0R[1] = 3;
WriteLine($"starting at ({x0R[0]}, {x0R[1]})");
vector minR = Mini.qnewton(valley, x0R, 1e-3, printSteps: true);
WriteLine($"Minimum is found at x=({minR[0]}, {minR[1]}) with accuracy: 1e-3");
WriteLine($"The number of function evaluations used was {valleyN}");
WriteLine("");

WriteLine("Finding a minimum of Himmelblau's function");
WriteLine("f(x,y)=(x^2-y-11)^2 + (x+y^2-7)^2");
WriteLine("which has minima at (3,2), (-2.81, 3.13), (-3.78, -3.28) and (3.58, -1.85)");
vector x0H = new vector(2);
x0H[0] = -6; x0H[1] = 5;
WriteLine($"starting at ({x0H[0]}, {x0H[1]})");
vector minH = Mini.qnewton(Himmelblau, x0H, 1e-3, printSteps: true);
WriteLine($"Minimum is found at x=({minH[0]}, {minH[1]}) with accuracy: 1e-3");
WriteLine($"The number of function evaluations used was {HimmelblauN}");

// Opgave A end
WriteLine("");

WriteLine("\n###############[ Opgave B ]###############\n");
// Opgave B start



// Opgave B end
WriteLine("");
}
}
