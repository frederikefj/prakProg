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

private static double simplexSize(matrix P) {
	int d = P.size1;
	vector avg = new vector(d);
	for(int i=0; i<d+1; i++) {
		avg += P[i]/(d+1);
		}
	double distMax = (P[0]-avg).norm();
	double distI=0;
	for(int i=1; i<d+1; i++)
		distI = (P[i]-avg).norm();
		if(distI>distMax) distMax=distI;
	return distMax;
	}


// downhill simplex
// takes a function f of d variables and
// a matrix P with d+1 columns of d dimentional vectors
// and does uses the downhill simplex algoritm from there
// the function stops when the maximum distance from the 
// center is less than eps
public static vector simplex(Func<vector, double> f,
	       	matrix P,
	       	double eps=1e-3,
		bool printData = false,
		int maxLoops = 10000
		) {

	int d = P.size1;
	WriteLine($"d={d}");
	vector fP = new vector(d+1);
	for(int i=0; i<d+1; i++) fP[i] = f(P[i]);

	double fplo; // function value of highest point
	int iplo; // index of highest value point
	double fphi; // function value of lowest point
	int iphi; // index of lowest point
		  //
	vector pce = new vector(d);
	vector pre; double fpre; // reflection vector and value
	vector pex; double fpex; // expansion vector and value
	vector pco; double fpco; // contraction vector and value
	
	// counters to track the simplex:
	int Nloop=0; int Nre=0; int Nex=0; int Nco=0; int Nrd=0;

	while(!(simplexSize(P)<eps)) {
		// Check if simplex converged
		if(Nloop>maxLoops) {
			throw new ArgumentException($"Simplex did not converge before (maxLoops: {maxLoops}) was reached");
			}
		Nloop++;
		
		// update highest lowest vectors
		fplo = fP[0]; iplo=0;
		fphi = fP[0]; iphi=0;
		for(int i=1; i<d+1; i++) {
			if(fP[i]<fplo) {fplo=fP[i]; iplo = i;}
			if(fP[i]>fphi) {fphi=fP[i]; iphi = i;}
			}
		
		// update centroid vector
		pce *= 0;
		for(int i=0; i<d+1; i++) {
			if(i!=iphi) pce+=P[i]/d;
		}
		pre = pce + (pce-P[iphi]);
		fpre = f(pre);
		
		// choose path
		if(fpre<fplo) {
			pex = pce + 2*(pce-P[iphi]);
			fpex = f(pex);
			if(fpex<fpre) {
				P[iphi] = pex.copy(); // accept expansion
				fP[iphi] = fpex; Nex++;
			} else {
				P[iphi] = pre.copy(); // accept reflection
				fP[iphi] = fpre; Nre++;
			}
		} else {
			if(fpre<fphi) {
				P[iphi] = pre.copy(); // accept reflection
				fP[iphi] = fpre; Nre++;
			} else {
				pco = pce + 0.5*(pce-P[iphi]);
				fpco = f(pco);
				if(fpco<fphi) {
					P[iphi] = pco.copy(); // accept contraction
					fP[iphi] = fpco; Nco++;
				} else {
					//do reduction
					for(int i=0; i<d+1; i++) {
						if(i!=iplo) {
							P[i] = 0.5*(P[i]+P[iplo]);
							fP[i] = f(P[i]);
						}
						Nrd++;
					}
				}
			}
		}
	} 
	
	if(printData==true) {
		WriteLine($"Simplex converged using {Nre} reflections, {Nex} expansions,");
		WriteLine($" {Nco} contractions and {Nrd} reductions.");
		}

	// Finding averge point and returning
	vector avg = new vector(d);
	for(int i=0; i<d+1; i++) {
		avg+=P[i]/(d+1);
		}
	return avg;
}



} // class Mini: end



