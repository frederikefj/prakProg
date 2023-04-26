using System;
using static System.Console;
using static System.Math;
using System.IO;
/*
public class genlist<T>{
	public T[] data;
	public int size => data.Length;
	public T this[int i] => data[i];
	public genlist() {data = new T[0]; }
	public void add(T item) {
		T[] newdata = new T[size+1];
		System.Array.Copy(data, newdata, size);
		newdata[size]=item;
		data=newdata;
		}
}
*/
public static class main{

public static void Main(string[] args){

WriteLine("\n###############[ Opgave 1 ]###############\n");
// Opgave 1 start

var list = new genlist<double[]>();
char[] delimiters = {' ', '\t'};
var options = StringSplitOptions.RemoveEmptyEntries;
for(string line = ReadLine(); line!=null; line = ReadLine()){
	var words = line.Split(delimiters, options);
	int n = words.Length;
	var numbers = new double[n];
	for(int i=0; i<n; i++) numbers[i] = double.Parse(words[i]);
	list.add(numbers);
	}
WriteLine($"Loading the file numbers.txt into a genlist");
WriteLine($"Printing the elements of the genlist");
for(int i=0; i<list.size;i++){
	var numbers = list[i];
	foreach(var number in numbers) Write($"{number : 0.00e+00; -0.00e+00}");
	WriteLine();
	}

// Opgave 1 end
WriteLine("");

}
}
