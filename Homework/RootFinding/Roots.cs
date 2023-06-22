using System;
using static System.Console;
using static System.Math;
using System.IO;


public static class Roots{

public static matrix jacobi(Func<vector, vector> f, vector x, vector fx) {
	int n = x.size;
	int m = fx.size;
	matrix J = new matrix(m, n);
	for(int i=0; i<n; i++) {
		vector dxi = new vector(n);
		dxi[i] = Abs(x[i])*Pow(2, -26);
		vector fxdxi = f(x+dxi);
		for(int j=0; j<m; j++) {
			J[j, i] = (fxdxi[j] - fx[j])/dxi[i];
			}
		}
	return J;
	}

public static (vector, int) newton(Func<vector, vector> f, vector x0, double eps=1e-2) {
	int fCall = 0;
	int n = x0.size; //Side of input vector
	for(int i=0; i<n; i++) { 
		if(Abs(x0[i])<Pow(2, -22)) x0[i] = Pow(2,-22);
		}  // Prevents devision by zero in jacobi matrix calculation of starting value.
	
	vector fx = f(x0); fCall++; //The current value of f(x) is saved so it only needs to be calculated once each step
	int m = fx.size;
	if(n!=m) throw new ArgumentException($"f must have dimension n->n but {n}->{m} was given");
	
	vector x = x0.copy();
	while(fx.norm()>=eps) 
		{
		matrix R = new matrix(n, n);
		matrix J = jacobi(f, x, fx); fCall += n;	
		QRGS.decomb(J, R);
		vector dx = QRGS.solve(J, R, -fx); for(int i=0; i<n; i++) {if(Abs(dx[i])<Pow(2, -26)) break;}
		double l = 1;
		while(true) {
			vector fxi = f(x+l*dx); fCall++;
			if(fxi.norm() < (1-l/2)*fx.norm() | l<=1.0/1024) {x=x+l*dx; fx=fxi; break;}
			else {l*=0.5;}
			}
		}
	return (x, fCall);
	}



}
