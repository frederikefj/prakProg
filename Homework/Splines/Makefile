default : Out.txt Aplot.svg Bplot.svg

Out.txt out.fz.data out.Integ.data out.points.data out.BInteg.data out.BDiff.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll spline.dll
	mcs -reference:matrix.dll -reference:spline.dll main.cs -target:exe -out:main.exe

DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

Aplot.svg : Aplot.gpi out.points.data out.fz.data out.Integ.data
	gnuplot $<

Bplot.svg : Bplot.gpi out.points.data out.Bfz.data out.BInteg.data out.BDiff.data
	gnuplot $<

spline.dll : spline.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./spline.dll spline.cs
