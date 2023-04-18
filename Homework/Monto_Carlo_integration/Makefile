default : Out.txt Aplot.svg Bplot.svg

Out.txt out.error.data out.errorQ.data out.errorM.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll MonteCarlo.dll
	mcs -reference:matrix.dll -reference:MonteCarlo.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

Aplot.svg : Aplot.gpi out.error.data
	gnuplot $<

Bplot.svg : Bplot.gpi out.errorQ.data out.errorM.data
	gnuplot $<

MonteCarlo.dll : MonteCarlo.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./MonteCarlo.dll MonteCarlo.cs
