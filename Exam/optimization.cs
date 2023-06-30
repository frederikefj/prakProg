using System;
using static System.Console;
using static System.Math;

public class globalMini {

public static vector quasiRandomMin(Func<vector,
	       	double> f,
	       	vector a,
	       	vector b,
		double time = 10,
		double acc = 0.001,
		string localMinimizer = "qnewton") {
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

	vector min = new vector(d);	
	if(localMinimizer == "qnewton") {
		min = Mini.qnewton(f, x0, acc);
	}
	if(localMinimizer == "simplex") {
		var random = new Random();
		matrix P = new matrix(d, d+1);
		// randomly distributes d+1 points around the current global minimum (x0)
	       	for(int j=0; j<d+1; j++) {
			for(int k=0; k<d; k++) {
				double r = random.NextDouble();
				P[j][k] = (b[k]-a[k])/Pow(i, 1.0/d)*(2*r-1)/2 + x0[k];
			}
		}
		min = Mini.simplex(f, P, acc);
	}
	return min;
	}

} // globalMini class: end
