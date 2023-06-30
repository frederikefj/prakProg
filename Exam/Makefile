default : Out.txt points.svg

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
MKEXE = mcs -target:exe -out:$@ $(DLLS) $(CODE)
MKDLL = mcs -target:library -out:$@ $(DLLS) $(CODE)

Out.txt out.points2.data out.points2min.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll func.dll optimization.dll; $(MKEXE)

DIR = ../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

DIR2 = ../Homework/Minimization
mini.dll : $(DIR2)/mini.cs matrix.dll ; $(MKDLL)

func.dll : func.cs matrix.dll ; $(MKDLL)

optimization.dll : optimization.cs matrix.dll func.dll mini.dll; $(MKDLL)

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

points.svg : points.gpi out.points2.data out.points2min.data
	gnuplot $<



#spline.dll : spline.cs matrix.dll
#	mcs -target:library -reference:matrix.dll -out:./spline.dll spline.cs
