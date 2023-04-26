default : Out.txt

Out.txt : main.exe numbers.txt
	cat numbers.txt | mono main.exe > Out.txt -target:$@


main.exe : main.cs matrix.dll genlist.dll
	mcs -reference:matrix.dll -reference:genlist.dll main.cs -target:exe -out:main.exe


DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

#Aplot.svg : Aplot.gpi out.points.data out.fz.data out.Integ.data
#	gnuplot $<


genlist.dll : genlist.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./genlist.dll genlist.cs
