Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs -target:exe -out:main.exe

DIR = $(HOME)/repos/prakProg/matlib/matrix

DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

QRGS.dll : QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./QRGS.dll QRGS.cs

clean : 
	rm -f main.exe matrix.dll Out.dll

decomp.exe : decomp.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll decomp.cs -target:exe -out:decomp.exe

out.times.data : decomp.exe
	>$@
	for N in $$(seq 100 20 1500); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N 1>out 2>err ;\
	done

plot.svg : plot.gpi out.times.data out.fit.data
	gnuplot $<

qubicFit.exe : qubicFit.cs out.times.data matrix.dll QRGS.dll
	mcs -target:exe -out:qubicFit.exe -reference:QRGS.dll -reference:matrix.dll qubicFit.cs

out.fit.data : qubicFit.exe
	mono qubicFit.exe > out.fit.data
