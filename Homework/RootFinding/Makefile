default : Out.txt

Out.txt : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll Roots.dll
	mcs -reference:matrix.dll -reference:Roots.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

DIR2 = ../Linear_Equations
QRGS.dll : $(DIR2)/QRGS.cs matrix.dll
	mcs -reference:matrix.dll -target:library -out:./QRGS.dll $(DIR2)/QRGS.cs

#QRGS.dll : MalteQRGS.cs matrix.dll
#	mcs -reference:matrix.dll -target:library -out:./QRGS.dll MalteQRGS.cs

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

Roots.dll : Roots.cs matrix.dll QRGS.dll
	mcs -target:library -reference:matrix.dll -reference:QRGS.dll -out:./Roots.dll Roots.cs

#error.svg : error.gpi out.erf.data out.erfTab.data
#	gnuplot error.gpi
