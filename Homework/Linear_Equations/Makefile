Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs -reference:matrix.dll main.cs -target:exe -out:main.exe

DIR = $(HOME)/repos/prakProg/matlib/matrix

DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean : 
	rm -f main.exe matrix.dll Out.dll
