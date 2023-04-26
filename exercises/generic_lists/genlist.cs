using System;
using static System.Console;
using static System.Math;
using System.IO;

public class genlist<T>{
	public T[] data;
	public int size => data.Length;
	public T this[int i] {get => data[i]; set => data[i] = value;}
	public genlist() {data = new T[0]; }
	public void add(T item) {
		T[] newdata = new T[size+1];
		System.Array.Copy(data, newdata, size);
		newdata[size]=item;
		data=newdata;
		}
}
