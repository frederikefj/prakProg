using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class ODE{
	public static (vector, vector) rkstep12(
		Func<double,vector,vector> f,
		double x,
		vector y,
		double h)
	{
		vector k0 = f(x,y);
		vector k1 = f(x+h/2, y+k0*(h/2));
		vector yh = y+k1*h;
		vector er = (k1-k0)*h;
		return (yh,er);
	}

	public static (genlist<double>, genlist<vector>) driver(
			Func<double, vector, vector> f,
			double a,
			vector ya,
			double b,
			double h = 0.01,
			double acc=0.01,
			double eps=0.01
			){
		if(a>b) throw new ArgumentException("driver: a>b, chose a<b");
		double x=a; vector y = ya.copy();
		var xlist = new genlist<double>(); xlist.add(x);
		var ylist = new genlist<vector>(); ylist.add(y);
		do {
			if(x>=b) return (xlist,ylist);
			if(x+h>b) h=b-x;
			var (yh, erv) = rkstep12(f, x, y, h);
			double tol = (acc+eps*yh.norm())*Sqrt(h/(b-a));
			double err = erv.norm();
			if(err<tol) {
				x+=h; y=yh;
				xlist.add(x); ylist.add(y);
				}
			h*= Min( Pow(tol/err,0.25)*0.95 , 2);
			} while(true);
		} 
	
	public static vector driver2(
			Func<double, vector, vector> f,
			double a,
			vector ya,
			double b,
			double h = 0.01,
			double acc=0.01,
			double eps=0.01,
			genlist<double> xlist=null, 
			genlist<vector> ylist=null
			){
		
		if(a>b) throw new ArgumentException("driver: a>b, chose a<b");
		double x=a; vector y = ya.copy(); double xNext = 0; int xIndex = 0;
		int n = 0;
		if(xlist!=null) n = xlist.size;
		if(xlist==null) xNext = b+1; else xNext = xlist[0];
		if(xNext < a) throw new ArgumentException("driver: xlist outside of range of [a,b]");
		if(xNext == a) {
			ylist.add(y);	
			xIndex += 1; if(xIndex == n) xNext = b+1; else xNext = xlist[xIndex]; 
			}
		bool saveVal = false;
		vector tol = new vector(y.size);
		do {
			if(x>=b) {if(xNext==b) ylist.add(y); return y;}
			if(x+h>b) h=b-x;
			if(x+h>=xNext) {
				h=xNext-x; saveVal = true;
				}
						
			var (yh, erv) = rkstep12(f, x, y, h);

			for(int i=0; i<y.size; i++) tol[i] = (acc+eps*Abs(yh[i])*Sqrt(h/(b-a)));
			bool ok = true;
			for(int i=0;i<y.size;i++) if( tol[i]<=Abs(erv[i]) ) ok = false;
			if(ok){
				x+=h; y=yh;
				if(saveVal == true) {
					ylist.add(y);
					xIndex += 1; if(xIndex == n) xNext = b+1; else xNext = xlist[xIndex]; 
					}
				}
			
			double factor = tol[0]/Abs(erv[0]);
			for(int i=0; i<y.size; i++) factor = Min(factor, tol[i]/Abs(erv[i]));
			h *= Pow(factor, 0.25)*0.95;
			
			saveVal = false;
			} while(true);
		} 

}
