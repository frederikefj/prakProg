Out.txt : main.exe dr_E0.svg rmax_E0.svg plot.svg
	mono main.exe -n:6 > Out.txt

main.exe : main.cs matrix.dll QRGS.dll jacobi.dll
	mcs -reference:matrix.dll -reference:jacobi.dll -reference:QRGS.dll main.cs -target:exe -out:main.exe

DIR = $(HOME)/repos/prakProg/matlib/matrix

DIR = ../../matlib/matrix

jacobi.dll : jacobi.cs matrix.dll QRGS.dll
	mcs -target:library -reference:matrix.dll -reference:QRGS.dll -out:jacobi.dll jacobi.cs

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

QRGS.dll : ../Linear_Equations/QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./QRGS.dll ../Linear_Equations/QRGS.cs

hydrogen.exe : hydrogen.cs jacobi.dll matrix.dll
	mcs -target:exe -reference:matrix.dll -reference:jacobi.dll -out:hydrogen.exe hydrogen.cs

cleanOld : 
	rm -f main.exe matrix.dll Out.dll QRGS.dll hydrogen.exe jacobi.dll out.dr_E0.data out.rmax_E0.data dr_E0.svg rmax_E0.svg

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

out.dr_E0.data : hydrogen.exe
	>$@
	for dr in $$(seq -f "%f" 0.04 0.04 2); do \
		echo $$dr; \
		mono $< -dr:$$dr -rmax:10 -out:dr_E0 1>>out.dr_E0.data ;\
	done

out.rmax_E0.data : hydrogen.exe
	>$@
	for rmax in $$(seq 1 1 20); do \
		echo $$rmax; \
		mono $< -dr:0.08 -rmax:$$rmax -out:rmax_E0 1>>out.rmax_E0.data ;\
	done

dr_E0.svg : dr_E0.gpi out.dr_E0.data
	gnuplot $<


rmax_E0.svg : rmax_E0.gpi out.rmax_E0.data
	gnuplot $<

out.E0_plot.data : hydrogen.exe
	mono hydrogen.exe -rmax:50 -dr:0.1 -energyLevel:0 -out:plot > out.E0_plot.data

out.E1_plot.data : hydrogen.exe
	mono hydrogen.exe -rmax:50 -dr:0.1 -energyLevel:1 -out:plot > out.E1_plot.data

out.E2_plot.data : hydrogen.exe
	mono hydrogen.exe -rmax:50 -dr:0.1 -energyLevel:2 -out:plot > out.E2_plot.data

plot.svg : plot.gpi out.E0_plot.data out.E1_plot.data out.E2_plot.data out.E0_analytical.data out.E1_analytical.data out.E2_analytical.data
	gnuplot $<

analytical.exe : analytical.cs matrix.dll
	mcs analytical.cs -reference:matrix.dll

out.E0_analytical.data : analytical.exe
	mono analytical.exe -rmax:8 -energyLevel:0 > out.E0_analytical.data

out.E1_analytical.data : analytical.exe
	mono analytical.exe -rmax:8 -energyLevel:1 > out.E1_analytical.data

out.E2_analytical.data : analytical.exe
	mono analytical.exe -rmax:8 -energyLevel:2 > out.E2_analytical.data
