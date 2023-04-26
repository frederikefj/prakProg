default : Out.txt Aplot.svg ODE.svg ODE2.svg Newton.svg General.svg

Out.txt out.f1y.data out.ODE.data out.ODED.data out.Newton1.data out.Newton2.data out.General.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll genlist.dll ODE.dll
	mcs -reference:matrix.dll -reference:genlist.dll -reference:ODE.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

DIR2 = ../../exercises/generic_lists
genlist.dll : $(DIR2)/genlist.cs
	mcs -target:library -out:./genlist.dll $^

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

Aplot.svg : Aplot.gpi out.f1y.data
	gnuplot $<

ODE.svg : ODE.gpi out.ODE.data out.ODED.data
	gnuplot $<

ODE2.svg : ODE2.gpi out.ODE.data out.ODE2.data
	gnuplot $<

Newton.svg : Newton.gpi out.Newton1.data out.Newton2.data
	gnuplot $<

General.svg : General.gpi out.General.data
	gnuplot $<

ODE.dll : ODE.cs matrix.dll genlist.dll
	mcs -target:library -reference:matrix.dll -reference:genlist.dll -out:./ODE.dll ODE.cs
