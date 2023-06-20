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

public static vector newton(Func<vector, vector> f, vector x0, double eps=1e-2) {
	// make sure that the components of starting guess x is larger than sqrt(ε) (mashine epsilon) 
	// So that the step to calculate the jacobi matrix doesn't get smaller than mashine epsilon
	int callN = 1;
	int n = x0.size; //Side of input vector
	vector fx = f(x0); //The current value of f(x) is saved so it only needs to be calculated once each step
	int m = fx.size;
	if(n!=m) throw new ArgumentException($"f must have dimension n->n but {n}->{m} was given");
	vector x = x0.copy();
	while(fx.norm()>=eps) 
		{
		matrix R = new matrix(n, n);
		matrix J = jacobi(f, x, fx);
		QRGS.decomb(J, R);
		vector dx = QRGS.solve(J, R, -fx);
		double l = 1;
		while(true) {
			vector fxi = f(x+l*dx); callN++;
			if(fxi.norm() < (1-l/2)*fx.norm()) {x=x+l*dx; fx=fxi; break;}
			else {l*=0.5;}
			}
		}
	WriteLine($"Number of function evaluations: {callN}");
	return x0;
	}



}
