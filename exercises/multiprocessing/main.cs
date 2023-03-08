using System;
using System.Threading;
using static System.Console;
using static System.Math;
class main{
	public class data { public int a,b; public double sumab; }
	public static void harm(object obj){	
		data x = (data)obj;
		x.sumab=0;
		for(int i=x.a;i<x.b;i++)x.sumab+=1.0/i;
		//WriteLine($"x.a = {x.a} x.b = {x.b} partialsum = {x.sumab}");
	}

	public static int Main(string[] args){
	int nterms=(int)1e8, nthreads=1;
	foreach(var arg in args){
		var words = arg.Split(':');
		if(words[0]=="-terms")nterms=(int)float.Parse(words[1]);

		if(words[0]=="-threads")nthreads=(int)float.Parse(words[1]);
	}
	WriteLine($"Calculation harmonic series with {nterms} terms using {nthreads} threads:");
	
	data[] x = new data[nthreads];
	for(int i=0; i<nthreads;i++){
		x[i] = new data();
		x[i].a = 1 + i*(nterms/nthreads);
		x[i].b = 1 + (i+1)*(nterms/nthreads);
		}
	
	Thread[] threads = new Thread[nthreads];
	for(int i=0; i<nthreads;i++){
		threads[i] = new Thread(harm);
		threads[i].Name = $"thread number {i+1}";
		threads[i].Start(x[i]);
		}
	for(int i=0;i<nthreads;i++){
		threads[i].Join();
		}
	
	double total=0;
	for(int i=0;i<nthreads;i++){
		total+=x[i].sumab;
		}
	WriteLine($"Sum = {total}");
	return 0;
	}
}
