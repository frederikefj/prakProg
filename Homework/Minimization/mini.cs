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
