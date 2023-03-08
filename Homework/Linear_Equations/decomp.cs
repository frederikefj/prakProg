using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(string[] args){
	int n = 0;
	int m = 0;
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
	/*
	var QTQ = Q.transpose()*Q;
	QTQ.print();
	R.print();
	*/
}
}
