default : Out.txt error.svg

Out.txt out.erf.data out.erfTap.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll Integ.dll
	mcs -reference:matrix.dll -reference:Integ.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

Integ.dll : Integ.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./Integ.dll Integ.cs

error.svg : error.gpi out.erf.data out.erfTab.data
	gnuplot error.gpi
