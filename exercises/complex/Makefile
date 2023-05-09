Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll main.cs -target:exe -out:main.exe

DIR = $(HOME)/repos/prakProg/matlib/complex
cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^

