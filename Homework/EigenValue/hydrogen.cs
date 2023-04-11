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
		WriteLine($"{r[i]} {Pow(V[energyLevel][i], 2)/Pow(C,2)}");
		}
	}

}
}
