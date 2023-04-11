using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(string[] args){
	string[] lines = System.IO.File.ReadAllLines("out.times.data");
	int n = 0;
	foreach(var line in lines) {n += 1;}
	vector b = new vector(n);
	vector x = new vector(n);
	int fv = 1;
	matrix A = new matrix(n, fv);
	int i = 0;
	foreach(var line in lines) {
		var words = line.Split(" ");
		b[i] = double.Parse(words[1]);
		x[i] = int.Parse(words[0]);
		//A[0][i] = 1.0;
		//A[1][i] = Pow(double.Parse(words[0]),3);
		A[0][i] = Pow(double.Parse(words[0]),3);
		i += 1;
		}
	matrix R = new matrix(fv, fv);
	QRGS.decomb(A, R);
	var c = QRGS.solve(A, R, b);	
	
	for(int j=0; j<n; j++) {
		WriteLine($"{x[j]} {Pow(x[j], 3)*c[0]}");
		}

	/*
	foreach(var arg in args) {
		var words = arg.Split(':');
		if(words[0]=="-size") {
			n = int.Parse(words[1]);
			m = n;
			}
		}
	
	var randomNumber = new System.Random(1);
	var A = new matrix(n,m);
	for (int i=0; i<n; i++) {
		for (int j=0; j<m; j++) {
			A[i,j] = randomNumber.NextDouble();
			}
		}
	
	var R = new matrix(m,m);	
	var Q = A.copy();
	QRGS.decomb(Q, R);
	var QTQ = Q.transpose()*Q;
	QTQ.print();
	R.print();
	*/
}
}
