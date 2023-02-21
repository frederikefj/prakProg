using System;
using static System.Console;
using static System.Math;

public static class main{

	public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
	if(Abs(a-b) < acc) return true;
	else if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps) return true;
	else return false;
	}

	public static void Main(){
	
	int i=1;
	while(i+1>1) {i++;}
	Write($"my max int = {i} int.MaxValue = {int.MaxValue}\n");

	int j=1;
	while(j-1<1) {j--;}
	Write($"my min int = {j} int.MinValue = {int.MinValue}\n");
	
	double x=1;
	while(1+x/2!=1) {x/=2;}
	Write($"The smallest double is {x*2}\n");
	
	float y = 1F;
	while(1+y/2!=1) {y/=2;}
	Write($"and smallest float is {y*2}\n");

	int n=(int)1e6;
	double epsilon = Pow(2,-52);
	double tiny = epsilon/2;
	double sumA = 0; 
	double sumB = 0;
	sumA += 1;
	for (int k=0; k<n; k++) {sumA += tiny;}
	for (int k=0; k<n; k++) {sumB += tiny;}
	sumB += 1;

	Write($"sumA = {sumA} and sumB = {sumB}\n");
	
	bool test1 = approx(1, 1.0001);
	Write($"is 1=1.0001 {test1}\n");
	
	bool test2 = approx(1, 1.0000000001);
	Write($"is 1=1.0000000001 {test2}\n");
	
	}

}
