using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(){
	int n = 6;
	int m = 6;
	var randomNumber = new System.Random(1);
	
	var b = new vector(n);
	for (int i=0; i<n; i++) {
		b[i] = randomNumber.NextDouble();
		}

	var A = new matrix(n,m);
	for (int i=0; i<n; i++) {
		for (int j=0; j<m; j++) {
			A[i,j] = randomNumber.NextDouble();
			}
		}

	var R = new matrix(m,m);	
	var Q = A.copy();
	QRGS.decomb(Q, R);

	WriteLine($"Generating a random lenght:{n} vector (b) and size:{n}x{m} matrix (A):");
	WriteLine("Preparing {m}x{m} matrix (R) and a copy of A (Q). Decomb used on matrix (Q).");
	var QR = Q*R;	
	bool QRapproxA = QR.approx(A);
	WriteLine($"Is QR = A: {QRapproxA}");
	var I = matrix.id(m);
	var QTQ = Q.transpose()*Q;
	bool QTQapproxI = QTQ.approx(I);
	WriteLine($"Is T(Q)Q equal to 1: {QTQapproxI}");
	bool isRupperT = true;
	for (int i=0; i<m; i++) {
		for (int j=0; j<i; j++) {
			bool con = matrix.approx(R[i,j], 0);
			if(con != true) {
				isRupperT = false;
				}
			}
		}
	WriteLine($"Is R upper triangular: {isRupperT}");

	if(n<10){
		WriteLine($"\n   Because n={n}<10 Various matrixes are printed:");
		WriteLine("\n   b:");	
		b.print();
		WriteLine("\n   A:");
		A.print();	
		WriteLine("\n   Q:");
		Q.print();
		WriteLine("\n   R:");
		R.print();
		
		WriteLine("\n   T(Q)Q. T(Q)Q should be equal to I:");
		QTQ.print();
		WriteLine("\n   QR. QR should be equal to A:");
		QR.print();
	} else {
		WriteLine($"\n   Matrix and tests not printed because n={n} is larger than 10");
	}

	
	WriteLine("\nSolveing the differential equation Rx=T(Q)b. Solution x = ");
	var s = QRGS.solve(Q, R, b);
	s.print();
	WriteLine("Checking that x is a solution by calculating Ax. Ax should be equal to b if the matrix is square:");
	var As = A*s;
	As.print();
	WriteLine("Inverse of A:");
	var B = QRGS.inverse(Q, R);
	B.print();

	WriteLine("Checking that AB = 1;");
	var AB = A*B;
	AB.print();
	
	WriteLine("Checking that BA = 1;");
	var BA = B*A;
	BA.print();
}
}
