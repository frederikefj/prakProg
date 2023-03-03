using System;
using static System.Console;
using static System.Math;


public static class main{
public static class QRGS{	
	public static void decomb(matrix A, matrix R){ 
		int n = A.size1;
		int m = A.size2;
		for (int i = 0; i<m; i++) {
			double Rii = 0;
			for (int j = 0; j<n; j++) {
				Rii += A[i][j]*A[i][j];
				}
			R[i][i] = Sqrt(Rii);
			for (int j = 0; j<n; j++) {
				A[i][j] = A[i][j]/Rii;
				}
				
			for (int j = i; j<m; j++) {
				double Rij = 0;
				for (int k = 0; k<n; k++) {
					Rij += A[i][k]*A[j][k];
					}
				R[i][j] = Rij;
				WriteLine($"i{i}, j{j}, Rij");
				for (int k = 0; k<n; k++) {
					A[j][k] = A[j][k] - A[i][k]*Rij;
					}			
				}
			WriteLine($"i = {i}, Rii = {Rii}");


			}
		}
	public static vector solve(matrix Q, matrix R, vector b) {
		var c = new vector(b.size);
		return c;
		}
	public static double det(matrix R) {
		return R[0][0];
		}
	}

public static void Main(){
	int n = 5;
	int m = 3;
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
	
	WriteLine($"Generating a random lenght:{n} vector (b) and size:{n}x{m} matrix (A):");
	if(n<10){
		WriteLine("b:");	
		b.print();
		WriteLine("A:");
		A.print();
		}

	QRGS.decomb(A, R);

	A.print();
	R.print();

}
}
