using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(string[] args){

double dr = 0.1;
double rmax = 5;
bool dr_E0_out = false;
bool rmax_E0_out = false;
bool plot_out = false;
int N = 0;
int energyLevel = 0;

foreach(var arg in args) {
	var words = arg.Split(':');
	if(words[0]=="-dr") {dr = double.Parse(words[1]);}
	if(words[0]=="-rmax") {
		rmax = double.Parse(words[1]);
		N = (int)(rmax/dr)-1;
		}
	if(words[0]=="-energyLevel") {energyLevel = int.Parse(words[1]);}
	if(words[0]=="-out") {
		if(words[1]=="dr_E0") {dr_E0_out = true;}
		if(words[1]=="rmax_E0") {rmax_E0_out = true;}
		if(words[1]=="plot") {plot_out = true;}
		}
	}

vector r = new vector(N);
for(int i=0; i<N;i++) {r[i]=dr*(i+1);}
matrix H = new matrix(N, N);
for (int i=0;i<N-1;i++){
	H[i,i] = -2;
	H[i,i+1] = 1;
	H[i+1,i] = 1;
	}
H[N-1,N-1] = -2;

H *= -0.5/dr/dr;
for(int i=0; i<N; i++) {H[i,i]+=-1/r[i];}

/*
WriteLine("\nH: ");
H.print();
WriteLine("\nr: ");
r.print();
*/

matrix V = new matrix(N, N);
jacobi.decomp(H, V);
int j = 0;
double E0 = H[0,0];
for(int i=1; i<N; i++) {
	if(H[i,i] < E0) {
		E0 = H[i,i];
		j = i;
		}	
}

if(dr_E0_out==true) {WriteLine($"{dr} {H[energyLevel][energyLevel]}");}
if(rmax_E0_out==true) {WriteLine($"{rmax} {H[energyLevel][energyLevel]}");}
if(plot_out==true) {
	double C = 0;
	for (int i=0; i<N; i++) {C += Pow(V[energyLevel][i], 2); }
	C = Pow(C, 1/2);
	for (int i=0; i<N; i++) {
		WriteLine($"{r[i]} {V[energyLevel][i]/r[i]/C}");
		}
	}


/*
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
*/
}
}
