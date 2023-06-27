default : Out.txt ann1.svg

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
MKEXE = mcs -target:exe -out:$@ $(DLLS) $(CODE)
MKDLL = mcs -target:library -out:$@ $(DLLS) $(CODE)


Out.txt out.ann1Data.data out.ann1Guess.data out.ann1Fit.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll mini.dll ; $(MKEXE)


DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

ann1.svg : out.ann1Data.data out.ann1Guess.data out.ann1Fit.data ann1.gpi
	gnuplot ann1.gpi

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

mini.dll : ../Minimization/mini.cs matrix.dll ; $(MKDLL)


