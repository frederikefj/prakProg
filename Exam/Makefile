default : Out.txt

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
MKEXE = mcs -target:exe -out:$@ $(DLLS) $(CODE)
MKDLL = mcs -target:library -out:$@ $(DLLS) $(CODE)

Out.txt : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll mini.dll func.dll; $(MKEXE)

DIR = ../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

DIR2 = ../Homework/Minimization
mini.dll : $(DIR2)/mini.cs matrix.dll ; $(MKDLL)

func.dll : func.cs matrix.dll ; $(MKDLL)

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

#Aplot.svg : Aplot.gpi out.points.data out.fz.data out.Integ.data
#	gnuplot $<


#spline.dll : spline.cs matrix.dll
#	mcs -target:library -reference:matrix.dll -out:./spline.dll spline.cs
