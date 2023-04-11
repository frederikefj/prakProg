default : Out.txt decayFit.svg

Out.txt out.fit.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

QRGS.dll : ../Linear_Equations/QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./QRGS.dll ../Linear_Equations/QRGS.cs

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

decayFit.svg : decayFit.gpi decay.data out.fit.data
	gnuplot $<
