using System;
using static System.Console;
using static System.Math;


public static class main{
public static void Main(string[] args){

double rmax = 5;
int energyLevel = 0;
int N = 200;

foreach(var arg in args) {
	var words = arg.Split(':');
	if(words[0]=="-rmax") {rmax = double.Parse(words[1]);}
	if(words[0]=="-energyLevel") {energyLevel = int.Parse(words[1]);}
	}

vector r = new vector(N+1);
vector fr = new vector(N+1);
for(int i=0; i<N+1;i++) {r[i]=rmax/N*i;}

if(energyLevel == 0) {
	for(int i = 0; i<N+1; i++) {
		fr[i] = 1/Sqrt(PI)*Exp(-r[i]);
		}
	}

if(energyLevel == 1) {
	for(int i = 0; i<N+1; i++) {
		fr[i] = 1/Sqrt(8*PI)*(1-0.5*r[i])*Exp(-r[i]/2);
		}
	}

if(energyLevel == 2) {
	for(int i = 0; i<N+1; i++) {
		fr[i] = 1/Sqrt(27*PI)*(1-2.0/3*r[i]+2.0/27*r[i]*r[i])*Exp(-r[i]/3);
		}
	}



for (int i=0; i<N+1; i++) {
	WriteLine($"{r[i]} {fr[i]}");
	}

}
}
