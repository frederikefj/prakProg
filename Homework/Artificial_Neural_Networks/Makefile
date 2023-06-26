default : Out.txt

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
MKEXE = mcs -target:exe -out:$@ $(DLLS) $(CODE)
MKDLL = mcs -target:library -out:$@ $(DLLS) $(CODE)


Out.txt : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs matrix.dll mini.dll ; $(MKEXE)


DIR = ../../matlib/matrix
matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^


clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

mini.dll : ../Minimization/mini.cs matrix.dll ; $(MKDLL)


