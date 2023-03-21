using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(string[] args){

WriteLine("\n###############[ Opgave A ]###############\n");
// Opgave A start
{
int n = 0;
foreach(var arg in args) {
	var words = arg.Split(':');
	if(words[0]=="-n") {n = int.Parse(words[1]);}
	}
var randomNumber = new System.Random(1);
//int n = 5;
matrix A = new matrix(n,n);
matrix V = new matrix(n,n);
V.set_identity();
matrix I = V.copy();
for(int i=0;i<n;i++){
	A[i,i] = randomNumber.NextDouble();
	for(int j=i+1;j<n;j++){
		A[i,j] = randomNumber.NextDouble();
		A[j,i] = A[i,j];
		}
	}
matrix D = A.copy();
//A.print();
/*
bool changed;
do{
	changed = false;
	for (int p = 0; p<n-1;p++) { 
		for (int q=p+1; q<n; q++) {	
			if(D[p,q]*D[p,q]!=0) {
				changed = true;
				double theta = 0.5*Atan2(2*D[p,q],D[q,q]-D[p,p]);
				jacobi.timesJ(D, p, q, theta);
				jacobi.Jtimes(D, p, q, -theta);
				jacobi.timesJ(V, p, q, theta);
				}
			}
		}	
}while(changed);
*/
jacobi.decomp(D, V);

matrix VtAV = V.transpose()*A*V;
matrix VDVt = V*D*V.transpose();
matrix VtV = V.transpose()*V;
matrix VVt = V*V.transpose();
bool VtAV_D = VtAV.approx(D);
bool VDVt_A = VDVt.approx(A);
bool VtV_1 = VtV.approx(I);
bool VVt_1 = VVt.approx(I);

WriteLine($"Eigenvalue decomposition for a random {n}x{n} matrix (A = VDVt)\n");

WriteLine($"is VtAV = D: {VtAV_D}");
WriteLine($"is VDVt = A: {VDVt_A}");
WriteLine($"is VtV = 1: {VtV_1}");
WriteLine($"is VVt = 1: {VVt_1}");

if(n>8) {WriteLine($"\nNo matrixes are printed when n={n} is larger than 8.");}
if(n<9) {
	WriteLine($"\nMatrixes are printed since n={n} is smaller than 9.");
	WriteLine("\nA: ");
	A.print();
	WriteLine("\nD: ");
	D.print();
	WriteLine("\nV: ");
	V.print();
	}
}
// Opgave A end

WriteLine("\n###############[ Opgave B ]###############\n");

{
WriteLine("Plots of the lowest eigenValue E0 as a function of rmax and dr can be found in dr_E0.svg and rmax_E0.svg");
WriteLine("At dr=0.1 and rmax=8 the value of E0 converges. The lowest eigenvectors are plotted in plot.svg");
}

}
}
